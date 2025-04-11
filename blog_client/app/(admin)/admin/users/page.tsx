'use client';

import { userService } from '@/services/user.service';
import { QUERY_KEYS } from '@/types/api_key';
import { ListUserResponse } from '@/types/user';
import { useQuery } from '@tanstack/react-query';
import { Breadcrumb, Button, Space, Table, TableProps, Tag, Tooltip } from 'antd';
import { ReloadOutlined } from '@ant-design/icons';

const columns: TableProps<ListUserResponse>['columns'] = [
  {
    title: '#',
    dataIndex: 'index',
    key: 'index',
    render(value, record, index) {
      return <span>{index + 1}</span>;
    },
    width: 40,
    align: 'center',
  },
  {
    title: 'Username',
    dataIndex: 'username',
    key: 'username',
    width: 100,
  },
  {
    title: 'Email',
    dataIndex: 'email',
    key: 'email',
    width: 230,
  },
  {
    title: 'Full Name',
    dataIndex: 'fullName',
    key: 'fullname',
    width: 200,
  },
  {
    title: 'Address',
    dataIndex: 'address',
    key: 'address',
    width: 200,
  },
  {
    title: 'Phone Number',
    dataIndex: 'phone',
    key: 'phone',
    width: 120,
  },
  {
    title: 'Email Verified',
    dataIndex: 'isEmailVerified',
    key: 'isEmailVerified',
    render: (text) => {
      return <Tag color={text ? 'green' : 'red'}>{text ? 'Verified' : 'Not Verified'}</Tag>;
    },
    width: 120,
  },
  {
    title: 'Roles',
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
    title: 'Create Date',
    dataIndex: 'createDate',
    key: 'createDate',
    render: (text) => {
      return <span>{new Date(text).toLocaleString()}</span>;
    },
    width: 170,
  },
  {
    title: 'Update Date',
    dataIndex: 'updateDate',
    key: 'updateDate',
    render: (text) => {
      return <span>{new Date(text).toLocaleString()}</span>;
    },
    width: 170,
  },
  {
    title: 'Action',
    key: 'action',
    render: (_, record) => (
      <Space size="middle">
        <a>Invite</a>
        <a>Delete</a>
      </Space>
    ),
    width: 120,
  },
];

const UserManagementPage = () => {
  // const queryClient = useQueryClient();

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
              title: 'User Management',
            },
            {
              title: 'Users',
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
        scroll={{ x: '1500px' }}
      />
    </div>
  );
};

export default UserManagementPage;
