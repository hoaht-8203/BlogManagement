'use client';

import { Breadcrumb, Table, Tag, TableProps } from 'antd';
import { TableParams } from '@/types/table';
import { useState } from 'react';
import { ListRoleRequest, ListRoleResponse } from '@/types/role';
import { roleService } from '@/services/role.service';
import { useQuery } from '@tanstack/react-query';
import { ApiResponse, PaginatedList } from '@/types/api';
import { QUERY_KEYS } from '@/types/api_key';
import { rolesTableColumns } from '@/components/admin/roles/rolesTableColumns';

const AdminRoles = () => {
  const [tableParams, setTableParams] = useState<TableParams>({
    pagination: {
      current: 1,
      pageSize: 10,
      total: 0,
    },
  });

  const [searchParams] = useState<ListRoleRequest>({
    pageNumber: 1,
    pageSize: 10,
  });

  const { data: roles, isFetching: isLoading } = useQuery<
    ApiResponse<PaginatedList<ListRoleResponse>>
  >({
    queryKey: [
      QUERY_KEYS.MANAGE_ROLE.LIST,
      tableParams.pagination?.current,
      tableParams.pagination?.pageSize,
      searchParams,
    ],
    queryFn: () => roleService.listRole(searchParams),
  });

  const handleTableChange: TableProps<ListRoleResponse>['onChange'] = (pagination) => {
    setTableParams({
      pagination: {
        ...pagination,
        total: roles?.data?.totalRecords || 0,
      },
    });
  };

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

      <div className="grid grid-cols-1 gap-3">
        <div className="col-span-1">
          <Table<ListRoleResponse>
            columns={rolesTableColumns(
              tableParams.pagination?.current || 1,
              tableParams.pagination?.pageSize || 10,
            )}
            dataSource={roles?.data?.items || []}
            bordered
            size="small"
            loading={isLoading}
            rowKey={(record) => record.id}
            scroll={{ x: 'max-content', y: 'max-content' }}
            pagination={{
              current: tableParams.pagination?.current,
              pageSize: tableParams.pagination?.pageSize,
              total: roles?.data?.totalRecords || 0,
              showSizeChanger: true,
              showTotal: (total) => (
                <>
                  <Tag color="#108ee9">Tổng: {total ?? 0} vai trò</Tag>
                </>
              ),
            }}
            onChange={handleTableChange}
          />
        </div>

        <div className="col-span-2">
          <h1>Display list user by role in here</h1>
        </div>
      </div>
    </div>
  );
};

export default AdminRoles;
