# OurBlog - Blog Management System

OurBlog là một hệ thống quản lý blog hiện đại, được xây dựng bằng ASP.NET Core cho backend và Next.js cho frontend. Hệ thống hỗ trợ đăng ký, đăng nhập, quản lý bài viết và tương tác với người dùng.

## Tính năng chính

- 🔐 Xác thực và phân quyền người dùng

  - Đăng ký/đăng nhập thông thường
  - Đăng nhập bằng Google
  - JWT Authentication
  - Role-based Authorization

- 📧 Gửi email tự động

  - Email chào mừng khi đăng ký
  - Email chứa mật khẩu cho tài khoản Google
  - Email đặt lại mật khẩu

- 📝 Quản lý bài viết

  - Tạo, chỉnh sửa, xóa bài viết
  - Phân loại bài viết theo danh mục
  - Tìm kiếm và lọc bài viết

- 👥 Quản lý người dùng
  - Phân quyền người dùng (Admin, User)
  - Quản lý thông tin cá nhân
  - Đổi mật khẩu

## Công nghệ sử dụng

### Backend

- ASP.NET Core 8.0
- Entity Framework Core
- PostgreSQL
- JWT Authentication
- MailKit cho gửi email
- AutoMapper
- Swagger/OpenAPI

### Frontend

- Next.js 14
- TypeScript
- Tailwind CSS
- React Query
- React Hook Form
- Zod validation

## Cài đặt và chạy

### Yêu cầu hệ thống

- .NET 8.0 SDK
- Node.js 18+
- PostgreSQL
- Git

### Backend

1. Clone repository:

```bash
git clone https://github.com/yourusername/BlogManagement.git
cd BlogManagement/blog_server
```

2. Cài đặt dependencies:

```bash
dotnet restore
```

3. Cập nhật connection string trong `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DbConnection": "Host=localhost;Username=your_username;Password=your_password;Database=blog_db"
  }
}
```

4. Chạy migrations:

```bash
dotnet ef database update
```

5. Chạy ứng dụng:

```bash
dotnet run
```

### Frontend

1. Di chuyển vào thư mục frontend:

```bash
cd ../blog_client
```

2. Cài đặt dependencies:

```bash
npm install
```

3. Tạo file `.env.local`:

```env
NEXT_PUBLIC_API_URL=http://localhost:5010
NEXT_PUBLIC_GOOGLE_CLIENT_ID=your_google_client_id
```

4. Chạy ứng dụng:

```bash
npm run dev
```

## Cấu hình Email

1. Tạo tài khoản Gmail
2. Bật 2-Step Verification
3. Tạo App Password
4. Cập nhật cấu hình trong `appsettings.json`:

```json
{
  "Email": {
    "SmtpServer": "smtp.gmail.com",
    "SmtpPort": 587,
    "SmtpUsername": "your_email@gmail.com",
    "SmtpPassword": "your_app_password",
    "FromEmail": "your_email@gmail.com",
    "FromName": "OurBlog"
  }
}
```

## Cấu hình Google OAuth

1. Tạo project trên Google Cloud Console
2. Bật Google Sign-In API
3. Tạo OAuth 2.0 credentials
4. Thêm authorized origins và redirect URIs
5. Cập nhật cấu hình trong `appsettings.json`:

```json
{
  "GoogleAuth": {
    "ClientId": "your_client_id",
    "ClientSecret": "your_client_secret"
  }
}
```

## API Documentation

API documentation có sẵn tại `/swagger` khi chạy backend server.

## Contributing

1. Fork repository
2. Tạo branch mới (`git checkout -b feature/AmazingFeature`)
3. Commit changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to branch (`git push origin feature/AmazingFeature`)
5. Tạo Pull Request

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Contact

Your Name - [@yourtwitter](https://twitter.com/yourtwitter) - email@example.com

Project Link: [https://github.com/yourusername/BlogManagement](https://github.com/yourusername/BlogManagement)
