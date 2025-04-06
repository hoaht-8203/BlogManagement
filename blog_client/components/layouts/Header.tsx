'use client';

import { useAuth } from '@/apis/useAuth';
import { Avatar, AvatarImage } from '@/components/ui/avatar';
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from '@/components/ui/dropdown-menu';
import Link from 'next/link';
import { useRouter } from 'next/navigation';
import { Button } from '../ui/button';
import { useEffect, useState } from 'react';
import { LogOutIcon, MonitorCog, UserCircle2Icon } from 'lucide-react';

const Header = () => {
  const router = useRouter();
  const [tokenCheck, setTokenCheck] = useState(false);
  const { user, logout, isLoadingUser } = useAuth();

  useEffect(() => {
    const tokenCheck = localStorage.getItem('accessToken');
    setTokenCheck(tokenCheck !== null);
  }, [router]);

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

            {tokenCheck && isLoadingUser && (
              <div className="flex items-center space-x-2">
                <span className="text-sm text-gray-500">Authenticating...</span>
              </div>
            )}

            {user === null && !tokenCheck && !isLoadingUser && (
              <div className="space-x-2">
                <Button variant="default" onClick={() => router.push('/login')}>
                  Login
                </Button>
                <Button variant="outline" onClick={() => router.push('/sign-up')}>
                  Sign Up
                </Button>
              </div>
            )}

            {user !== null && !isLoadingUser && (
              <DropdownMenu>
                <DropdownMenuTrigger>
                  <Avatar className="selection:bg-transparent selection:text-inherit">
                    <AvatarImage
                      className="bg-slate-100"
                      src="https://cdn3.iconfinder.com/data/icons/avatars-flat/33/man_5-512.png"
                      alt="@shadcn"
                    />
                  </Avatar>
                </DropdownMenuTrigger>
                <DropdownMenuContent className="mr-5">
                  <DropdownMenuLabel>{user.username}</DropdownMenuLabel>
                  <DropdownMenuSeparator />
                  <DropdownMenuItem onClick={() => router.push('/admin/dashboard')}>
                    <MonitorCog /> Admin Dashboard
                  </DropdownMenuItem>
                  <DropdownMenuItem onClick={() => router.push('/profile')}>
                    <UserCircle2Icon /> Profile
                  </DropdownMenuItem>
                  <DropdownMenuItem onClick={handleLogout}>
                    <LogOutIcon /> Logout
                  </DropdownMenuItem>
                </DropdownMenuContent>
              </DropdownMenu>
            )}
          </div>
        </div>
      </header>
    </>
  );
};

export default Header;
