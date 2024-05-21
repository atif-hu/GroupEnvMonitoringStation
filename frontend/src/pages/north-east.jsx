import { Helmet } from 'react-helmet-async';

import { NorthEastView } from 'src/sections/north-east';

// ----------------------------------------------------------------------

export default function NorthEastPage() {
  return (
    <>
      <Helmet>
        <title> North East | Weather Monitoring Station </title>
      </Helmet>

      <NorthEastView />
    </>
  );
}
