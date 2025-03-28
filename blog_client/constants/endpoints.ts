export const API_ENDPOINTS = {
  TEST: '/Test',
  AUTH: {
    LOGIN: '/auth/login',
    REGISTER: '/auth/register',
    REFRESH_TOKEN: '/auth/refresh-token',
    MY_INFO: '/auth/my-info',
    LOGOUT: '/auth/revoke',
  },
} as const;
