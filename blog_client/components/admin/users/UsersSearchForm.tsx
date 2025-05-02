import { ListUserRequest } from '@/types/user';
import { ListTypeResponse } from '@/types/type';
import { FilterOutlined, SearchOutlined } from '@ant-design/icons';
import { Button, Form, FormProps, Input, Select } from 'antd';
import toast from 'react-hot-toast';

interface UsersSearchFormProps {
  onSearch: (values: ListUserRequest) => void;
  roles: ListTypeResponse[];
}

const UsersSearchForm: React.FC<UsersSearchFormProps> = ({ onSearch, roles }) => {
  const [form] = Form.useForm();

  const onFinish: FormProps<ListUserRequest>['onFinish'] = (values) => {
    onSearch(values);
  };

  const onFinishFailed: FormProps<ListUserRequest>['onFinishFailed'] = (errorInfo) => {
    toast.error(`Lỗi khi tìm kiếm người dùng ${errorInfo.errorFields[0].errors[0]}`);
  };

  const handleReset = () => {
    form.resetFields();
    onSearch({
      pageNumber: 1,
      pageSize: 10,
    });
  };

  const handleKeyDown = (e: React.KeyboardEvent<HTMLInputElement>) => {
    if (e.key === 'Enter') {
      form.submit();
    }
  };

  return (
    <div className="flex flex-col gap-4">
      <Form
        form={form}
        layout="inline"
        name="search"
        initialValues={{ remember: true }}
        onFinish={onFinish}
        onFinishFailed={onFinishFailed}
        autoComplete="off"
        className="flex flex-wrap gap-y-4"
      >
        <Form.Item<ListUserRequest> name="username">
          <Input
            allowClear
            size="large"
            placeholder="Tên người dùng"
            style={{ width: 200 }}
            onKeyDown={handleKeyDown}
          />
        </Form.Item>

        <Form.Item<ListUserRequest> name="email">
          <Input
            allowClear
            size="large"
            placeholder="Email"
            style={{ width: 200 }}
            onKeyDown={handleKeyDown}
          />
        </Form.Item>

        <Form.Item<ListUserRequest> name="fullName">
          <Input
            allowClear
            size="large"
            placeholder="Họ tên"
            style={{ width: 200 }}
            onKeyDown={handleKeyDown}
          />
        </Form.Item>

        <Form.Item<ListUserRequest> name="address">
          <Input
            allowClear
            size="large"
            placeholder="Địa chỉ"
            style={{ width: 200 }}
            onKeyDown={handleKeyDown}
          />
        </Form.Item>

        <Form.Item<ListUserRequest> name="phone">
          <Input
            allowClear
            size="large"
            placeholder="Số điện thoại"
            style={{ width: 200 }}
            onKeyDown={handleKeyDown}
          />
        </Form.Item>

        <Form.Item<ListUserRequest> name="status">
          <Select
            allowClear
            size="large"
            placeholder="Trạng thái"
            style={{ width: 200 }}
            options={[
              { value: 1, label: 'Hoạt động' },
              { value: 0, label: 'Không hoạt động' },
            ]}
          />
        </Form.Item>

        <Form.Item<ListUserRequest> name="roles">
          <Select
            size="large"
            mode="multiple"
            allowClear
            style={{ width: 250 }}
            placeholder="Vai trò"
            options={roles.map((role) => ({
              label: role.name,
              value: role.value,
            }))}
          />
        </Form.Item>
      </Form>

      <div className="flex justify-start gap-2">
        <Button icon={<SearchOutlined />} type="primary" onClick={() => form.submit()}>
          Tìm kiếm
        </Button>

        <Button icon={<FilterOutlined />} type="primary" onClick={handleReset}>
          Bỏ lọc
        </Button>
      </div>
    </div>
  );
};

export default UsersSearchForm;
