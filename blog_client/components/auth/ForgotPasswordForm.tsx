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
import { ApiError } from '@/types/error';
import { zodResolver } from '@hookform/resolvers/zod';
import { Loader2 } from 'lucide-react';
import { useForm } from 'react-hook-form';
import { z } from 'zod';

const formSchema = z.object({
  email: z.string().email('Email không hợp lệ'),
});

interface ForgotPasswordFormProps {
  onTokenSent: (email: string) => void;
}

export function ForgotPasswordForm({ onTokenSent }: ForgotPasswordFormProps) {
  const { forgotPassword } = useAuth();
  const form = useForm<{ email: string }>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      email: '',
    },
  });

  const onSubmit = async (values: { email: string }) => {
    forgotPassword.mutate(
      {
        email: values.email,
      },
      {
        onError(error: ApiError) {
          error.errors?.forEach((err) => {
            const [field, message] = err.split(':');
            form.setError(field as 'email', {
              type: 'server',
              message: message,
            });
          });
        },
        onSuccess: () => {
          onTokenSent(values.email);
        },
      },
    );
  };

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-4">
        <FormField
          control={form.control}
          name="email"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Email</FormLabel>
              <FormControl>
                <Input placeholder="example@email.com" {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <Button type="submit" className="w-full" disabled={forgotPassword.isPending}>
          {forgotPassword.isPending ? (
            <Loader2 className="mr-2 h-4 w-4 animate-spin" />
          ) : (
            'Gửi mã xác thực'
          )}
        </Button>
      </form>
    </Form>
  );
}
