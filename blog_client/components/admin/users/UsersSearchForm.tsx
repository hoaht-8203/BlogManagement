import { ListUserRequest } from '@/types/user';
import { SearchOutlined } from '@ant-design/icons';
import { Button, Form, FormProps, Input, Select } from 'antd';
import toast from 'react-hot-toast';

interface UsersSearchFormProps {
  onSearch: (values: ListUserRequest) => void;
}

const UsersSearchForm: React.FC<UsersSearchFormProps> = ({ onSearch }) => {
  const [form] = Form.useForm();

  const onFinish: FormProps<ListUserRequest>['onFinish'] = (values) => {
    onSearch(values);
  };

  const onFinishFailed: FormProps<ListUserRequest>['onFinishFailed'] = (errorInfo) => {
    toast.error(`Lỗi khi tìm kiếm người dùng ${errorInfo.errorFields[0].errors[0]}`);
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
          <Input allowClear size="large" placeholder="Tên người dùng" style={{ width: 200 }} />
        </Form.Item>

        <Form.Item<ListUserRequest> name="email">
          <Input allowClear size="large" placeholder="Email" style={{ width: 200 }} />
        </Form.Item>

        <Form.Item<ListUserRequest> name="fullName">
          <Input allowClear size="large" placeholder="Họ tên" style={{ width: 200 }} />
        </Form.Item>

        <Form.Item<ListUserRequest> name="address">
          <Input allowClear size="large" placeholder="Địa chỉ" style={{ width: 200 }} />
        </Form.Item>

        <Form.Item<ListUserRequest> name="phone">
          <Input allowClear size="large" placeholder="Số điện thoại" style={{ width: 200 }} />
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

        <Form.Item<ListUserRequest> name="status">
          <Select
            size="large"
            mode="multiple"
            allowClear
            style={{ width: 250 }}
            placeholder="Vai trò"
            options={[
              {
                label: 'Admin',
                value: 'ADMIN',
              },
              {
                label: 'User',
                value: 'USER',
              },
            ]}
          />
        </Form.Item>
      </Form>

      <div className="flex justify-start">
        <Button icon={<SearchOutlined />} type="primary" onClick={() => form.submit()}>
          Tìm kiếm
        </Button>
      </div>
    </div>
  );
};

export default UsersSearchForm;
