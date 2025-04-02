/* eslint-disable @typescript-eslint/no-explicit-any */
import { API_ENDPOINTS } from '@/constants/endpoints';
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

// Add request interceptor to add auth token
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
  (error) => Promise.reject(error),
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

const handleUnauthorized = () => {
  if (isClient) {
    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken');
    window.location.href = '/login';
  }
};

// Add response interceptor to handle token refresh
axiosInstance.interceptors.response.use(
  (response) => response,
  async (error) => {
    const originalRequest = error.config;

    // Case 1: Refresh token API returns 401
    // This means both access token and refresh token are invalid
    if (
      error.response?.status === 401 &&
      originalRequest.url === API_ENDPOINTS.AUTH.REFRESH_TOKEN
    ) {
      handleUnauthorized();
      throw new ApiError('Authentication failed - Please login again', null, false);
    }

    // Case 2: Any other API returns 401 and we haven't tried refreshing yet
    if (error.response?.status === 401 && !originalRequest._retry) {
      if (!isClient) {
        throw new ApiError('Unauthorized - Please login', null, false);
      }

      // Check if we have tokens
      const accessToken = localStorage.getItem('accessToken');
      const refreshToken = localStorage.getItem('refreshToken');

      if (!accessToken || !refreshToken) {
        handleUnauthorized();
        throw new ApiError('No tokens found - Please login', null, false);
      }

      // If already refreshing, add request to queue
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
              handleUnauthorized();
              throw err;
            }
            handleUnauthorized();
            throw new ApiError('Authentication failed', null, false);
          });
      }

      // Start refresh token process
      originalRequest._retry = true;
      isRefreshing = true;

      try {
        const response = await authService.refreshToken({
          accessToken,
          refreshToken,
        });

        if (!response.data) {
          handleUnauthorized();
          throw new ApiError('Refresh token failed - No data received', null, false);
        }

        const { accessToken: newAccessToken, refreshToken: newRefreshToken } = response.data;

        // Update tokens
        localStorage.setItem('accessToken', newAccessToken);
        localStorage.setItem('refreshToken', newRefreshToken);
        axiosInstance.defaults.headers.common.Authorization = `Bearer ${newAccessToken}`;

        // Process queued requests
        processQueue(null, newAccessToken);
        return axiosInstance(originalRequest);
      } catch (err) {
        processQueue(err, null);
        handleUnauthorized();

        if (err instanceof ApiError) {
          throw err;
        }
        throw new ApiError('Authentication failed', null, false);
      } finally {
        isRefreshing = false;
      }
    }

    // Case 3: Handle other API errors
    if (axios.isAxiosError(error) && error.response?.data) {
      const apiError = error.response.data as ApiResponse<unknown>;
      throw new ApiError(apiError.message, apiError.errors, apiError.success);
    }

    // Case 4: Handle unknown errors
    throw new ApiError('An unexpected error occurred', null, false);
  },
);

export default axiosInstance;
