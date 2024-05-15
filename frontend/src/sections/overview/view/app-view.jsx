import { faker } from '@faker-js/faker';
import React, { useState,useEffect } from 'react';

import Container from '@mui/material/Container';
import Grid from '@mui/material/Unstable_Grid2';
import Typography from '@mui/material/Typography';

import { useRouter } from 'src/routes/hooks';

import Iconify from 'src/components/iconify';

import AppTasks from '../app-tasks';
import config from '../../../../config';
import AppNewsUpdate from '../app-news-update';
import AppOrderTimeline from '../app-order-timeline';
import AppCurrentVisits from '../app-current-visits';
import AppWebsiteVisits from '../app-website-visits';
import AppWidgetSummary from '../app-widget-summary';
import AppTrafficBySite from '../app-traffic-by-site';
import AppCurrentSubject from '../app-current-subject';
import AppConversionRates from '../app-conversion-rates';


// ----------------------------------------------------------------------

export default function AppView() {

  const router = useRouter();
  const [newsData, setNewsData] = useState([]);
  let londonTempWarning = false;

  const handleGridClick = (route) => {
    router.push(route);
  };

  const fetchLatestData = async () => {
    try {
      const temp_limit = config.TEMPERATURE_LIMIT;
      const response = await fetch(`https://localhost:7051/api/temperature-monitoring/updates?limit=${temp_limit}`); // Replace 'YOUR_API_ENDPOINT' with your actual API endpoint
      if (!response.ok) {
        throw new Error('Failed to fetch data');
      }
      const data = await response.json();
      setNewsData(data);
      if(data.temperature>30 || data.temperature<-9){
        londonTempWarning = true;
      }
    } catch (error) {
      console.error('Error fetching data:', error);
    }
    // try {
    //   const rainfall_limit = config.RAINFALL_LIMIT;
    //   const response = await fetch(`https://localhost:7051/api/rainfall-monitoring/updates?limit=${rainfall_limit}`); // Replace 'YOUR_API_ENDPOINT' with your actual API endpoint
    //   if (!response.ok) {
    //     throw new Error('Failed to fetch data');
    //   }
    //   const data = await response.json();
    //   setNewsData(data);
    // } catch (error) {
    //   console.error('Error fetching data:', error);
    // }
  };

  useEffect(() => {
    if(document.cookie==='') router.push('/login')
    fetchLatestData();

    // Fetch new data every 1 minute
    const interval = setInterval(() => {
      fetchLatestData();
    }, 60000);

    // Clear interval on component unmount
    return () => clearInterval(interval);
  });

  return (
    <Container maxWidth="xl">
      <Typography variant="h4" sx={{ mb: 5 }}>
        Hi, Welcome back 👋
      </Typography>

      <Grid container spacing={3}>
        <Grid xs={12} sm={6} md={2.4}>
        <button
           type="button"
           style={{ cursor: 'pointer', border: 'none', background: 'none', padding: 0 }}
           onClick={() => handleGridClick('/london')}
        >
            <AppWidgetSummary
              title="London"
              total={714000}
              color="success"
              icon={<img alt="icon" src="/assets/icons/regions/london.png" />}
              />
          </button>
        </Grid>

        <Grid xs={12} sm={6} md={2.4} sx={{cursor:'pointer'}}>
        <button
           type="button"
           style={{ cursor: 'pointer', border: 'none', background: 'none', padding: 0 }}
           onClick={() => handleGridClick('/north-east')}
        >
          <AppWidgetSummary
            title="North East"
            total={1352831}
            color="info"
            icon={<img alt="icon" src="/assets/icons/regions/north-east.png" />}
          />
          </button>
        </Grid>

        <Grid xs={12} sm={6} md={2.4} sx={{cursor:'pointer'}}>
        <button
           type="button"
           style={{ cursor: 'pointer', border: 'none', background: 'none', padding: 0 }}
           onClick={() => handleGridClick('/yorkshire')}
        >
          <AppWidgetSummary
            title="Yorkshire"
            total={1723315}
            color="warning"
            icon={<img alt="icon" src="/assets/icons/regions/fort-york.png" />}
          />
          </button>
        </Grid>

        <Grid xs={12} sm={6} md={2.4} sx={{cursor:'pointer'}}>
        <button
           type="button"
           style={{ cursor: 'pointer', border: 'none', background: 'none', padding: 0 }}
           onClick={() => handleGridClick('/midlands')}
        >
          <AppWidgetSummary
            title="Midlands"
            total={234}
            color="error"
            icon={<img alt="icon" src="/assets/icons/regions/midland.png" />}
          />
          </button>
        </Grid>

        <Grid xs={12} sm={6} md={2.4} sx={{cursor:'pointer'}}>
        <button
           type="button"
           style={{ cursor: 'pointer', border: 'none', background: 'none', padding: 0 }}
           onClick={() => handleGridClick('/south-east')}
        >
          <AppWidgetSummary
            title="South East"
            total={234}
            color="error"
            icon={<img alt="icon" src="/assets/icons/regions/south-east.png" />}
          />
          </button>
        </Grid>
        
            <Grid xs={12} md={6 } lg={12}>
              <AppNewsUpdate
                title='Temperature'
                list={newsData.map((news, index) => ({
                  id: news.id,
                  title: 'London temperature',
                  description: news.temperature,
                  image: `/assets/icons/sensors/temperature.png`,
                  postedAt: news.timestamp,
                  warning:!londonTempWarning
                }))}
              />
            </Grid>

        <Grid xs={12} md={6} lg={8}>
          <AppWebsiteVisits
            title="Website Visits"
            subheader="(+43%) than last year"
            chart={{
              labels: [
                '01/01/2003',
                '02/01/2003',
                '03/01/2003',
                '04/01/2003',
                '05/01/2003',
                '06/01/2003',
                '07/01/2003',
                '08/01/2003',
                '09/01/2003',
                '10/01/2003',
                '11/01/2003',
              ],
              series: [
                {
                  name: 'Team A',
                  type: 'column',
                  fill: 'solid',
                  data: [23, 11, 22, 27, 13, 22, 37, 21, 44, 22, 30],
                },
                {
                  name: 'Team B',
                  type: 'area',
                  fill: 'gradient',
                  data: [44, 55, 41, 67, 22, 43, 21, 41, 56, 27, 43],
                },
                {
                  name: 'Team C',
                  type: 'line',
                  fill: 'solid',
                  data: [30, 25, 36, 30, 45, 35, 64, 52, 59, 36, 39],
                },
              ],
            }}
          />
        </Grid>

        <Grid xs={12} md={6} lg={4}>
          <AppCurrentVisits
            title="Current Visits"
            chart={{
              series: [
                { label: 'America', value: 4344 },
                { label: 'Asia', value: 5435 },
                { label: 'Europe', value: 1443 },
                { label: 'Africa', value: 4443 },
              ],
            }}
          />
        </Grid>

        <Grid xs={12} md={6} lg={8}>
          <AppConversionRates
            title="Conversion Rates"
            subheader="(+43%) than last year"
            chart={{
              series: [
                { label: 'Italy', value: 400 },
                { label: 'Japan', value: 430 },
                { label: 'China', value: 448 },
                { label: 'Canada', value: 470 },
                { label: 'France', value: 540 },
                { label: 'Germany', value: 580 },
                { label: 'South Korea', value: 690 },
                { label: 'Netherlands', value: 1100 },
                { label: 'United States', value: 1200 },
                { label: 'United Kingdom', value: 1380 },
              ],
            }}
          />
        </Grid>

        <Grid xs={12} md={6} lg={4}>
          <AppCurrentSubject
            title="Current Subject"
            chart={{
              categories: ['English', 'History', 'Physics', 'Geography', 'Chinese', 'Math'],
              series: [
                { name: 'Series 1', data: [80, 50, 30, 40, 100, 20] },
                { name: 'Series 2', data: [20, 30, 40, 80, 20, 80] },
                { name: 'Series 3', data: [44, 76, 78, 13, 43, 10] },
              ],
            }}
          />
        </Grid>

        

        <Grid xs={12} md={6} lg={4}>
          <AppOrderTimeline
            title="Order Timeline"
            list={[...Array(5)].map((_, index) => ({
              id: faker.string.uuid(),
              title: [
                '1983, orders, $4220',
                '12 Invoices have been paid',
                'Order #37745 from September',
                'New order placed #XF-2356',
                'New order placed #XF-2346',
              ][index],
              type: `order${index + 1}`,
              time: faker.date.past(),
            }))}
          />
        </Grid>

        <Grid xs={12} md={6} lg={4}>
          <AppTrafficBySite
            title="Traffic by Site"
            list={[
              {
                name: 'FaceBook',
                value: 323234,
                icon: <Iconify icon="eva:facebook-fill" color="#1877F2" width={32} />,
              },
              {
                name: 'Google',
                value: 341212,
                icon: <Iconify icon="eva:google-fill" color="#DF3E30" width={32} />,
              },
              {
                name: 'Linkedin',
                value: 411213,
                icon: <Iconify icon="eva:linkedin-fill" color="#006097" width={32} />,
              },
              {
                name: 'Twitter',
                value: 443232,
                icon: <Iconify icon="eva:twitter-fill" color="#1C9CEA" width={32} />,
              },
            ]}
          />
        </Grid>

        <Grid xs={12} md={6} lg={8}>
          <AppTasks
            title="Tasks"
            list={[
              { id: '1', name: 'Create FireStone Logo' },
              { id: '2', name: 'Add SCSS and JS files if required' },
              { id: '3', name: 'Stakeholder Meeting' },
              { id: '4', name: 'Scoping & Estimations' },
              { id: '5', name: 'Sprint Showcase' },
            ]}
          />
        </Grid>
      </Grid>
    </Container>
  );
}
