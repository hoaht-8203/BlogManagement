import { ListUserResponse } from '@/types/user';
import { TableColumnsType, Tag } from 'antd';

export const usersTableColumns = (
  currentPage: number,
  pageSize: number,
): TableColumnsType<ListUserResponse> => [
  {
    title: 'STT',
    dataIndex: 'index',
    key: 'index',
    render(_value, _record, index) {
      return <span>{(currentPage - 1) * pageSize + index + 1}</span>;
    },
    width: 50,
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
          return role === 'ADMIN' ? (
            <Tag key={role} color="blue">
              {role}
            </Tag>
          ) : (
            <Tag key={role} color="green">
              {role}
            </Tag>
          );
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
    width: 100,
    fixed: 'right',
    align: 'center',
  },
];
