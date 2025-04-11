'use client';

import { AppSidebar } from '@/components/layouts/AppSideBar';
import { Button } from '@/components/ui/button';
import { SidebarProvider, SidebarTrigger, useSidebar } from '@/components/ui/sidebar';
import { Tooltip, TooltipContent, TooltipProvider, TooltipTrigger } from '@/components/ui/tooltip';
import { AntdRegistry } from '@ant-design/nextjs-registry';
import '@ant-design/v5-patch-for-react-19';
import { Alert, ConfigProvider } from 'antd';
import 'antd/dist/reset.css';
import { House } from 'lucide-react';
import { Geist, Geist_Mono } from 'next/font/google';
import { Toaster } from 'react-hot-toast';
import '../globals.css';
import { Providers } from '../providers';
import { useRouter } from 'next/navigation';
const geistSans = Geist({
  variable: '--font-geist-sans',
  subsets: ['latin'],
});

const geistMono = Geist_Mono({
  variable: '--font-geist-mono',
  subsets: ['latin'],
});

export default function AdminRootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body className={`${geistSans.variable} ${geistMono.variable} antialiased`}>
        <AntdRegistry>
          <ConfigProvider>
            <Providers>
              <TooltipProvider>
                <SidebarProvider>
                  <AdminMainContent>{children}</AdminMainContent>
                </SidebarProvider>
              </TooltipProvider>
            </Providers>
            <Toaster position="top-right" />
          </ConfigProvider>
        </AntdRegistry>
      </body>
    </html>
  );
}

const AdminMainContent = ({ children }: { children: React.ReactNode }) => {
  const router = useRouter();
  const { open, isMobile } = useSidebar();

  return (
    <>
      <AppSidebar />
      {isMobile ? (
        <main className="w-full p-2">
          <SidebarTrigger />
          <div className="mt-3">
            <Alert
              message="Bạn nên sử dụng máy tính, laptop có độ phân giải 800px trở lên để truy cập trang quản trị"
              type="warning"
              showIcon
              closable
            />
          </div>
          {children}
        </main>
      ) : (
        <main
          style={
            open
              ? { width: 'calc(100% - 256px)', transition: 'width 0.3s ease-in-out' }
              : { width: '100%' }
          }
          className="p-2"
        >
          <div className="flex gap-1">
            <SidebarTrigger />

            <Tooltip>
              <TooltipTrigger asChild>
                <Button
                  className="size-7"
                  variant="outline"
                  size="icon"
                  onClick={() => router.push('/')}
                >
                  <House />
                </Button>
              </TooltipTrigger>
              <TooltipContent>
                <span>Trở về trang chủ</span>
              </TooltipContent>
            </Tooltip>
          </div>
          {children}
        </main>
      )}
    </>
  );
};
