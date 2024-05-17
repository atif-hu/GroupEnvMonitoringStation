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
        console.log('API Response:', response.data); // Log the API response for debugging

        const newData = response.data;

        const updatedWidgetsData = {};
        newData.forEach((item) => {
          updatedWidgetsData[item.environmentParameter] = item.value;
        });

        console.log('Updated Widgets Data:', updatedWidgetsData); // Log the updated widget data
        setWidgetsData(updatedWidgetsData);
      } catch (error) {
        console.error('Error fetching data:', error);
      }
    };

    fetchData(); // Initial fetch
    const interval = setInterval(fetchData, 5000); // Polling every 5 seconds
    return () => clearInterval(interval);
  }, []);

  return (
    <Container maxWidth="xl">
      <Typography variant="h4" sx={{ mb: 5 }}>
        Hi, Welcome back 👋
      </Typography>
      <Grid container spacing={3}>
        {Object.keys(widgetsData).map((parameter) => (
          <Grid key={parameter}  xs={12} sm={6} md={3}>
            <AppWidgetSummary
              title={parameter}
              total={widgetsData[parameter] || 'N/A'} // Display 'N/A' if the value is undefined or null
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
        {/* Other components */}
        <Grid item xs={12}>
          <BarChart/>
        </Grid>
      </Grid>
    </Container>

    </Container>
  );
}
