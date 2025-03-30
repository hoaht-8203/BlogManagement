import React, { useState } from 'react';
import { Button } from '../ui/button';
import { Check } from 'lucide-react';
import Image from 'next/image';

const plans = [
  {
    name: 'Basic',
    price: '0',
    features: [
      'Đăng bài viết cơ bản',
      'Đăng tối đa 5 bài viết/tháng',
      'Hỗ trợ cơ bản',
      'Học khoá học bản Basic',
    ],
    current: true,
  },
  {
    name: 'Pro',
    price: '9.99',
    features: [
      'Tất cả tính năng của Basic',
      'Đăng bài viết không giới hạn',
      'Ưu tiên hỗ trợ',
      'Không có quảng cáo',
      'Học khoá học bản Pro',
    ],
    current: false,
  },
  {
    name: 'Enterprise',
    price: '29.99',
    features: ['Tất cả tính năng của Pro', 'Học khoá học bản Enterprise', 'Hỗ trợ 24/7'],
    current: false,
  },
];

const UpgradeAccountTab = () => {
  const [selectedPlan, setSelectedPlan] = useState<string>('Basic');

  return (
    <div className="space-y-8">
      <div>
        <h2 className="text-2xl font-bold">Nâng cấp tài khoản</h2>
        <p className="text-muted-foreground">Chọn gói phù hợp với nhu cầu của bạn</p>
      </div>

      <div className="grid gap-6 md:grid-cols-3">
        {plans.map((plan) => (
          <div
            key={plan.name}
            onClick={() => setSelectedPlan(plan.name)}
            className={`cursor-pointer rounded-lg border-2 p-6 transition-all hover:border-blue-500 ${
              selectedPlan === plan.name
                ? 'border-blue-500 bg-blue-50/50'
                : 'border-gray-200 hover:bg-gray-50/50'
            }`}
          >
            <div className="space-y-2">
              <h3 className="text-xl font-semibold">{plan.name}</h3>
              <div className="text-3xl font-bold">
                ${plan.price}
                <span className="text-muted-foreground text-sm font-normal">/tháng</span>
              </div>
            </div>

            <ul className="mt-6 space-y-4">
              {plan.features.map((feature) => (
                <li key={feature} className="flex items-center gap-2">
                  <Check className="text-primary h-4 w-4" />
                  <span>{feature}</span>
                </li>
              ))}
            </ul>

            <Button
              className={`mt-6 w-full ${
                selectedPlan === plan.name
                  ? 'bg-blue-500 hover:bg-blue-600'
                  : 'bg-white text-blue-500 hover:bg-blue-50'
              }`}
              variant={selectedPlan === plan.name ? 'default' : 'outline'}
            >
              {selectedPlan === plan.name ? (plan.current ? 'Đã chọn' : 'Thanh toán') : 'Chọn gói'}
            </Button>
          </div>
        ))}
      </div>

      <div className="relative overflow-hidden rounded-lg border bg-gradient-to-r from-blue-50/50 via-white/50 to-blue-50/50 p-8">
        <div className="from-primary/5 to-primary/5 absolute inset-0 bg-gradient-to-r via-transparent" />
        <div className="relative">
          <h3 className="text-xl font-semibold">Thông tin thanh toán</h3>
          <p className="text-muted-foreground mt-2 text-sm">
            Chúng tôi chấp nhận thanh toán qua các phương thức sau:
          </p>
          <div className="mt-6 flex flex-wrap items-center justify-start gap-6">
            <div className="group relative h-16 w-16 rounded-lg border bg-white/80 p-2 shadow-sm transition-all hover:shadow-md">
              <div className="absolute inset-0 rounded-lg bg-gradient-to-r from-blue-500/5 to-transparent opacity-0 transition-opacity group-hover:opacity-100" />
              <Image
                src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ8VNUYiwB1DuoiPYNKl6jXWIcQEOxbNkXM6w&s"
                alt="VNPay"
                fill
                className="object-contain p-2"
              />
            </div>
            <div className="group relative h-16 w-16 rounded-lg border bg-white/80 p-2 shadow-sm transition-all hover:shadow-md">
              <div className="absolute inset-0 rounded-lg bg-gradient-to-r from-blue-400/5 to-transparent opacity-0 transition-opacity group-hover:opacity-100" />
              <Image
                src="https://s3.thoainguyentek.com/2021/11/zalopay-logo.png"
                alt="ZaloPay"
                fill
                className="object-contain p-2"
              />
            </div>
            <div className="group relative h-16 w-16 rounded-lg border bg-white/80 p-2 shadow-sm transition-all hover:shadow-md">
              <div className="absolute inset-0 rounded-lg bg-gradient-to-r from-pink-500/5 to-transparent opacity-0 transition-opacity group-hover:opacity-100" />
              <Image
                src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQZcQPC-zWVyFOu9J2OGl0j2D220D49D0Z7BQ&s"
                alt="Momo"
                fill
                className="object-contain p-2"
              />
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default UpgradeAccountTab;
