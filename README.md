# OurBlog - Blog Management System

OurBlog là một hệ thống quản lý blog hiện đại, được xây dựng bằng ASP.NET Core cho backend và Next.js cho frontend. Hệ thống hỗ trợ đăng ký, đăng nhập, quản lý bài viết và tương tác với người dùng.

<div align="center" style=" border: 1px dashed black; padding: 1rem">

<h3 style="margin: 0">🛠️ Technology Stack</h3>

<h4 style="text-decoration: underline;">Backend</h4>

<div style="display: flex; justify-content: center; gap: 20px; flex-wrap: wrap; margin: 20px 0;">

<img src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQvyg_gJ4zw1fQTH98RxTvYRXfLKuZea76zxg&s" width="85" height="85" alt="ASP.NET Core" title="ASP.NET Core" />
<img src="https://static-00.iconduck.com/assets.00/postgresql-plain-wordmark-icon-2048x2042-up54u54l.png" width="85" height="85" alt="PostgreSQL" title="PostgreSQL" />
<img src="https://cdn4.iconfinder.com/data/icons/redis-2/1451/Untitled-2-512.png" width="85" height="85" alt="Redis" title="Redis" />
<img src="https://img.icons8.com/color/512/java-web-token.png" width="85" height="85" alt="JWT" title="JWT" />
<img src="https://cyclr.com/wp-content/uploads/2022/03/ext-537.png" width="85" height="85" alt="MailKit" title="MailKit" />
<img src="https://static-00.iconduck.com/assets.00/swagger-icon-2048x2048-563qbzey.png" width="85" height="85" alt="Swagger" title="Swagger" />

</div>

<h4 style="text-decoration: underline">Frontend</h4>

<div style="display: flex; justify-content: center; gap: 20px; flex-wrap: wrap; margin: 20px 0">

<img src="https://static-00.iconduck.com/assets.00/next-js-icon-2048x2048-5dqjgeku.png" width="85" height="85" alt="Next.js" title="Next.js" />
<img src="https://static-00.iconduck.com/assets.00/typescript-icon-icon-2048x2048-2rhh1z66.png" width="85" height="85" alt="TypeScript" title="TypeScript" />
<img src="https://www.svgrepo.com/show/374118/tailwind.svg" width="85" height="85" alt="Tailwind CSS" title="Tailwind CSS" />
<img src="https://files.svgcdn.io/logos/react-query-icon.png" width="85" height="85" alt="React Query" title="React Query" />

</div>

</div>

## Công nghệ sử dụng

### Backend

- ASP.NET Core 8.0
- Entity Framework Core
- PostgreSQL
- Redis cho caching
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

## Cài đặt và chạy

### Yêu cầu hệ thống

- .NET 8.0 SDK
- Node.js 18+
- PostgreSQL
- Redis
- Git

### Backend

1. Clone repository:

```bash
git clone https://github.com/hoaht-8203/BlogManagement.git
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
    "DbConnection": "Host=localhost;Username=your_username;Password=your_password;Database=blog_db",
    "RedisConnection": "localhost:6379"
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

## Cấu hình Redis

1. Cài đặt Redis trên máy local hoặc sử dụng Redis Cloud
2. Cập nhật connection string trong `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "RedisConnection": "localhost:6379"
  }
}
```

Redis được sử dụng để cache:

- Thông tin người dùng
- Danh mục bài viết
- Các dữ liệu thường xuyên truy cập

## API Documentation

API documentation có sẵn tại `/swagger` khi chạy backend server.

## Contact

- Email: hoahthe172735@gmail.com - hoaht.dev03@gmail.com
- Facebook: [@hoaht_facebook](https://www.facebook.com/hoanghoa.8203)
- Linkedin: [@hoaht_linkedin](https://www.linkedin.com/in/ho%C3%A0-ho%C3%A0ng-trung-22444b336/)
