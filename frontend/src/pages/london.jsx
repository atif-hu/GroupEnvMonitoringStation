import { Helmet } from 'react-helmet-async';

import { LondonView } from 'src/sections/london';

// ----------------------------------------------------------------------

export default function NorthEastPage() {
  return (
    <>
      <Helmet>
        <title> London | Weather Monitoring Station </title>
      </Helmet>

      <LondonView />
    </>
  );
}
