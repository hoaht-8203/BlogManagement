import type { Metadata } from 'next';
import { Geist, Geist_Mono } from 'next/font/google';
import './globals.css';
import Header from '@/components/layouts/Header';

const geistSans = Geist({
  variable: '--font-geist-sans',
  subsets: ['latin'],
});

const geistMono = Geist_Mono({
  variable: '--font-geist-mono',
  subsets: ['latin'],
});

export const metadata: Metadata = {
  title: 'Blog management app',
  description: 'Blog management app',
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body className={`${geistSans.variable} ${geistMono.variable} antialiased`}>
        <div className="min-h-screen">
          <div className="flex flex-col">
            <Header />
            <div className="sm:min-h-[calc(100vh-360px)]">
              <div className="mx-auto flex max-w-(--ui-container) flex-col gap-10 px-4 sm:px-6 lg:px-8">
                {children}
              </div>
            </div>
          </div>
        </div>
      </body>
    </html>
  );
}
