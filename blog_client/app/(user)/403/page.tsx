'use client';

import { Button } from '@/components/ui/button';
import { useRouter } from 'next/navigation';

const ForbiddenPage = () => {
  const router = useRouter();

  return (
    <div className="flex h-screen flex-col items-center justify-center">
      <h1 className="text-2xl font-bold text-red-500">403 - Forbidden</h1>
      <p className="text-gray-500">Bạn không có quyền truy cập vào trang này</p>

      <Button className="mt-3" onClick={() => router.push('/')}>
        Trang chủ
      </Button>
    </div>
  );
};

export default ForbiddenPage;
