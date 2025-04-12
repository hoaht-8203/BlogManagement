import { PaginationRequest } from '../api';

export interface ListUserResponse {
  id: string;
  username: string;
  email: string;
  fullName: string;
  address: string;
  phone: string;
  avatarUrl: string;
  status: number;
  isEmailVerified: boolean;
  roles: string[];
  createDate: Date;
  updateDate: Date;
}

export interface ListUserRequest extends PaginationRequest {
  username?: string;
  email?: string;
  fullName?: string;
  address?: string;
  phone?: string;
  status?: number;
}
