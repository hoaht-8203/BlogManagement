import { useAuth } from '@/apis/useAuth';
import { Avatar, AvatarImage } from '@/components/ui/avatar';
import { Button } from '@/components/ui/button';
import { Card, CardContent, CardDescription, CardTitle } from '@/components/ui/card';
import { Tabs, TabsContent, TabsList, TabsTrigger } from '@/components/ui/tabs';
import { Edit, Lock, UserCircle2, ImageIcon, CreditCard } from 'lucide-react';
import UpdateInfoTab from './UpdateInfoTab';
import ChangPasswordTab from './ChangPasswordTab';
import ChangeAvatarTab from './ChangeAvatarTab';
import UpgradeAccountTab from './UpgradeAccountTab';
import { Separator } from '@/components/ui/separator';

const MyProfile = () => {
  const { user } = useAuth();

  return (
    <div className="grid grid-cols-12 gap-5">
      <div className="col-span-12 md:col-span-3">
        <Card className="w-full py-3">
          <CardContent className="flex flex-col items-center justify-center px-3">
            <Avatar className="size-[100px]">
              <AvatarImage src="https://cdn3.iconfinder.com/data/icons/avatars-flat/33/man_5-512.png" />
            </Avatar>
            <CardTitle className="mt-5">{user?.username}</CardTitle>
            <CardDescription className="mt-2">{user?.email}</CardDescription>

            <Button variant="outline" className="mt-5 w-full">
              <UserCircle2 /> Trang cá nhân
            </Button>
          </CardContent>
        </Card>
      </div>

      <Card className="col-span-12 py-3 md:col-span-9">
        <CardContent className="px-3">
          <Tabs defaultValue="update-info">
            <TabsList>
              <TabsTrigger value="update-info">
                <Edit /> Cập nhật thông tin
              </TabsTrigger>
              <TabsTrigger value="change-password">
                <Lock /> Đổi mật khẩu
              </TabsTrigger>
              <TabsTrigger value="change-avatar">
                <ImageIcon /> Đổi ảnh đại diện
              </TabsTrigger>
              <TabsTrigger value="upgrade-account">
                <CreditCard /> Nâng cấp tài khoản
              </TabsTrigger>
            </TabsList>

            <Separator className="mt-1 mb-2" />

            <TabsContent value="update-info">
              <UpdateInfoTab />
            </TabsContent>
            <TabsContent value="change-password">
              <ChangPasswordTab />
            </TabsContent>
            <TabsContent value="change-avatar">
              <ChangeAvatarTab />
            </TabsContent>
            <TabsContent value="upgrade-account">
              <UpgradeAccountTab />
            </TabsContent>
          </Tabs>
        </CardContent>
      </Card>
    </div>
  );
};

export default MyProfile;
