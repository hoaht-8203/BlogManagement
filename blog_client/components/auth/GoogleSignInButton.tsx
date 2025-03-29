'use client';

import { useEffect } from 'react';
import { useAuth } from '@/apis/useAuth';

declare global {
  interface Window {
    google: {
      accounts: {
        id: {
          initialize: (config: {
            client_id: string;
            callback: (response: { credential: string }) => void;
          }) => void;
          renderButton: (
            element: HTMLElement,
            config: {
              theme: string;
              size: string;
              width: string;
            },
          ) => void;
        };
      };
    };
  }
}

const GoogleSignInButton = () => {
  const { googleLogin } = useAuth();

  useEffect(() => {
    const initializeGoogleSignIn = () => {
      if (typeof window.google === 'undefined') return;

      window.google.accounts.id.initialize({
        client_id: process.env.NEXT_PUBLIC_GOOGLE_CLIENT_ID!,
        callback: async (response) => {
          if (response.credential) {
            googleLogin.mutate(
              {
                idToken: response.credential,
              },
              {
                onError(error) {
                  console.error('Google login failed:', error);
                },
              },
            );
          }
        },
      });

      window.google.accounts.id.renderButton(document.getElementById('google-sign-in-button')!, {
        theme: 'outline',
        size: 'large',
        width: '100%',
      });
    };

    initializeGoogleSignIn();
  }, [googleLogin]);

  return (
    <div className="w-full">
      <div id="google-sign-in-button" className="w-full"></div>
    </div>
  );
};

export default GoogleSignInButton;
