import axiosInstance from '@/configs/axios';
import { API_ENDPOINTS } from '@/constants/endpoints';
import { ApiResponse, PaginatedList } from '@/types/api';
import { ListUserRequest, ListUserResponse } from '@/types/user';

export const userService = {
  listUser: async (
    params: ListUserRequest,
  ): Promise<ApiResponse<PaginatedList<ListUserResponse>>> => {
    const response = await axiosInstance.get<ApiResponse<PaginatedList<ListUserResponse>>>(
      API_ENDPOINTS.USER.LIST,
      {
        params,
      },
    );
    return response.data;
  },
};
