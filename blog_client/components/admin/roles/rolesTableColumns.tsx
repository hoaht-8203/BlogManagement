import { ListRoleResponse } from '@/types/role';
import { TableColumnsType } from 'antd';

export const rolesTableColumns = (
  currentPage: number,
  pageSize: number,
): TableColumnsType<ListRoleResponse> => [
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
    title: 'Tên vai trò',
    dataIndex: 'name',
    key: 'name',
    width: 100,
  },
  {
    title: 'Mô tả',
    dataIndex: 'description',
    key: 'description',
    width: 230,
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
