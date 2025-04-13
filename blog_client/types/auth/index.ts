export interface LoginRequest {
  username: string;
  password: string;
}

export interface GoogleLoginRequest {
  idToken: string;
}

export interface LoginResponse {
  accessToken: string;
  refreshToken: string;
  username: string;
  roles: string[];
}

export interface RegisterRequest {
  username: string;
  email: string;
  password: string;
}

export interface RegisterResponse {
  username: string;
  email: string;
}

export interface RefreshTokenRequest {
  accessToken: string;
  refreshToken: string;
}

export interface RefreshTokenResponse {
  accessToken: string;
  refreshToken: string;
}

export interface MyInfoResponse {
  username: string;
  email: string;
  fullName: string;
  address: string;
  phone: string;
  avatarUrl: string;
}

export interface ForgotPasswordRequest {
  email: string;
}

export interface VerifyResetTokenRequest {
  email: string;
  token: string;
}

export interface ResetPasswordRequest {
  oldPassword: string;
  newPassword: string;
}

export interface UpdateInfoRequest {
  fullName: string;
  phone: string;
  address: string;
  avatarUrl: string;
}

export interface VerifyEmailRequest {
  email: string;
  token: string;
}
