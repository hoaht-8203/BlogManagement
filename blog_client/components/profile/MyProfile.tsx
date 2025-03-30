import { useAuth } from '@/apis/useAuth';
import { Avatar, AvatarImage } from '@/components/ui/avatar';
import { Button } from '@/components/ui/button';
import { Card, CardContent, CardDescription, CardTitle } from '@/components/ui/card';
import { Tabs, TabsContent, TabsList, TabsTrigger } from '@/components/ui/tabs';
import { Edit, Lock, UserCircle2, ImageIcon, CreditCard, Crown, Clock } from 'lucide-react';
import UpdateInfoTab from './UpdateInfoTab';
import ChangPasswordTab from './ChangPasswordTab';
import ChangeAvatarTab from './ChangeAvatarTab';
import UpgradeAccountTab from './UpgradeAccountTab';
import { Separator } from '@/components/ui/separator';
import { Badge } from '@/components/ui/badge';

const MyProfile = () => {
  const { user } = useAuth();
  const accountType = 'enterprise' as 'basic' | 'pro' | 'enterprise';

  return (
    <div className="grid grid-cols-12 gap-5">
      <div className="col-span-12 md:col-span-3">
        <Card
          className={`relative w-full py-3 ${
            accountType === 'basic'
              ? 'bg-basic'
              : accountType === 'pro'
                ? 'bg-pro'
                : 'bg-enterprise'
          }`}
        >
          <CardContent className="flex flex-col items-center justify-center px-3">
            <div className="relative">
              <div className="relative">
                <Avatar
                  className={`size-[100px] ring-4 ring-offset-2 transition-all ${
                    accountType === 'basic'
                      ? 'ring-gray-200'
                      : accountType === 'pro'
                        ? 'shadow-lg shadow-blue-500/10 ring-blue-500/30'
                        : 'shadow-lg shadow-purple-500/10 ring-purple-500/30'
                  }`}
                >
                  <AvatarImage src="https://cdn3.iconfinder.com/data/icons/avatars-flat/33/man_5-512.png" />
                </Avatar>
              </div>
            </div>

            <CardTitle className="mt-5">{user?.username}</CardTitle>
            <CardDescription className="mt-2">{user?.email}</CardDescription>

            <Badge
              variant="default"
              className={`mt-3 flex items-center gap-1 ${
                accountType === 'basic'
                  ? 'bg-gray-500/80'
                  : accountType === 'pro'
                    ? 'bg-gradient-to-r from-blue-600 to-blue-700'
                    : 'bg-gradient-to-r from-purple-600 to-purple-700'
              }`}
            >
              <Crown className="h-3 w-3" />
              <span>
                {accountType === 'basic'
                  ? 'Tài khoản thường'
                  : accountType === 'pro'
                    ? 'Tài khoản Pro'
                    : 'Tài khoản Enterprise'}
              </span>
            </Badge>

            <div className="text-muted-foreground mt-2 flex items-center gap-1 text-sm">
              <Clock className="h-3 w-3" />
              <span>Hết hạn: 31/12/2024</span>
            </div>

            <Button
              variant="outline"
              className={`mt-5 w-full ${
                accountType === 'basic'
                  ? 'hover:bg-gray-100'
                  : accountType === 'pro'
                    ? 'border-blue-200 hover:bg-blue-50'
                    : 'border-purple-200 hover:bg-purple-50'
              }`}
            >
              <UserCircle2 className="mr-2" /> Trang cá nhân
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
