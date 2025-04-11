import { Home, Inbox } from 'lucide-react';

export const AdminNavbarItems = [
  {
    title: 'Dashboard',
    url: '/admin/dashboard',
    icon: Home,
  },
  {
    title: 'User Management',
    url: '/admin/users',
    icon: Inbox,
  },
  {
    title: 'Back to home',
    url: '/',
    icon: Inbox,
  },
];
