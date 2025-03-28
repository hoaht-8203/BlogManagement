/* eslint-disable @typescript-eslint/no-explicit-any */
import { authService } from '@/services/auth.service';
import { ApiResponse } from '@/types/api';
import { ApiError } from '@/types/error';
import axios from 'axios';

const isClient = typeof window !== 'undefined';

const axiosInstance = axios.create({
  baseURL: process.env.NEXT_PUBLIC_API_URL || 'http://localhost:5010/api',
  headers: {
    'Content-Type': 'application/json',
  },
});

// Add interceptors
axiosInstance.interceptors.request.use(
  (config) => {
    if (isClient) {
      const token = localStorage.getItem('accessToken');
      if (token) {
        config.headers.Authorization = `Bearer ${token}`;
      }
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  },
);

let isRefreshing = false;
let failedQueue: any[] = [];

const processQueue = (error: any, token: string | null = null) => {
  failedQueue.forEach((prom) => {
    if (error) {
      prom.reject(error);
    } else {
      prom.resolve(token);
    }
  });
  failedQueue = [];
};

axiosInstance.interceptors.response.use(
  (response) => response,
  async (error) => {
    const originalRequest = error.config;

    // Xử lý 401 và refresh token
    if (error.response?.status === 401 && !originalRequest._retry) {
      if (!isClient) {
        throw new ApiError('Unauthorized - Please login', null, false);
      }

      // Kiểm tra tokens
      const accessToken = localStorage.getItem('accessToken');
      const refreshToken = localStorage.getItem('refreshToken');

      // Nếu không có tokens, redirect to login
      if (!accessToken || !refreshToken) {
        localStorage.removeItem('accessToken');
        localStorage.removeItem('refreshToken');
        window.location.href = '/login'; // Redirect trước
        throw new ApiError('Unauthorized - Please login', null, false);
      }

      // Nếu đang refresh, thêm request vào queue
      if (isRefreshing) {
        return new Promise((resolve, reject) => {
          failedQueue.push({ resolve, reject });
        })
          .then((token) => {
            originalRequest.headers.Authorization = `Bearer ${token}`;
            return axiosInstance(originalRequest);
          })
          .catch((err) => {
            if (err instanceof ApiError) {
              window.location.href = '/login';
              throw err;
            }
            window.location.href = '/login';
            throw new ApiError('Authentication failed', null, false);
          });
      }

      // Bắt đầu refresh token
      originalRequest._retry = true;
      isRefreshing = true;

      try {
        const response = await authService.refreshToken({
          accessToken,
          refreshToken,
        });

        if (!response.data) {
          window.location.href = '/login';
          throw new ApiError('Refresh token failed: No data received', null, false);
        }

        const { accessToken: newAccessToken, refreshToken: newRefreshToken } = response.data;

        localStorage.setItem('accessToken', newAccessToken);
        localStorage.setItem('refreshToken', newRefreshToken);

        axiosInstance.defaults.headers.common.Authorization = `Bearer ${newAccessToken}`;
        processQueue(null, newAccessToken);
        return axiosInstance(originalRequest);
      } catch (err) {
        processQueue(err, null);
        // Clear tokens and redirect
        localStorage.removeItem('accessToken');
        localStorage.removeItem('refreshToken');
        window.location.href = '/login';

        if (err instanceof ApiError) {
          throw err;
        }
        throw new ApiError('Authentication failed', null, false);
      } finally {
        isRefreshing = false;
      }
    }

    // Xử lý các error khác
    if (axios.isAxiosError(error) && error.response?.data) {
      const apiError = error.response.data as ApiResponse<unknown>;
      // Nếu là lỗi unauthorized, redirect to login
      if (error.response.status === 401) {
        localStorage.removeItem('accessToken');
        localStorage.removeItem('refreshToken');
        window.location.href = '/login';
      }
      throw new ApiError(apiError.message, apiError.errors, apiError.success);
    }

    // Nếu không phải error đã biết, throw generic error
    throw new ApiError('An unexpected error occurred', null, false);
  },
);

export default axiosInstance;
