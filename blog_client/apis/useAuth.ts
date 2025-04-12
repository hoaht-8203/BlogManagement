'use client';

import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import { authService } from '@/services/auth.service';
import { ApiError } from '@/types/error';
import toast from 'react-hot-toast';
import { useEffect, useState } from 'react';
import { useRouter } from 'next/navigation';
import { ApiResponse } from '@/types/api';
import { QUERY_KEYS } from '@/types/api_key';

export const useAuth = () => {
  const router = useRouter();
  const queryClient = useQueryClient();
  const [isClient, setIsClient] = useState(false);

  useEffect(() => {
    setIsClient(true);
  }, []);

  const { data: userInfo, isLoading: isLoadingUser } = useQuery({
    queryKey: [QUERY_KEYS.AUTHENTICATED_USER],
    queryFn: authService.myInfor,
    enabled: isClient && !!localStorage.getItem('accessToken'),
    retry: false,
    staleTime: 5 * 60 * 1000,
  });

  const login = useMutation({
    mutationFn: authService.login,
    onSuccess: async (response) => {
      if (!response.data) {
        throw new ApiError('Login failed: No data received', null, false);
      }
      localStorage.setItem('accessToken', response.data.accessToken);
      localStorage.setItem('refreshToken', response.data.refreshToken);

      await queryClient.fetchQuery({
        queryKey: [QUERY_KEYS.AUTHENTICATED_USER],
        queryFn: authService.myInfor,
      });

      router.push('/');
    },
    onError: (error: ApiError) => {
      toast.error(`Login failed: ${error.message}`);
    },
  });

  const googleLogin = useMutation({
    mutationFn: authService.googleLogin,
    onSuccess: async (response) => {
      if (!response.data) {
        throw new ApiError('Google login failed: No data received', null, false);
      }

      localStorage.setItem('accessToken', response.data.accessToken);
      localStorage.setItem('refreshToken', response.data.refreshToken);

      await queryClient.fetchQuery({
        queryKey: [QUERY_KEYS.AUTHENTICATED_USER],
        queryFn: authService.myInfor,
      });

      router.push('/');
    },
    onError: (error: ApiError) => {
      toast.error(`Google login failed: ${error.message}`);
    },
  });

  const register = useMutation({
    mutationFn: authService.register,
    onSuccess: (response) => {
      toast.success('Đăng ký thành công! Vui lòng kiểm tra email để xác thực tài khoản.');
      router.push(`/verify-email?email=${response.data?.email}`);
    },
    onError: (error: ApiError) => {
      toast.error(`Đăng ký thất bại: ${error.message}`);
    },
  });

  const revokeToken = useMutation({
    mutationFn: authService.revokeToken,
    onSuccess: () => {
      localStorage.removeItem('accessToken');
      localStorage.removeItem('refreshToken');

      queryClient.removeQueries({ queryKey: [QUERY_KEYS.AUTHENTICATED_USER] });

      toast.success('Logged out successfully');

      router.prefetch('/');
    },
    onError: (error: ApiError) => {
      toast.error(`Logout failed: ${error.message}`);
    },
  });

  const logout = () => {
    revokeToken.mutate();
  };

  const forgotPassword = useMutation({
    mutationFn: authService.forgotPassword,
    onSuccess: () => {
      toast.success('Mã xác thực đã được gửi đến email của bạn');
    },
    onError: (error: ApiError) => {
      toast.error(`${error.message}`);
    },
  });

  const verifyResetToken = useMutation({
    mutationFn: authService.verifyResetToken,
    onSuccess: () => {
      toast.success('Mật khẩu mới đã được gửi đến email của bạn');
      router.push('/login');
    },
    onError: (error: ApiError) => {
      toast.error(`${error.message}`);
    },
  });

  const updateInfo = useMutation({
    mutationFn: authService.updateInfo,
    onSuccess: (data: ApiResponse<string>) => {
      toast.success(data.message);
      queryClient.invalidateQueries({ queryKey: [QUERY_KEYS.AUTHENTICATED_USER] });
    },
  });

  const resetPassword = useMutation({
    mutationFn: authService.resetPassword,
    onSuccess: (data: ApiResponse<string>) => {
      toast.success(data.message);
      queryClient.invalidateQueries({ queryKey: [QUERY_KEYS.AUTHENTICATED_USER] });
    },
    onError: (error: ApiError) => {
      toast.error(`${error.message}`);
    },
  });

  return {
    login,
    googleLogin,
    register,
    logout,
    revokeToken,
    user: userInfo?.data || null,
    isLoadingUser: !isClient || isLoadingUser,
    forgotPassword,
    verifyResetToken,
    updateInfo,
    resetPassword,
  };
};
