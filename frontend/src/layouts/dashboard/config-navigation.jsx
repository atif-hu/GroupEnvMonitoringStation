import SvgColor from 'src/components/svg-color';

// ----------------------------------------------------------------------

const icon = (name) => (
  <SvgColor src={`/assets/icons/regions/${name}.png`} sx={{ width: 1, height: 1 }} />
);

const navConfig = [
  {
    title: 'dashboard',
    path: '/',
    icon: icon('dashboard'),
  },
  {
    title: 'london',
    path: '/london',
    icon: icon('london'),
  },
  {
    title: 'north-east',
    path: '/north-east',
    icon: icon('north-east'),
  },
  {
    title: 'yorkshire',
    path: '/yorkshire',
    icon: icon('fort-york'),
  },
  {
    title: 'midlands',
    path: '/midlands',
    icon: icon('midland'),
  },
  {
    title: 'south-east',
    path: '/south-east',
    icon: icon('south-east'),
  },
  {
    title: 'user',
    path: '/user',
    icon: icon('users'),
  },
  // {
  //   title: 'product',
  //   path: '/products',
  //   icon: icon('ic_cart'),
  // },
  // {
  //   title: 'blog',
  //   path: '/blog',
  //   icon: icon('ic_blog'),
  // },
  {
    title: 'login',
    path: '/login',
    icon: icon('user'),
  },
  {
    title: 'Not found',
    path: '/404',
    icon: icon('disabled'),
  },
];

export default navConfig;
