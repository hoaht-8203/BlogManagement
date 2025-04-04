/** @type {import('next').NextConfig} */
const nextConfig = {
  allowedDevOrigins: ['192.168.38.109'],
  images: {
    remotePatterns: [
      {
        protocol: 'https',
        hostname: 'encrypted-tbn0.gstatic.com',
      },
      {
        protocol: 'https',
        hostname: 's3.thoainguyentek.com',
      },
    ],
    domains: ['cdn3.iconfinder.com'],
  },
};

module.exports = nextConfig;
