import axios from 'axios';
import React, { useState, useEffect } from 'react';
import { Bar, XAxis, YAxis, Legend, Tooltip, BarChart, CartesianGrid, ResponsiveContainer } from 'recharts';

import Box from '@mui/material/Box';
import Grid from '@mui/material/Grid';
import Card from '@mui/material/Card';
import Stack from '@mui/material/Stack';
import Typography from '@mui/material/Typography';

export default function AppWebsiteVisit() {
  const [barChartData, setBarChartData] = useState([]);
  const [warnings, setWarnings] = useState({
    Temperature: '',
    Rainfall: '',
    Humidity: '',
    'Air Pollution': '',
    'CO2 Emission': ''
  });

  useEffect(() => {
    const fetchData = async () => {
      try {
        const { data } = await axios.get('https://localhost:7298/api/MonitoringStation');
        
        const chartData = data.map((item) => ({
          name: item.environmentParameter,
          value: item.value,
        }));

        const latestData = chartData.slice(-10);
        setBarChartData(latestData);

        const newWarnings = {
          Temperature: '',
          Rainfall: '',
          Humidity: '',
          'Air Pollution': '',
          'CO2 Emission': ''
        };
        
        data.forEach((item) => {
          if (item.environmentParameter === 'Temperature') {
            if (item.value > 30 || item.value < -9) newWarnings.Temperature = 'Temperature Is More Than Normal Level Please take Precautionary Measures';
            else newWarnings.Temperature = 'Temperature is Normal';
          }
          if (item.environmentParameter === 'Humidity') {
            if (item.value > 0) newWarnings.Humidity = (item.value);
          }
          if (item.environmentParameter === 'Rainfall') {
            if (item.value > 32 || item.value < 0) newWarnings.Rainfall = 'Rainfall Is More Than Normal Level Please take Precautionary Measures.';
            else newWarnings.Rainfall = 'Rainfall is Normal.';
          }
          if (item.environmentParameter === 'Air Pollution') {
            if (item.value > 9 || item.value < 1) newWarnings['Air Pollution'] = 'AirPollution Is Above Normal';
            else newWarnings['Air Pollution'] = 'Air Pollution is Normal';
          }
          if (item.environmentParameter === 'CO2 Emission') {
            if (item.value > 0) newWarnings['CO2 Emission'] = (item.value);
          }
        });

        setWarnings(newWarnings);
      } catch (error) {
        console.error('Error fetching data:', error);
      }
    };

    const interval = setInterval(fetchData, 5000); 

    return () => clearInterval(interval);
  }, []);

  return (
    <Box>
      <ResponsiveContainer width="100%" height={400}>
        <BarChart data={barChartData}>
          <CartesianGrid strokeDasharray="3 3" />
          <XAxis dataKey="name" />
          <YAxis />
          <Tooltip />
          <Legend />
          <Bar dataKey="value" fill="#8884d8" barSize={30} />
        </BarChart>
      </ResponsiveContainer>

      <Grid container spacing={2} sx={{ mt: 3 }}>
        {Object.keys(warnings).map((parameter, index) => (
          <Grid item xs={12} sm={6} md={3} key={index}>
            <Card sx={{ p: 2, display: 'flex', flexDirection: 'column', alignItems: 'center' }}>
              <img alt="icon" src="/assets/icons/glass/War.jpeg" style={{ width: 64, height: 64 }} />
              <Stack spacing={1} sx={{ mt: 2, textAlign: 'center' }}>
                <Typography variant="h6">{parameter}</Typography>
                <Typography variant="body2" color="error">{warnings[parameter]}</Typography>
              </Stack>
            </Card>
          </Grid>
        ))}
      </Grid>
    </Box>
  );
}
