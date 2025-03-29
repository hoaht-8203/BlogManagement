import axiosInstance from '@/configs/axios';
import { API_ENDPOINTS } from '@/constants/endpoints';
import { ApiResponse } from '@/types/api';
import {
  LoginRequest,
  LoginResponse,
  MyInfoResponse,
  RefreshTokenRequest,
  RegisterRequest,
  RegisterResponse,
  RefreshTokenResponse,
  GoogleLoginRequest,
  ForgotPasswordRequest,
  VerifyResetTokenRequest,
} from '@/types/auth';

export const authService = {
  login: async (data: LoginRequest): Promise<ApiResponse<LoginResponse>> => {
    const response = await axiosInstance.post(API_ENDPOINTS.AUTH.LOGIN, data);
    return response.data;
  },

  googleLogin: async (data: GoogleLoginRequest): Promise<ApiResponse<LoginResponse>> => {
    const response = await axiosInstance.post<ApiResponse<LoginResponse>>(
      '/auth/google-login',
      data,
    );
    return response.data;
  },

  register: async (data: RegisterRequest): Promise<ApiResponse<RegisterResponse>> => {
    const response = await axiosInstance.post(API_ENDPOINTS.AUTH.REGISTER, data);
    return response.data;
  },

  refreshToken: async (data: RefreshTokenRequest): Promise<ApiResponse<RefreshTokenResponse>> => {
    const response = await axiosInstance.post(API_ENDPOINTS.AUTH.REFRESH_TOKEN, data);
    return response.data;
  },

  myInfor: async (): Promise<ApiResponse<MyInfoResponse>> => {
    const response = await axiosInstance.get(API_ENDPOINTS.AUTH.MY_INFO);
    return response.data;
  },

  revokeToken: async (): Promise<ApiResponse<null>> => {
    const response = await axiosInstance.post(API_ENDPOINTS.AUTH.LOGOUT);
    return response.data;
  },

  forgotPassword: async (data: ForgotPasswordRequest): Promise<ApiResponse<string>> => {
    const response = await axiosInstance.post(API_ENDPOINTS.AUTH.FORGOT_PASSWORD, data);
    return response.data;
  },

  verifyResetToken: async (data: VerifyResetTokenRequest): Promise<ApiResponse<string>> => {
    const response = await axiosInstance.post(API_ENDPOINTS.AUTH.VERIFY_RESET_TOKEN, data);
    return response.data;
  },
};
