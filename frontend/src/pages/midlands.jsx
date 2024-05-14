import { Helmet } from 'react-helmet-async';

import { MidlandsView } from 'src/sections/midlands';

// ----------------------------------------------------------------------

export default function NorthEastPage() {
  return (
    <>
      <Helmet>
        <title> Midlands | Weather Monitoring Station </title>
      </Helmet>

      <MidlandsView />
    </>
  );
}
