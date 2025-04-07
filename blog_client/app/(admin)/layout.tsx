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
      <head>
        <script src="https://accounts.google.com/gsi/client" async defer></script>
      </head>
      <body className={`${geistSans.variable} ${geistMono.variable} antialiased`}>
        <AntdRegistry>
          <ConfigProvider>
            <Providers>
              <SidebarProvider>
                <AppSidebar />
                <main
                  style={{
                    width: '100%',
                    height: '100%',
                    marginLeft: '8px',
                    marginRight: '8px',
                    marginTop: '8px',
                  }}
                >
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
