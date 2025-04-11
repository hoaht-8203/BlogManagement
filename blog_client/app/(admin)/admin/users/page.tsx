'use client';

import { userService } from '@/services/user.service';
import { QUERY_KEYS } from '@/types/api_key';
import { ListUserResponse } from '@/types/user';
import { ReloadOutlined } from '@ant-design/icons';
import { useQuery } from '@tanstack/react-query';
import { Breadcrumb, Button, Table, TableProps, Tag, Tooltip } from 'antd';

export const columns: TableProps<ListUserResponse>['columns'] = [
  {
    title: 'STT',
    dataIndex: 'index',
    key: 'index',
    render(value, record, index) {
      return <span>{index + 1}</span>;
    },
    width: 40,
    align: 'center',
    fixed: 'left',
  },
  {
    title: 'Tên người dùng',
    dataIndex: 'username',
    key: 'username',
    width: 130,
    fixed: 'left',
  },
  {
    title: 'Email',
    dataIndex: 'email',
    key: 'email',
    width: 230,
  },
  {
    title: 'Họ và tên',
    dataIndex: 'fullName',
    key: 'fullname',
    render(text) {
      return <span>{text ? text : '-'}</span>;
    },
    width: 200,
  },
  {
    title: 'Địa chỉ',
    dataIndex: 'address',
    key: 'address',
    render(text) {
      return <span>{text ? text : '-'}</span>;
    },
    width: 200,
  },
  {
    title: 'Số điện thoại',
    dataIndex: 'phone',
    key: 'phone',
    render(text) {
      return <span>{text ? text : '-'}</span>;
    },
    width: 120,
  },
  {
    title: 'Trạng thái email',
    dataIndex: 'isEmailVerified',
    key: 'isEmailVerified',
    render(text) {
      return <Tag color={text ? 'green' : 'red'}>{text ? 'Đã xác thực' : 'Chưa xác thực'}</Tag>;
    },
    width: 130,
  },
  {
    title: 'Vai trò',
    key: 'roles',
    dataIndex: 'roles',
    render: (_, { roles }) => (
      <>
        {roles.map((role) => {
          return <Tag key={role}>role</Tag>;
        })}
      </>
    ),
    width: 120,
  },
  {
    title: 'Ngày tạo',
    dataIndex: 'createDate',
    key: 'createDate',
    render: (text) => {
      return <span>{new Date(text).toLocaleString()}</span>;
    },
    width: 170,
  },
  {
    title: 'Ngày cập nhật',
    dataIndex: 'updateDate',
    key: 'updateDate',
    render: (text) => {
      return <span>{new Date(text).toLocaleString()}</span>;
    },
    width: 170,
  },
  {
    title: 'Hành động',
    key: 'action',
    width: 90,
    fixed: 'right',
    align: 'center',
  },
];

const UserManagementPage = () => {
  const {
    data: users,
    isFetching: isLoading,
    refetch,
  } = useQuery({
    queryKey: [QUERY_KEYS.MANAGE_USER.LIST],
    queryFn: userService.listUser,
  });

  return (
    <div>
      <div className="my-3">
        <Breadcrumb
          items={[
            {
              title: 'Quản lý người dùng',
            },
            {
              title: 'Danh sách người dùng',
            },
          ]}
        />
      </div>

      <div className="my-3 flex items-end justify-between">
        <div>
          <Tag color="#108ee9">Tổng: {users?.data?.length ?? 0} người dùng</Tag>
        </div>
        <div>
          <Tooltip placement="leftBottom" title={'Refresh'}>
            <Button type="primary" icon={<ReloadOutlined />} onClick={() => refetch()} />
          </Tooltip>
        </div>
      </div>

      <Table<ListUserResponse>
        columns={columns}
        dataSource={users?.data || undefined}
        bordered
        size="small"
        loading={isLoading}
        rowKey={(record) => record.id}
        scroll={{ x: 'max-content' }}
      />
    </div>
  );
};

export default UserManagementPage;
