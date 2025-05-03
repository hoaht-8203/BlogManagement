import { PaginationRequest } from '../api';

export interface ListRoleRequest extends PaginationRequest {
  name?: string;
}

export interface ListRoleResponse extends PaginationRequest {
  id: number;
  name: string;
  description: string;
  createdAt: Date;
  updatedAt: Date;
  createdBy: string;
  updatedBy: string;
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
