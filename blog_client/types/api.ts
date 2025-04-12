export interface ApiResponse<T> {
  success: boolean;
  message: string;
  data: T | null;
  errors: string[] | null;
}

export type ApiResponseData<T> = T extends ApiResponse<infer D> ? D : never;

export interface PaginatedList<T> {
  items: T[];
  pageNumber: number;
  pageSize: number;
  totalRecords: number;
  totalPages: number;
  hasPrevious: boolean;
  hasNext: boolean;
}

export interface PaginationRequest {
  pageNumber: number;
  pageSize: number;
}
