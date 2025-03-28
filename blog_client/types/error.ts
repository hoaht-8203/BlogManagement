export class ApiError extends Error {
  constructor(
    message: string,
    public errors?: string[] | null,
    public success: boolean = false,
  ) {
    super(message);
    this.name = 'ApiError';
  }
}
