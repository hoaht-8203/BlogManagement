import { useAuth } from '@/apis/useAuth';
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
import { loginSchema } from '@/schema/authSchema';
import { zodResolver } from '@hookform/resolvers/zod';
import { useRouter } from 'next/navigation';
import { useForm } from 'react-hook-form';
import { z } from 'zod';
import { Card, CardContent, CardDescription, CardFooter, CardHeader, CardTitle } from '../ui/card';

const LoginForm = () => {
  const router = useRouter();
  const { login } = useAuth();

  const form = useForm<z.infer<typeof loginSchema>>({
    resolver: zodResolver(loginSchema),
    defaultValues: {
      username: '',
      password: '',
    },
  });

  function onSubmit(values: z.infer<typeof loginSchema>) {
    login.mutate({
      username: values.username,
      password: values.password,
    });
  }

  return (
    <>
      <Form {...form}>
        <form onSubmit={form.handleSubmit(onSubmit)}>
          <Card className="w-[350px]">
            <CardHeader>
              <CardTitle className="text-center text-2xl font-bold text-blue-500">
                OurBlog
              </CardTitle>
              <CardDescription className="text-center">Đăng nhập vào OurBlog.</CardDescription>
            </CardHeader>
            <CardContent>
              <div className="space-y-5">
                <FormField
                  control={form.control}
                  name="username"
                  render={({ field }) => (
                    <FormItem>
                      <FormLabel>Username hoặc email</FormLabel>
                      <FormControl>
                        <Input placeholder="Enter your username or email" {...field} />
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
                        <Input type="password" placeholder="Enter your password" {...field} />
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )}
                />
              </div>
            </CardContent>
            <CardFooter>
              <div className="flex w-full flex-col space-y-2 text-center">
                <div className="flex justify-end">
                  <Button className="w-full" type="submit">
                    Đăng nhập
                  </Button>
                </div>

                <div className="flex justify-between">
                  <span>
                    <Button type="button" variant={'link'} className="p-0 text-blue-500">
                      Quên mật khẩu?
                    </Button>
                  </span>
                  <span>
                    <Button
                      type="button"
                      variant={'link'}
                      className="p-0 text-blue-500"
                      onClick={() => router.push('/sign-up')}
                    >
                      Tạo tài khoản
                    </Button>
                  </span>
                </div>
              </div>
            </CardFooter>
          </Card>
        </form>
      </Form>
    </>
  );
};

export default LoginForm;
