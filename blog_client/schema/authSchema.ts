import { z } from 'zod';

export const loginSchema = z.object({
  username: z.string().min(1),
  password: z.string().min(8),
});

export const registerSchema = z
  .object({
    username: z
      .string()
      .min(1, { message: 'Tên tài khoản là bắt buộc' })
      .max(255, { message: 'Tên tài khoản không vượt quá 255 ký tự' }),
    email: z
      .string()
      .min(1, { message: 'Email là bắt buộc' })
      .email({ message: 'Email không hợp lệ' }),
    password: z
      .string()
      .min(1, { message: 'Mật khẩu là bắt buộc' })
      .min(8, { message: 'Mật khẩu cần ít nhất 8 ký tự' }),
    confirmPassword: z.string().min(1, { message: 'Xác nhận mật khẩu là bắt buộc' }),
  })
  .refine((data) => data.password === data.confirmPassword, {
    message: 'Mật khảu xác nhận không khớp',
    path: ['confirmPassword'],
  });
