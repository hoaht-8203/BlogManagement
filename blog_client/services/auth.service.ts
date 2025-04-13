import axiosInstance from '@/configs/axios';
import { API_ENDPOINTS } from '@/constants/endpoints';
import { ApiResponse } from '@/types/api';
import {
  ForgotPasswordRequest,
  GoogleLoginRequest,
  LoginRequest,
  LoginResponse,
  MyInfoResponse,
  RefreshTokenRequest,
  RefreshTokenResponse,
  RegisterRequest,
  RegisterResponse,
  ResetPasswordRequest,
  UpdateInfoRequest,
  VerifyEmailRequest,
  VerifyResetTokenRequest,
} from '@/types/auth';

export const authService = {
  login: async (data: LoginRequest): Promise<ApiResponse<LoginResponse>> => {
    const response = await axiosInstance.post<ApiResponse<LoginResponse>>(
      API_ENDPOINTS.AUTH.LOGIN,
      data,
    );
    return response.data;
  },

  googleLogin: async (data: GoogleLoginRequest): Promise<ApiResponse<LoginResponse>> => {
    const response = await axiosInstance.post<ApiResponse<LoginResponse>>(
      API_ENDPOINTS.AUTH.GOOGLE_LOGIN,
      data,
    );
    return response.data;
  },

  register: async (data: RegisterRequest): Promise<ApiResponse<RegisterResponse>> => {
    const response = await axiosInstance.post<ApiResponse<RegisterResponse>>(
      API_ENDPOINTS.AUTH.REGISTER,
      data,
    );
    return response.data;
  },

  refreshToken: async (data: RefreshTokenRequest): Promise<ApiResponse<RefreshTokenResponse>> => {
    const response = await axiosInstance.post<ApiResponse<RefreshTokenResponse>>(
      API_ENDPOINTS.AUTH.REFRESH_TOKEN,
      data,
    );
    return response.data;
  },

  myInfor: async (): Promise<ApiResponse<MyInfoResponse>> => {
    const response = await axiosInstance.get<ApiResponse<MyInfoResponse>>(
      API_ENDPOINTS.AUTH.MY_INFO,
    );
    return response.data;
  },

  revokeToken: async (): Promise<ApiResponse<null>> => {
    const response = await axiosInstance.post<ApiResponse<null>>(API_ENDPOINTS.AUTH.LOGOUT);
    return response.data;
  },

  forgotPassword: async (data: ForgotPasswordRequest): Promise<ApiResponse<string>> => {
    const response = await axiosInstance.post<ApiResponse<string>>(
      API_ENDPOINTS.AUTH.FORGOT_PASSWORD,
      data,
    );
    return response.data;
  },

  verifyResetToken: async (data: VerifyResetTokenRequest): Promise<ApiResponse<string>> => {
    const response = await axiosInstance.post<ApiResponse<string>>(
      API_ENDPOINTS.AUTH.VERIFY_RESET_TOKEN,
      data,
    );
    return response.data;
  },

  updateInfo: async (data: UpdateInfoRequest): Promise<ApiResponse<string>> => {
    const response = await axiosInstance.put<ApiResponse<string>>(
      API_ENDPOINTS.AUTH.UPDATE_INFO,
      data,
    );
    return response.data;
  },

  resetPassword: async (data: ResetPasswordRequest): Promise<ApiResponse<string>> => {
    const response = await axiosInstance.post<ApiResponse<string>>(
      API_ENDPOINTS.AUTH.RESET_PASSWORD,
      data,
    );
    return response.data;
  },

  verifyEmail: async (data: VerifyEmailRequest): Promise<ApiResponse<string>> => {
    const response = await axiosInstance.post<ApiResponse<string>>(
      API_ENDPOINTS.AUTH.VERIFY_EMAIL,
      data,
    );
    return response.data;
  },
};
