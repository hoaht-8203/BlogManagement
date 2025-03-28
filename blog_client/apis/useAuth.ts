'use client';

import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import { authService } from '@/services/auth.service';
import { ApiError } from '@/types/error';
import toast from 'react-hot-toast';
import { useEffect, useState } from 'react';
import { useRouter } from 'next/navigation';

export const QUERY_KEYS = {
  USER: 'user',
};

export const useAuth = () => {
  const router = useRouter();
  const queryClient = useQueryClient();
  const [isClient, setIsClient] = useState(false);

  useEffect(() => {
    setIsClient(true);
  }, []);

  const { data: userInfo, isLoading: isLoadingUser } = useQuery({
    queryKey: [QUERY_KEYS.USER],
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
        queryKey: [QUERY_KEYS.USER],
        queryFn: authService.myInfor,
      });

      router.push('/');
    },
    onError: (error: ApiError) => {
      toast.error(`${error.message}`);
    },
  });

  const register = useMutation({
    mutationFn: authService.register,
    onSuccess: () => {
      toast.success('Registration successful! Please login.');
      router.push('/login');
    },
    onError: (error: ApiError) => {
      toast.error(`${error.message}`);
    },
  });

  const revokeToken = useMutation({
    mutationFn: authService.revokeToken,
    onSuccess: () => {
      localStorage.removeItem('accessToken');
      localStorage.removeItem('refreshToken');

      queryClient.removeQueries({ queryKey: [QUERY_KEYS.USER] });

      toast.success('Logged out successfully');

      router.refresh();
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
