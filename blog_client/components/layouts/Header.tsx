'use client';

import React from 'react';
import { Button } from '../ui/button';
import Link from 'next/link';
import { useRouter } from 'next/navigation';
import { useAuth } from '@/apis/useAuth';

const Header = () => {
  const router = useRouter();
  const { user, logout } = useAuth();

  const handleLogout = () => {
    logout();
  };

  return (
    <>
      <header className="sticky top-0 z-50 w-full flex-none border-b border-neutral-200 bg-white/75 backdrop-blur dark:border-neutral-800 dark:bg-neutral-900/75">
        <div className="mx-auto max-w-(--ui-container) px-4 sm:px-6 lg:px-8">
          <div className="flex h-14 items-center justify-between gap-3">
            <div>
              <Link className="cursor-pointer text-2xl font-bold text-blue-500" href={'/'}>
                OurBlog
              </Link>
            </div>

            {user !== null && (
              <>
                {user.username} is logging <Button onClick={handleLogout}>Logout</Button>
              </>
            )}

            {user === null && (
              <div className="space-x-2">
                <Button variant="default" onClick={() => router.push('/login')}>
                  Login
                </Button>
                <Button variant="outline" onClick={() => router.push('/sign-up')}>
                  Sign Up
                </Button>
              </div>
            )}
          </div>
        </div>
      </header>
    </>
  );
};

export default Header;
