export interface ApiResponse<T> {
  success: boolean;
  message: string;
  data: T | null;
  errors: string[] | null;
}

export type ApiResponseData<T> = T extends ApiResponse<infer D> ? D : never;
