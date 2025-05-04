import { PaginationRequest } from '../api';

export interface ListRoleRequest extends PaginationRequest {
  name?: string;
}

export interface ListRoleResponse {
  id: number;
  name: string;
  description: string;
  totalUsers: number;
  createBy: string;
  updateBy: string;
  createDate: Date;
  updateDate: Date;
}

export interface CreateRoleRequest {
  name: string;
  description: string;
}

export interface UpdateRoleRequest {
  id: number;
  name: string;
  description: string;
}

export interface DeleteRoleRequest {
  id: number;
}
