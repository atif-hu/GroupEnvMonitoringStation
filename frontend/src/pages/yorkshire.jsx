import { Helmet } from 'react-helmet-async';

import { YorkshireView } from 'src/sections/yorkshire';

// ----------------------------------------------------------------------

export default function NorthEastPage() {
  return (
    <>
      <Helmet>
        <title> Yorkshire | Weather Monitoring Station </title>
      </Helmet>

      <YorkshireView />
    </>
  );
}
