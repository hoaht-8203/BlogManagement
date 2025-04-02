import { zodResolver } from '@hookform/resolvers/zod';
import { ShieldCheck } from 'lucide-react';
import { useForm } from 'react-hook-form';
import { z } from 'zod';
import { Button } from '../ui/button';
import { Form, FormControl, FormField, FormItem, FormLabel, FormMessage } from '../ui/form';
import { Input } from '../ui/input';
import { resetPasswordSchema } from '../../schema/authSchema';
import { useAuth } from '../../apis/useAuth';
import { ApiError } from '../../types/error';

const ChangPasswordTab = () => {
  const { resetPassword } = useAuth();
  const form = useForm<z.infer<typeof resetPasswordSchema>>({
    resolver: zodResolver(resetPasswordSchema),
    defaultValues: {
      oldPassword: '',
      newPassword: '',
      confirmNewPassword: '',
    },
  });

  function onSubmit(values: z.infer<typeof resetPasswordSchema>) {
    resetPassword.mutate(values, {
      onError(error: ApiError) {
        error.errors?.forEach((err) => {
          const [field, message] = err.split(':');
          form.setError(field as 'oldPassword' | 'newPassword' | 'confirmNewPassword', {
            type: 'server',
            message: message,
          });
        });
      },
      onSuccess() {
        form.reset();
      },
    });
  }

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-5 md:w-[450px]">
        <FormField
          control={form.control}
          name="oldPassword"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Mật khẩu cũ</FormLabel>
              <FormControl>
                <Input placeholder="Mật khẩu cũ" {...field} type="password" />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="newPassword"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Mật khẩu mới</FormLabel>
              <FormControl>
                <Input placeholder="Mật khẩu mới" {...field} type="password" />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="confirmNewPassword"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Nhập lại mật khẩu mới</FormLabel>
              <FormControl>
                <Input placeholder="Nhập lại mật khẩu mới" {...field} type="password" />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />

        <Button variant="outline" type="submit">
          <ShieldCheck /> Thay đổi mật khẩu
        </Button>
      </form>
    </Form>
  );
};

export default ChangPasswordTab;
