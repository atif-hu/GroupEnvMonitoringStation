/* eslint-disable perfectionist/sort-imports */
import './global.css';

// import {AuthPage} from 'src/components/Auth'

import { SnackbarProvider } from 'notistack';
import { useScrollToTop } from './hooks/use-scroll-to-top';

import Router from './routes/sections';
import ThemeProvider from './theme';

// ----------------------------------------------------------------------

export default function App() {
  useScrollToTop();

  return (
    <SnackbarProvider maxSnack={3}>
    <ThemeProvider>
      {/* <AuthPage> */}
      <Router />
      {/* </AuthPage> */}
    </ThemeProvider>
    </SnackbarProvider>
  );
}
