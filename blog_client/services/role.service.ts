import axiosInstance from '@/configs/axios';
import { API_ENDPOINTS } from '@/constants/endpoints';
import { ApiResponse, PaginatedList } from '@/types/api';
import {
  CreateRoleRequest,
  DeleteRoleRequest,
  ListRoleRequest,
  ListRoleResponse,
  UpdateRoleRequest,
} from '@/types/role';

export const roleService = {
  listRole: async (
    params: ListRoleRequest,
  ): Promise<ApiResponse<PaginatedList<ListRoleResponse>>> => {
    const response = await axiosInstance.get<ApiResponse<PaginatedList<ListRoleResponse>>>(
      API_ENDPOINTS.ROLE.LIST,
      {
        params,
      },
    );
    return response.data;
  },

  createRole: async (request: CreateRoleRequest): Promise<ApiResponse<void>> => {
    const response = await axiosInstance.post<ApiResponse<void>>(
      API_ENDPOINTS.ROLE.CREATE,
      request,
    );
    return response.data;
  },

  updateRole: async (request: UpdateRoleRequest): Promise<ApiResponse<void>> => {
    const response = await axiosInstance.put<ApiResponse<void>>(API_ENDPOINTS.ROLE.UPDATE, request);
    return response.data;
  },

  deleteRole: async (request: DeleteRoleRequest): Promise<ApiResponse<void>> => {
    const response = await axiosInstance.delete<ApiResponse<void>>(API_ENDPOINTS.ROLE.DELETE, {
      params: request,
    });
    return response.data;
  },
};
