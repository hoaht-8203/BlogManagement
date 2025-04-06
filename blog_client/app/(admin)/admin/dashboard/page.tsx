import Link from 'next/link';
import React from 'react';

const AdminDashboard = () => {
  return (
    <div>
      This is admin dashboard <Link href={'/'}>Go to user page</Link>
    </div>
  );
};

export default AdminDashboard;
