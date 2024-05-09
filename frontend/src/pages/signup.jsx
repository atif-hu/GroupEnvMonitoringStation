import { Helmet } from 'react-helmet-async';

import {SignupView} from 'src/sections/signup';

// ----------------------------------------------------------------------

export default function SignUpPage() {
  return (
    <>
      <Helmet>
        <title> Signup | Weather Monitoring Station </title>
      </Helmet>

      <SignupView />
    </>
  );
}
