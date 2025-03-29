import { z } from 'zod';

export const loginSchema = z.object({
  username: z.string().min(1, { message: 'Tên tài khoản là bắt buộc' }),
  password: z
    .string()
    .min(1, { message: 'Mật khẩu là bắt buộc' })
    .min(8, { message: 'Mật khẩu cần ít nhất 8 ký tự' }),
});

export const registerSchema = z
  .object({
    username: z
      .string()
      .min(1, { message: 'Tên tài khoản là bắt buộc' })
      .max(255, { message: 'Tên tài khoản không vượt quá 255 ký tự' })
      .regex(/^[a-zA-Z0-9_.]+$/, {
        message: 'Tên tài khoản chỉ được chứa chữ cái không dấu, số và dấu gạch dưới',
      }),
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
    message: 'Mật khẩu xác nhận không khớp',
    path: ['confirmPassword'],
  });

export const forgotPasswordSchema = z.object({
  email: z.string().email('Email không hợp lệ'),
});

export const verifyResetTokenSchema = z.object({
  email: z.string().email('Email không hợp lệ'),
  token: z.string().length(6, 'Mã xác thực phải có 6 chữ số'),
});
