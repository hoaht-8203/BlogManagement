'use client';

import { Breadcrumb } from 'antd';
import CommingSoon from '@/components/common/CommingSoon';
const AdminDashboard = () => {
  return (
    <div>
      <div className="my-3">
        <Breadcrumb
          items={[
            {
              title: 'Bảng điều khiển',
            },
          ]}
        />
      </div>

      <CommingSoon />
    </div>
  );
};

export default AdminDashboard;
