'use client';

import { useRouter } from 'next/navigation';
import React from 'react';
import { Button } from '../ui/button';

const Header = () => {
  const router = useRouter();

  return (
    <>
      <header className="sticky top-0 z-50 w-full flex-none border-b border-neutral-200 bg-white/75 backdrop-blur dark:border-neutral-800 dark:bg-neutral-900/75">
        <div className="mx-auto max-w-(--ui-container) px-4 sm:px-6 lg:px-8">
          <div className="flex h-14 items-center justify-between gap-3">
            <div>
              <h1 className="cursor-pointer text-2xl font-bold" onClick={() => router.push('/')}>
                OurBlog
              </h1>
            </div>

            <div>
              <Button variant="outline" onClick={() => router.push('/sign-up')}>
                Sign Up
              </Button>
            </div>
          </div>
        </div>
      </header>
    </>
  );
};

export default Header;
