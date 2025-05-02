'use client';

import { Breadcrumb } from 'antd';
import CommingSoon from '@/components/common/CommingSoon';
const AdminRoles = () => {
  return (
    <div>
      <div className="my-3">
        <Breadcrumb
          items={[
            {
              title: 'Quản lý vai trò',
            },
          ]}
        />
      </div>

      <CommingSoon />
    </div>
  );
};

export default AdminRoles;
