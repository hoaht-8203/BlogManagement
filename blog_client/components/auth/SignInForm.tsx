import { Button } from '@/components/ui/button';
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from '@/components/ui/form';
import { Input } from '@/components/ui/input';
import { zodResolver } from '@hookform/resolvers/zod';
import { useForm } from 'react-hook-form';
import { z } from 'zod';
import { Card, CardContent, CardDescription, CardFooter, CardHeader, CardTitle } from '../ui/card';
import { loginSchema } from '@/schema/authSchema';
import { useAuth } from '@/apis/useAuth';
import GoogleSignInButton from './GoogleSignInButton';
import Link from 'next/link';
import { useState } from 'react';
import { Loader2 } from 'lucide-react';
import { ApiError } from '@/types/error';

const SignInForm = () => {
  const { login } = useAuth();
  const [isLoadingForm, setIsLoadingForm] = useState(false);

  const form = useForm<z.infer<typeof loginSchema>>({
    resolver: zodResolver(loginSchema),
    defaultValues: {
      username: '',
      password: '',
    },
  });

  function onSubmit(values: z.infer<typeof loginSchema>) {
    setIsLoadingForm(true);
    login.mutate(
      {
        username: values.username,
        password: values.password,
      },
      {
        onError(error: ApiError) {
          setIsLoadingForm(false);
          error.errors?.forEach((err) => {
            const [field, message] = err.split(':');
            form.setError(field as 'username' | 'password', {
              type: 'server',
              message: message,
            });
          });
        },
        onSuccess() {
          setIsLoadingForm(false);
        },
      },
    );
  }

  return (
    <>
      <Form {...form}>
        <form onSubmit={form.handleSubmit(onSubmit)}>
          <Card className="w-[450px]">
            <CardHeader>
              <CardTitle>
                Đăng nhập <span className="text-blue-500">OurBlog</span>
              </CardTitle>
              <CardDescription>
                Chào mừng bạn quay trở lại! Vui lòng đăng nhập vào tài khoản của bạn để tiếp tục.
              </CardDescription>
            </CardHeader>
            <CardContent>
              <div className="grid gap-5">
                <FormField
                  control={form.control}
                  name="username"
                  render={({ field }) => (
                    <FormItem>
                      <FormLabel>Tên tài khoản hoặc email</FormLabel>
                      <FormControl>
                        <Input placeholder="Nhập tên tài khoản hoặc email của bạn" {...field} />
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )}
                />
                <FormField
                  control={form.control}
                  name="password"
                  render={({ field }) => (
                    <FormItem>
                      <FormLabel>Mật khẩu</FormLabel>
                      <FormControl>
                        <Input type="password" placeholder="Nhập mật khẩu của bạn" {...field} />
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )}
                />
              </div>
            </CardContent>
            <CardFooter className="grid grid-cols-1 gap-3 text-center">
              <Button type="submit" className="w-full" disabled={isLoadingForm}>
                {isLoadingForm ? (
                  <>
                    <Loader2 className="animate-spin" />
                    {'Đang đăng nhập'}
                  </>
                ) : (
                  'Đăng nhập'
                )}
              </Button>

              <div className="flex justify-between">
                <span className="text-sm">
                  <Link
                    href="/forgot-password"
                    className="text-blue-500 hover:text-blue-500/80 hover:underline"
                  >
                    Quên mật khẩu?
                  </Link>
                </span>
                <span className="text-sm">
                  <Link
                    href="/sign-up"
                    className="text-blue-500 hover:text-blue-500/80 hover:underline"
                  >
                    Tạo tài khoản
                  </Link>
                </span>
              </div>

              <div className="relative">
                <div className="absolute inset-0 flex items-center">
                  <span className="w-full border-t" />
                </div>
                <div className="relative flex justify-center text-xs uppercase">
                  <span className="bg-background text-muted-foreground px-2">
                    Hoặc tiếp tục với
                  </span>
                </div>
              </div>
              <GoogleSignInButton />
            </CardFooter>
          </Card>
        </form>
      </Form>
    </>
  );
};

export default SignInForm;
