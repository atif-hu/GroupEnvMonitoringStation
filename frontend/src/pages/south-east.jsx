import { Helmet } from 'react-helmet-async';

import { SouthEastView } from 'src/sections/south-east';

// ----------------------------------------------------------------------

export default function NorthEastPage() {
  return (
    <>
      <Helmet>
        <title> South East | Weather Monitoring Station </title>
      </Helmet>

      <SouthEastView />
    </>
  );
}
