import axiosInstance from '@/configs/axios';
import { API_ENDPOINTS } from '@/constants/endpoints';
import { ListTypeRequest, ListTypeResponse } from '@/types/type';
import { ApiResponse } from '@/types/api';

const APP_TYPES = {
  APP_ROLES: 'APP_ROLES',
  APP_STATUS: 'APP_STATUS',
};

export const typeService = {
  listRolesType: async (): Promise<ApiResponse<ListTypeResponse[]>> => {
    const params: ListTypeRequest = {
      type: APP_TYPES.APP_ROLES,
    };

    const response = await axiosInstance.get<ApiResponse<ListTypeResponse[]>>(
      API_ENDPOINTS.TYPE.LIST,
      {
        params,
      },
    );
    return response.data;
  },
};
