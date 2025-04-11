import { Gauge, Inbox, Users } from 'lucide-react';

export const AdminNavbarItems = [
  {
    title: 'Bảng điều khiển',
    url: '/admin/dashboard',
    icon: Gauge,
  },
  {
    title: 'Quản lý người dùng',
    url: '/admin/users',
    icon: Users,
  },
  {
    title: 'Trang chủ',
    url: '/',
    icon: Inbox,
  },
];
