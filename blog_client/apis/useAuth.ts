'use client';

import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import { authService } from '@/services/auth.service';
import { ApiError } from '@/types/error';
import toast from 'react-hot-toast';
import { useEffect, useState } from 'react';

export const QUERY_KEYS = {
  USER: 'user',
};

export const useAuth = () => {
  const queryClient = useQueryClient();
  const [isClient, setIsClient] = useState(false);

  useEffect(() => {
    setIsClient(true);
  }, []);

  // Query for getting user info
  const { data: userInfo, isLoading: isLoadingUser } = useQuery({
    queryKey: [QUERY_KEYS.USER],
    queryFn: authService.myInfor,
    enabled: isClient && !!localStorage.getItem('accessToken'), // Chỉ gọi API khi ở client và có accessToken
    retry: false,
    staleTime: 5 * 60 * 1000, // Consider data fresh for 5 minutes
  });

  const login = useMutation({
    mutationFn: authService.login,
    onSuccess: async (response) => {
      if (!response.data) {
        throw new ApiError('Login failed: No data received', null, false);
      }
      // Save tokens to localStorage
      localStorage.setItem('accessToken', response.data.accessToken);
      localStorage.setItem('refreshToken', response.data.refreshToken);

      // Fetch user info immediately after login
      await queryClient.fetchQuery({
        queryKey: [QUERY_KEYS.USER],
        queryFn: authService.myInfor,
      });

      // Navigate to home page or dashboard
      window.location.href = '/';
    },
    onError: (error: ApiError) => {
      toast.error(`${error.message}`);
    },
  });

  const register = useMutation({
    mutationFn: authService.register,
    onSuccess: () => {
      toast.success('Registration successful! Please login.');
      // Navigate to login page after successful registration
      window.location.href = '/login';
    },
    onError: (error: ApiError) => {
      toast.error(`${error.message}`);
    },
  });

  const revokeToken = useMutation({
    mutationFn: authService.revokeToken,
    onSuccess: () => {
      // Clear local storage
      localStorage.removeItem('accessToken');
      localStorage.removeItem('refreshToken');

      // Clear user data from cache
      queryClient.removeQueries({ queryKey: [QUERY_KEYS.USER] });

      toast.success('Logged out successfully');
      // Navigate to login page
      window.location.href = '/login';
    },
    onError: (error: ApiError) => {
      toast.error(`Logout failed: ${error.message}`);
    },
  });

  const logout = () => {
    revokeToken.mutate();
  };

  return {
    login,
    register,
    logout,
    revokeToken,
    user: userInfo?.data || null,
    isLoadingUser: !isClient || isLoadingUser,
  };
};
