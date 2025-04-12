'use client';

import UsersSearchForm from '@/components/admin/users/UsersSearchForm';
import { usersTableColumns } from '@/components/admin/users/usersTableColumns';
import { userService } from '@/services/user.service';
import { ApiResponse, PaginatedList } from '@/types/api';
import { QUERY_KEYS } from '@/types/api_key';
import { ListUserRequest, ListUserResponse } from '@/types/user';
import { TableParams } from '@/types/table';
import { ReloadOutlined } from '@ant-design/icons';
import { useQuery } from '@tanstack/react-query';
import { Breadcrumb, Button, Table, TableProps, Tag, Tooltip } from 'antd';
import { useState } from 'react';

const UserManagementPage = () => {
  const [tableParams, setTableParams] = useState<TableParams>({
    pagination: {
      current: 1,
      pageSize: 10,
      total: 0,
    },
  });

  const {
    data: users,
    isFetching: isLoading,
    refetch,
  } = useQuery<ApiResponse<PaginatedList<ListUserResponse>>>({
    queryKey: [
      QUERY_KEYS.MANAGE_USER.LIST,
      tableParams.pagination?.current,
      tableParams.pagination?.pageSize,
    ],
    queryFn: () =>
      userService.listUser({
        pageNumber: tableParams.pagination?.current || 1,
        pageSize: tableParams.pagination?.pageSize || 10,
      }),
  });

  const handleTableChange: TableProps<ListUserResponse>['onChange'] = (pagination) => {
    setTableParams({
      pagination: {
        ...pagination,
        total: users?.data?.totalRecords || 0,
      },
    });
  };

  const handleSearch = (values: ListUserRequest) => {
    console.log(values);
  };

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
          <UsersSearchForm onSearch={handleSearch} />
        </div>
        <div>
          <Tooltip placement="leftBottom" title={'Làm mới'}>
            <Button
              disabled={isLoading}
              type="primary"
              icon={<ReloadOutlined />}
              onClick={() => refetch()}
            />
          </Tooltip>
        </div>
      </div>

      <Table<ListUserResponse>
        columns={usersTableColumns(
          tableParams.pagination?.current || 1,
          tableParams.pagination?.pageSize || 10,
        )}
        dataSource={users?.data?.items || []}
        bordered
        size="small"
        loading={isLoading}
        rowKey={(record) => record.id}
        scroll={{ x: 'max-content', y: 'max-content' }}
        pagination={{
          current: tableParams.pagination?.current,
          pageSize: tableParams.pagination?.pageSize,
          total: users?.data?.totalRecords || 0,
          showSizeChanger: true,
          showTotal: (total) => (
            <>
              <Tag color="#108ee9">Tổng: {total ?? 0} người dùng</Tag>
            </>
          ),
        }}
        onChange={handleTableChange}
      />
    </div>
  );
};

export default UserManagementPage;
