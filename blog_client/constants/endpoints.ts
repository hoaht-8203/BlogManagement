export const API_ENDPOINTS = {
  TEST: '/Test',
  AUTH: {
    LOGIN: '/auth/login',
    GOOGLE_LOGIN: '/auth/google-login',
    REGISTER: '/auth/register',
    REFRESH_TOKEN: '/auth/refresh-token',
    MY_INFO: '/auth/my-info',
    LOGOUT: '/auth/revoke',
    FORGOT_PASSWORD: '/auth/forgot-password',
    VERIFY_RESET_TOKEN: '/auth/verify-reset-token',
    UPDATE_INFO: '/auth/update-info',
    RESET_PASSWORD: '/auth/reset-password',
  },
} as const;
