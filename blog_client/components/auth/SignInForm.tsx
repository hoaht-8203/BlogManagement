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
import { ApiError } from '@/types/error';
import GoogleSignInButton from './GoogleSignInButton';

const SignInForm = () => {
  const { login } = useAuth();

  const form = useForm<z.infer<typeof loginSchema>>({
    resolver: zodResolver(loginSchema),
    defaultValues: {
      username: '',
      password: '',
    },
  });

  function onSubmit(values: z.infer<typeof loginSchema>) {
    login.mutate(
      {
        username: values.username,
        password: values.password,
      },
      {
        onError(error: ApiError) {
          console.log(error.errors);
        },
      },
    );
  }

  return (
    <>
      <Form {...form}>
        <form onSubmit={form.handleSubmit(onSubmit)}>
          <Card className="w-[550px]">
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
              </div>
            </CardContent>
            <CardFooter className="flex justify-end">
              <Button type="submit">Submit</Button>
            </CardFooter>
          </Card>
        </form>
      </Form>
    </>
  );
};

export default SignInForm;
