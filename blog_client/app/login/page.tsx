'use client';

import LoginForm from '@/components/auth/LoginForm';
import SignUpForm from '@/components/auth/SignUpForm';
import React from 'react';

const LoginPage = () => {
  return (
    <div className="mt-5 flex justify-center">
      <LoginForm />
    </div>
  );
};

export default LoginPage;
