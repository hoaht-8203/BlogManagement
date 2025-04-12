'use client';

import { VerifyTokenForm } from '@/components/auth/VerifyTokenForm';
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card';
import { useSearchParams } from 'next/navigation';

export default function VerifyEmailPage() {
  const searchParams = useSearchParams();
  const email = searchParams.get('email');

  if (!email) {
    return (
      <div className="mt-5 flex justify-center">
        <Card className="w-[450px]">
          <CardHeader>
            <CardTitle>Lỗi</CardTitle>
            <CardDescription>Không tìm thấy email để xác thực</CardDescription>
          </CardHeader>
        </Card>
      </div>
    );
  }

  return (
    <div className="mt-5 flex justify-center">
      <Card className="w-[450px]">
        <CardHeader>
          <CardTitle>Xác thực email</CardTitle>
          <CardDescription>
            Vui lòng nhập mã xác thực đã được gửi đến email của bạn để hoàn tất quá trình đăng ký
          </CardDescription>
        </CardHeader>
        <CardContent>
          <VerifyTokenForm email={email} />
        </CardContent>
      </Card>
    </div>
  );
}
