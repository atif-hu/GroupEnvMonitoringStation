import { lazy, Suspense } from 'react';
import { Outlet, Navigate, useRoutes } from 'react-router-dom';

import SignupPage from 'src/pages/signup';
import DashboardLayout from 'src/layouts/dashboard';

export const IndexPage = lazy(() => import('src/pages/app'));
export const BlogPage = lazy(() => import('src/pages/blog'));
export const UserPage = lazy(() => import('src/pages/user'));
export const LoginPage = lazy(() => import('src/pages/login'));
export const LondonPage = lazy(() => import('src/pages/london'));
export const NorthEastPage = lazy(() => import('src/pages/north-east'));
export const YorkshirePage = lazy(() => import('src/pages/yorkshire'));
export const MidlandsPage = lazy(() => import('src/pages/midlands'));
export const SouthEastPage = lazy(() => import('src/pages/south-east'));
export const ProductsPage = lazy(() => import('src/pages/products'));
export const Page404 = lazy(() => import('src/pages/page-not-found'));

// ----------------------------------------------------------------------

export default function Router() {
  const routes = useRoutes([
    {
      element: (
        <DashboardLayout>
          <Suspense>
            <Outlet />
          </Suspense>
        </DashboardLayout>
      ),
      children: [
        { element: <IndexPage />, index: true },
        { path: 'user', element: <UserPage /> },
        { path: 'london', element: <LondonPage /> },
        { path: 'north-east', element: <NorthEastPage /> },
        { path: 'yorkshire', element: <YorkshirePage /> },
        { path: 'midlands', element: <MidlandsPage /> },
        { path: 'south-east', element: <SouthEastPage /> },
        { path: 'products', element: <ProductsPage /> },
        { path: 'blog', element: <BlogPage /> },
      ],
    },
    {
      path: 'login',
      element: <LoginPage />,
    },
    {
      path: 'signup',
      element: <SignupPage />,
    },
    {
      path: 'dashboard',
      element: <UserPage />,
    },
    {
      path: '404',
      element: <Page404 />,
    },
    {
      path: '*',
      element: <Navigate to="/404" replace />,
    },
  ]);

  return routes;
}
