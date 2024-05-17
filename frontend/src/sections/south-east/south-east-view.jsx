import axios from 'axios';
import { useState, useEffect } from 'react';

import Container from '@mui/material/Container';
import Grid from '@mui/material/Unstable_Grid2';
import Typography from '@mui/material/Typography';

import BarChart from '../overview/BarChart';
import AppWidgetSummary from '../overview/app-widget-summary';


export default function AppView() {
  const [widgetsData, setWidgetsData] = useState({});

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await axios.get('https://localhost:7298/api/MonitoringStation');
        console.log('API Response:', response.data); 

        const newData = response.data;

        const updatedWidgetsData = {};
        newData.forEach((item) => {
          updatedWidgetsData[item.environmentParameter] = item.value;
        });

        console.log('Updated Widgets Data:', updatedWidgetsData);
        setWidgetsData(updatedWidgetsData);
      } catch (error) {
        console.error('Error fetching data:', error);
      }
    };

    fetchData(); 
    const interval = setInterval(fetchData, 5000);
    return () => clearInterval(interval);
  }, []);

  return (
    <Container maxWidth="xl">
      <Typography variant="h4" sx={{ mb: 5 }}>
        Hi, This is South East Data
      </Typography>
      <Grid container spacing={3}>
        {Object.keys(widgetsData).map((parameter) => (
          <Grid key={parameter}  xs={12} sm={6} md={3}>
            <AppWidgetSummary
              title={parameter}
              total={widgetsData[parameter] || 'N/A'} 
              icon={<img alt="icon" src="/assets/icons/glass/Weather.jpeg" />}
            />
          </Grid>
        ))}
      </Grid>

      <Container maxWidth="xl">
      <Typography variant="h4" sx={{ mb: 5 }}>
        This is the Bar Chart for data
      </Typography>
      <Grid container spacing={3}>
        <Grid item xs={12}>
          <BarChart/>
        </Grid>
      </Grid>
    </Container>

    </Container>
  );
}
