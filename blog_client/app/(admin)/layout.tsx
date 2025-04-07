'use client';

import { AntdRegistry } from '@ant-design/nextjs-registry';
import '@ant-design/v5-patch-for-react-19';
import { ConfigProvider } from 'antd';
import 'antd/dist/reset.css';
import { Geist, Geist_Mono } from 'next/font/google';
import { Toaster } from 'react-hot-toast';
import '../globals.css';
import { Providers } from '../providers';
import { SidebarProvider, SidebarTrigger } from '@/components/ui/sidebar';
import { AppSidebar } from '@/components/layouts/AppSideBar';

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
              <SidebarProvider>
                <AppSidebar />
                <main className="mx-2 mt-2 w-full">
                  <SidebarTrigger />
                  {children}
                </main>
              </SidebarProvider>
            </Providers>
            <Toaster position="top-right" />
          </ConfigProvider>
        </AntdRegistry>
      </body>
    </html>
  );
}
