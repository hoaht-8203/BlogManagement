import axiosInstance from '@/configs/axios';
import { API_ENDPOINTS } from '@/constants/endpoints';
import { ApiResponse } from '@/types/api';
import { MyInfoResponse } from '@/types/auth';
import { ListUserResponse } from '@/types/user';

export const userService = {
  listUser: async (): Promise<ApiResponse<ListUserResponse[]>> => {
    const response = await axiosInstance.get<ApiResponse<ListUserResponse[]>>(
      API_ENDPOINTS.USER.LIST,
    );
    return response.data;
  },
};
