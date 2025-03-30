import { zodResolver } from '@hookform/resolvers/zod';
import { z } from 'zod';
import { useForm } from 'react-hook-form';
import { Form, FormControl, FormField, FormItem, FormLabel, FormMessage } from '../ui/form';
import { Input } from '../ui/input';
import { Button } from '../ui/button';
import { ImageIcon } from 'lucide-react';
import { useState } from 'react';

const formSchema = z.object({
  avatar: z.any(),
});

const ChangeAvatarTab = () => {
  const [previewUrl, setPreviewUrl] = useState<string>('');

  const form = useForm<z.infer<typeof formSchema>>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      avatar: null,
    },
  });

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const file = e.target.files?.[0];
    if (file) {
      form.setValue('avatar', file);
      const url = URL.createObjectURL(file);
      setPreviewUrl(url);
    }
  };

  function onSubmit(values: z.infer<typeof formSchema>) {
    console.log(values);
  }

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-5 md:w-[450px]">
        <FormField
          control={form.control}
          name="avatar"
          render={({ field: { onChange, value, ...field } }) => (
            <FormItem>
              <FormLabel>Ảnh đại diện</FormLabel>
              <FormControl>
                <Input
                  type="file"
                  accept="image/*"
                  onChange={(e) => {
                    handleFileChange(e);
                    onChange(e.target.files?.[0]);
                  }}
                  {...field}
                />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />

        {previewUrl && (
          <div className="relative h-[100px] w-[100px] overflow-hidden rounded-full">
            <img src={previewUrl} alt="avatar" className="h-full w-full object-cover" />
          </div>
        )}

        <Button variant="outline" type="submit">
          <ImageIcon className="mr-2" /> Thay đổi ảnh đại diện
        </Button>
      </form>
    </Form>
  );
};

export default ChangeAvatarTab;
