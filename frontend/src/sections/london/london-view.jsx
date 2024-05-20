// import Cookies from 'js-cookie';
// import { useState } from 'react';
// import { useSnackbar } from 'notistack';

// import Box from '@mui/material/Box';
// import Link from '@mui/material/Link';
// import Card from '@mui/material/Card';
// import Stack from '@mui/material/Stack';
// import Button from '@mui/material/Button';
// import Divider from '@mui/material/Divider';
// import TextField from '@mui/material/TextField';
// import Typography from '@mui/material/Typography';
// import IconButton from '@mui/material/IconButton';
// import LoadingButton from '@mui/lab/LoadingButton';
// import { alpha, useTheme } from '@mui/material/styles';
// import InputAdornment from '@mui/material/InputAdornment';

// import { useRouter } from 'src/routes/hooks';

// import { bgGradient } from 'src/theme/css';

// import Logo from 'src/components/logo';
// import Iconify from 'src/components/iconify';
// import { Chart } from 'chart.js';
// import { Line } from 'react-chartjs-2';
import React, { useState, useEffect  } from 'react';

import { Grid } from '@mui/material';

import config from '../../../config';
import AppWebsiteVisits from '../overview/app-website-visits';


// ----------------------------------------------------------------------

export default function LondonView() {
    const [temperatureData, setTemperatureData] = useState([]);
    const [rainfallData, setRainfallData] = useState([]);
  //   // const [loading, setLoading] = useState(true);

    const fetchTemperatureData = async () => {
        // Fetch temperature data from API
        try {
          const temp_limit = 50;
          const response = await fetch(`${config.LONDON_MONITORING_STATION_URL}/temperature-monitoring/updates?limit=${temp_limit}`);
          const data = await response.json();
          setTemperatureData(data);
        } catch (error) {
            console.error('Error fetching temperature data:', error);
        }
    };

    const fetchRainfallData = async () => {
        // Fetch rainfall data from API
        try {
          const rainfall_limit = 50;
          const response = await fetch(`${config.LONDON_MONITORING_STATION_URL}/rainfall-monitoring/updates?limit=${rainfall_limit}`);
          const data = await response.json();
          setRainfallData(data);
        } catch (error) {
            console.error('Error fetching rainfall data:', error);
        }
    };

    useEffect(() => {
      // Fetch temperature and rainfall data from your backend API
      fetchTemperatureData();
      fetchRainfallData();
  }, []);

    // Calculate daily and monthly averages
    // const calculateAverages = (data) => {
    //     // Calculate daily average
    //     const dailyAverage = data.reduce((acc, curr) => acc + curr.temperature, 0) / data.length;

    //     // Calculate monthly average
    //     const monthlyAverage = data.reduce((acc, curr) => acc + curr.temperature, 0) / data.length;

    //     return { dailyAverage, monthlyAverage };
    // };
  
    function generateLabels() {
      const labels = [];
      const today = new Date();
  
      for (let i = 0; i < 30; i+=1) {
          const date = new Date(today);
          date.setDate(date.getDate() - i);
          labels.push(date.toISOString().split('T')[0]);
      }
  
      return labels.reverse();
  }
  console.log(rainfallData)
  console.log(temperatureData)
  
  const labels = generateLabels();
  console.log(labels)
  if (!labels.length || !rainfallData.length === 0 || !temperatureData.length) {
    return null;
  }
    return (
      <Grid container spacing={3}>
          <Grid item xs={12} md={6} lg={8}>
          <AppWebsiteVisits
            title="London Sensors"
            // subheader="(+43%) than last year"
            chart={{
              labels: temperatureData.map(label => label.timestamp),
              series: [
                {
                  name: 'Temperature',
                  type: 'column',
                  fill: 'solid',
                  data: temperatureData.map(t=>t.temperature),
                },
                {
                  name: 'Rainfall',
                  type: 'column',
                  fill: 'solid',
                  data: rainfallData.map(r=>r.rainfall),
                },
                // {
                //   name: 'Air Pollution',
                //   type: 'area',
                //   fill: 'gradient',
                //   data: [44, 55, 41, 67, 22, 43, 21, 41, 56, 27, 43],
                // },
                // {
                //   name: 'Humidity',
                //   type: 'line',
                //   fill: 'solid',
                //   data: [30, 25, 36, 30, 45, 35, 64, 52, 59, 36, 39],
                // },
                // {
                //   name: 'CO2 Emissions',
                //   type: 'area',
                //   fill: 'solid',
                //   data: [30, 25, 36, 30, 45, 35, 64, 52, 59, 36, 39],
                // },
              ],
            }}
          />
              {/* <Typography variant="body1">Daily Average: {calculateAverages(temperatureData).dailyAverage.toFixed(2)}°C</Typography>
              <Typography variant="body1">Monthly Average: {calculateAverages(temperatureData).monthlyAverage.toFixed(2)}°C</Typography> */}
          </Grid>
          {/* <Grid item xs={12} md={6} lg={8}>
              <Chart
                  title="Rainfall"
                  data={{
                      labels: rainfallData.map((entry) => entry.timestamp),
                      datasets: [
                          {
                              label: 'Rainfall',
                              data: rainfallData.map((entry) => entry.rainfall),
                              backgroundColor: 'rgba(75, 192, 192, 0.2)',
                              borderColor: 'rgba(75, 192, 192, 1)',
                              borderWidth: 1,
                          },
                      ],
                  }}
              />
              <Line data={{labels: rainfallData}} />
              <Typography variant="body1">Daily Average: {calculateAverages(rainfallData).dailyAverage.toFixed(2)} mm</Typography>
              <Typography variant="body1">Monthly Average: {calculateAverages(rainfallData).monthlyAverage.toFixed(2)} mm</Typography>
          </Grid> */}
      </Grid>
  );
};

