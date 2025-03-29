'use client';

import { ForgotPasswordForm } from '@/components/auth/ForgotPasswordForm';
import { VerifyTokenForm } from '@/components/auth/VerifyTokenForm';
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card';
import { useState } from 'react';

type Step = 'request' | 'verify';

export default function ForgotPasswordPage() {
  const [step, setStep] = useState<Step>('request');
  const [email, setEmail] = useState('');

  const handleTokenSent = (email: string) => {
    setEmail(email);
    setStep('verify');
  };

  return (
    <div className="mt-5 flex justify-center">
      <Card className="w-[450px]">
        <CardHeader>
          <CardTitle>{step === 'request' ? 'Quên mật khẩu' : 'Xác thực mã'}</CardTitle>
          <CardDescription>
            {step === 'request'
              ? 'Bạn quên mật khẩu của mình? Đừng lo lắng! Hãy cung cấp cho chúng tôi email bạn sử dụng để đăng ký tài khoản OurBlog. Chúng tôi sẽ gửi cho bạn một mật khẩu mới cho bạn qua email đó.'
              : 'Nhập mã xác thực đã được gửi đến email của bạn'}
          </CardDescription>
        </CardHeader>
        <CardContent>
          {step === 'request' ? (
            <ForgotPasswordForm onTokenSent={handleTokenSent} />
          ) : (
            <VerifyTokenForm email={email} />
          )}
        </CardContent>
      </Card>
    </div>
  );
}
