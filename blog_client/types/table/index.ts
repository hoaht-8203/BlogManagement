import { TableProps } from 'antd';
import { ListUserResponse } from '@/types/user';

export type TablePaginationConfig = Exclude<TableProps<ListUserResponse>['pagination'], boolean>;

export interface TableParams {
  pagination?: TablePaginationConfig;
}
