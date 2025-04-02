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
import { registerSchema } from '@/schema/authSchema';
import { useAuth } from '@/apis/useAuth';
import { ApiError } from '@/types/error';
import { useState } from 'react';
import { Loader2 } from 'lucide-react';

const SignUpForm = () => {
  const { register } = useAuth();
  const [loadingSubmitForm, setLoadingSubmitForm] = useState(false);

  const form = useForm<z.infer<typeof registerSchema>>({
    resolver: zodResolver(registerSchema),
    defaultValues: {
      username: '',
      email: '',
      password: '',
      confirmPassword: '',
    },
  });

  function onSubmit(values: z.infer<typeof registerSchema>) {
    setLoadingSubmitForm(true);
    register.mutate(
      {
        username: values.username,
        email: values.email,
        password: values.password,
      },
      {
        onError(error: ApiError) {
          error.errors?.forEach((err) => {
            const [field, message] = err.split(':');
            form.setError(field as 'email' | 'username', {
              type: 'server',
              message: message,
            });
          });
        },
        onSuccess() {
          setLoadingSubmitForm(false);
        },
      },
    );
  }

  return (
    <>
      <Form {...form}>
        <form onSubmit={form.handleSubmit(onSubmit)} className="mx-auto w-full md:w-[550px]">
          <Card>
            <CardHeader>
              <CardTitle>
                Đăng ký tài khoản <span className="text-blue-500">OurBlog</span>
              </CardTitle>
              <CardDescription>
                Chào mừng bạn đến Nền tảng OurBlog! Tham gia cùng chúng tôi để tìm kiếm thông tin
                hữu ích cần thiết để cải thiện kỹ năng IT của bạn. Vui lòng điền thông tin của bạn
                vào biểu mẫu bên dưới để tiếp tục. .
              </CardDescription>
            </CardHeader>
            <CardContent>
              <div className="grid grid-cols-2 items-start gap-5">
                <FormField
                  control={form.control}
                  name="username"
                  render={({ field }) => (
                    <FormItem className="col-span-2 md:col-span-1">
                      <FormLabel>Tên tài khoản (username)</FormLabel>
                      <FormControl>
                        <Input placeholder="Nhập tên tài khoản của bạn" {...field} />
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )}
                />
                <FormField
                  control={form.control}
                  name="email"
                  render={({ field }) => (
                    <FormItem className="col-span-2 md:col-span-1">
                      <FormLabel>Email</FormLabel>
                      <FormControl>
                        <Input placeholder="example@gmail.com" {...field} />
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )}
                />
                <FormField
                  control={form.control}
                  name="password"
                  render={({ field }) => (
                    <FormItem className="col-span-2">
                      <FormLabel>Mật khẩu</FormLabel>
                      <FormControl>
                        <Input type="password" placeholder="Nhập mật khẩu của bạn" {...field} />
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )}
                />
                <FormField
                  control={form.control}
                  name="confirmPassword"
                  render={({ field }) => (
                    <FormItem className="col-span-2">
                      <FormLabel>Xác nhận mật khẩu</FormLabel>
                      <FormControl>
                        <Input type="password" placeholder="Xác nhận mật khẩu của bạn" {...field} />
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )}
                />
              </div>
            </CardContent>
            <CardFooter className="flex justify-end">
              <Button className="w-full" type="submit" disabled={loadingSubmitForm}>
                {loadingSubmitForm ? (
                  <>
                    <Loader2 className="animate-spin" />
                    {'Đang đăng ký...'}
                  </>
                ) : (
                  'Đăng ký'
                )}
              </Button>
            </CardFooter>
          </Card>
        </form>
      </Form>
    </>
  );
};

export default SignUpForm;
