import type { Metadata } from 'next';
import { Geist, Geist_Mono } from 'next/font/google';
import { Toaster } from 'react-hot-toast';
import '../globals.css';
import { Providers } from '../providers';

const geistSans = Geist({
  variable: '--font-geist-sans',
  subsets: ['latin'],
});

const geistMono = Geist_Mono({
  variable: '--font-geist-mono',
  subsets: ['latin'],
});

export const metadata: Metadata = {
  title: 'OurBlog - HoaHT',
  description: 'Blog management app',
};

export default function UserRootLayout({
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
        <Providers>
          <div className="min-h-screen">
            <div className="flex flex-col">
              <div className="sm:min-h-[calc(100vh-360px)]">
                <div className="mx-auto flex max-w-(--ui-container) flex-col gap-10 px-4 sm:px-6 lg:px-8">
                  {children}
                </div>
              </div>
            </div>
          </div>
        </Providers>
        <Toaster position="bottom-right" />
      </body>
    </html>
  );
}
