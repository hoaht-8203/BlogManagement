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
import { zodResolver } from '@hookform/resolvers/zod';
import { Loader2 } from 'lucide-react';
import { useForm } from 'react-hook-form';
import { z } from 'zod';
const formSchema = z.object({
  token: z.string().length(6, 'Mã xác thực phải có 6 chữ số'),
});

interface VerifyTokenFormProps {
  email: string;
}

export function VerifyTokenForm({ email }: VerifyTokenFormProps) {
  const { verifyResetToken } = useAuth();
  const form = useForm<{ token: string }>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      token: '',
    },
  });

  const onSubmit = (values: { token: string }) => {
    verifyResetToken.mutate({
      email,
      token: values.token,
    });
  };

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-4">
        <FormField
          control={form.control}
          name="token"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Mã xác thực</FormLabel>
              <FormControl>
                <Input placeholder="Nhập mã 6 chữ số" maxLength={6} {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <Button type="submit" className="w-full" disabled={verifyResetToken.isPending}>
          {verifyResetToken.isPending ? (
            <Loader2 className="mr-2 h-4 w-4 animate-spin" />
          ) : (
            'Xác thực'
          )}
        </Button>
      </form>
    </Form>
  );
}
