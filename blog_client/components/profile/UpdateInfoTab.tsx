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
import { updateInfoSchema } from '@/schema/authSchema';
import { MyInfoResponse } from '@/types/auth';
import { zodResolver } from '@hookform/resolvers/zod';
import { CheckCheck } from 'lucide-react';
import { useForm } from 'react-hook-form';
import { z } from 'zod';

const UpdateInfoTab: React.FC<{ user: MyInfoResponse }> = ({ user }) => {
  const { updateInfo } = useAuth();
  const form = useForm<z.infer<typeof updateInfoSchema>>({
    resolver: zodResolver(updateInfoSchema),
    defaultValues: {
      fullName: user.fullName || '',
      username: user.username || '',
      email: user.email || '',
      phone: user.phone || '',
      address: user.address || '',
    },
  });

  function onSubmit(values: z.infer<typeof updateInfoSchema>) {
    updateInfo.mutate({
      fullName: values.fullName?.trim() || '',
      phone: values.phone?.trim() || '',
      address: values.address?.trim() || '',
      avatarUrl: user.avatarUrl.trim(),
    });
  }

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-5 md:w-[450px]">
        <FormField
          control={form.control}
          name="fullName"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Họ và tên</FormLabel>
              <FormControl>
                <Input placeholder="Họ và tên" {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="username"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Tên tài khoản</FormLabel>
              <FormControl>
                <Input placeholder="Tên tài khoản" {...field} disabled />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="email"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Email</FormLabel>
              <FormControl>
                <Input placeholder="Email" {...field} disabled />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="phone"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Số điện thoại</FormLabel>
              <FormControl>
                <Input placeholder="Số điện thoại" {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="address"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Địa chỉ</FormLabel>
              <FormControl>
                <Input placeholder="Địa chỉ" {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />

        <Button variant="outline" type="submit">
          <CheckCheck /> Áp dụng
        </Button>
      </form>
    </Form>
  );
};

export default UpdateInfoTab;
