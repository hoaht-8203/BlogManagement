import { Gauge, UserRoundCog, Users } from 'lucide-react';

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
    title: 'Quản lý vai trò',
    url: '/admin/roles',
    icon: UserRoundCog,
  },
];
