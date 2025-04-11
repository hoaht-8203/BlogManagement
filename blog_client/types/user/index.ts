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
