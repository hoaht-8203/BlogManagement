const UnAuthorized = () => {
  return (
    <div className="flex flex-col items-center justify-center">
      <h1 className="text-2xl font-bold">Bạn chưa đăng nhập</h1>
      <p className="text-muted-foreground">Vui lòng đăng nhập để truy cập trang cá nhân</p>
    </div>
  );
};

export default UnAuthorized;
