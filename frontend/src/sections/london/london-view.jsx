// import Cookies from 'js-cookie';
// import { useState } from 'react';
// import { useSnackbar } from 'notistack';
import { faker } from '@faker-js/faker';
// import Container from '@mui/material/Container';
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

import { useRouter } from 'src/routes/hooks';

// import { bgGradient } from 'src/theme/css';

// import Logo from 'src/components/logo';
// import Iconify from 'src/components/iconify';
// import { Chart } from 'chart.js';
// import { Line } from 'react-chartjs-2';
import React, { useState, useEffect  } from 'react';

import { Grid } from '@mui/material';

import config from '../../../config';
import AppNewsUpdate from './app-news-update';
import AppOrderTimeline from './app-order-timeline';
import AppWebsiteVisits from '../overview/app-website-visits';
import LondonDailyMonthlyAverage from './london-daily-monthly-average'


// ----------------------------------------------------------------------

export default function LondonView() {
    const [temperatureData, setTemperatureData] = useState([]);
    const [rainfallData, setRainfallData] = useState([]);
    const [humidityData, setHumidityData] = useState([]);
    const [cO2Data, setcO2Data] = useState([]);
    const [airPollutionData, setAirPollutionData] = useState([]);
    const router = useRouter();
    const [londonTemperatureData, setLondonTemperatureData] = useState([]);
    const [londonRainfallData, setLondonRainfallData] = useState([]);
    const [londonHumidityData, setLondonHumidityData] = useState([]);
    const [londonCO2EmissionsData, setLondonCO2EmissionsData] = useState([]);
    const [londonAirPollutionData, setLondonAirPollutionData] = useState([]);
  //   // const [loading, setLoading] = useState(true);
  const fetchLatestData = async () => {
    try {
      // const temp_limit = config.TEMPERATURE_LIMIT;
      const response = await fetch(`${config.LONDON_MONITORING_STATION_URL}/temperature-monitoring/updates?limit=${50}`); // Replace 'YOUR_API_ENDPOINT' with your actual API endpoint
      if (!response.ok) {
        throw new Error('Failed to fetch data');
      }
      const data = await response.json();
      setLondonTemperatureData(data);
    } catch (error) {
      console.error('Error fetching data:', error);
    }
    try {
      // const rainfall_limit = config.RAINFALL_LIMIT;
      const response = await fetch(`${config.LONDON_MONITORING_STATION_URL}/rainfall-monitoring/updates?limit=${50}`); // Replace 'YOUR_API_ENDPOINT' with your actual API endpoint
      if (!response.ok) {
        throw new Error('Failed to fetch data');
      }
      const data = await response.json();
      setLondonRainfallData(data);
    } catch (error) {
      console.error('Error fetching data:', error);
    }
    try {
      // const humidity_limit = config.HUMIDITY_LIMIT;
      const response = await fetch(`${config.LONDON_MONITORING_STATION_URL}/humidity-monitoring/updates?limit=${50}`); // Replace 'YOUR_API_ENDPOINT' with your actual API endpoint
      if (!response.ok) {
        throw new Error('Failed to fetch data');
      }
      const data = await response.json();
      setLondonHumidityData(data);
    } catch (error) {
      console.error('Error fetching data:', error);
    }
    try {
      // const co2_limit = config.CO2_EMISSIONS_LIMIT;
      const response = await fetch(`${config.LONDON_MONITORING_STATION_URL}/co2-emissions-monitoring/updates?limit=${50}`); // Replace 'YOUR_API_ENDPOINT' with your actual API endpoint
      if (!response.ok) {
        throw new Error('Failed to fetch data');
      }
      const data = await response.json();
      setLondonCO2EmissionsData(data);
    } catch (error) {
      console.error('Error fetching data:', error);
    }
    try {
      // const air_pollution_limit = config.AIR_POLLUTION_LIMIT;
      const response = await fetch(`${config.LONDON_MONITORING_STATION_URL}/air-pollution-monitoring/updates?limit=${50}`); // Replace 'YOUR_API_ENDPOINT' with your actual API endpoint
      if (!response.ok) {
        throw new Error('Failed to fetch data');
      }
      const data = await response.json();
      setLondonAirPollutionData(data);
    } catch (error) {
      console.error(`Error fetching data: `, error);
    }
  };

  useEffect(() => {
    if(document.cookie==='') {
      router.push('/login');
    }
  }, [router]);
  
  useEffect(() => {
    fetchLatestData();
  
    // Fetch new data every 1 minute
    const interval = setInterval(() => {
      fetchLatestData();
    }, 60000);
  
    // Clear interval on component unmount
    return () => clearInterval(interval);
  }, []);
    const fetchTemperatureData = async () => {
        // Fetch temperature data from API
        try {
          // const temp_limit = 5;
          const response = await fetch(`${config.LONDON_MONITORING_STATION_URL}/temperature-monitoring/updates?limit=${50}`);
          const data = await response.json();
          setTemperatureData(data);
        } catch (error) {
            console.error('Error fetching temperature data:', error);
        }
    };

    const fetchRainfallData = async () => {
        // Fetch rainfall data from API
        try {
          // const rainfall_limit = 5;
          const response = await fetch(`${config.LONDON_MONITORING_STATION_URL}/rainfall-monitoring/updates?limit=${50}`);
          const data = await response.json();
          setRainfallData(data);
        } catch (error) {
            console.error('Error fetching rainfall data:', error);
        }
    };
    
    const fetchHumidityData = async () => {
        // Fetch rainfall data from API
        try {
          // const rainfall_limit = 5;
          const response = await fetch(`${config.LONDON_MONITORING_STATION_URL}/humidity-monitoring/updates?limit=${50}`);
          const data = await response.json();
          setHumidityData(data);
        } catch (error) {
            console.error('Error fetching humidity data:', error);
        }
    };

    const fetchAirPollutionData = async () => {
        // Fetch rainfall data from API
        try {
          // const rainfall_limit = 5;
          const response = await fetch(`${config.LONDON_MONITORING_STATION_URL}/air-pollution-monitoring/updates?limit=${50}`);
          const data = await response.json();
          setAirPollutionData(data);
        } catch (error) {
            console.error('Error fetching humidity data:', error);
        }
    };

    const fetchCO2Data = async () => {
        // Fetch rainfall data from API
        try {
          // const rainfall_limit = 5;
          const response = await fetch(`${config.LONDON_MONITORING_STATION_URL}/co2-emissions-monitoring/updates?limit=${50}`);
          const data = await response.json();
          setcO2Data(data);
        } catch (error) {
            console.error('Error fetching humidity data:', error);
        }
    };

    useEffect(() => {
      // Fetch temperature and rainfall data from your backend API
      fetchTemperatureData();
      fetchRainfallData();
      fetchHumidityData();
      fetchAirPollutionData();
      fetchCO2Data();
  }, []);

  function calculateRainfallAverages(data) {
    // Group by date
    const dailyTemperatures = data.reduce((acc, entry) => {
      const date = entry.timestamp.split('T')[0];
      if (!acc[date]) {
        acc[date] = [];
      }
      acc[date].push(entry.rainfall);
      return acc;
    }, {});
  
    // Calculate daily averages
    const dailyAverages = Object.entries(dailyTemperatures).reduce((acc, [date, temps]) => {
      const average = temps.reduce((sum, temp) => sum + temp, 0) / temps.length;
      acc[date] = average;
      return acc;
    }, {});
  
    // Calculate monthly average
    const dailyAverageValues = Object.values(dailyAverages);
    const monthlyAverage = dailyAverageValues.reduce((sum, avg) => sum + avg, 0) / dailyAverageValues.length;
    return { dailyAverages, monthlyAverage };
  }

  function calculateHumidityAverages(data) {
    // Group by date
    const dailyTemperatures = data.reduce((acc, entry) => {
      const date = entry.timestamp.split('T')[0];
      if (!acc[date]) {
        acc[date] = [];
      }
      acc[date].push(entry.humidity);
      return acc;
    }, {});
  
    // Calculate daily averages
    const dailyAverages = Object.entries(dailyTemperatures).reduce((acc, [date, temps]) => {
      const average = temps.reduce((sum, temp) => sum + temp, 0) / temps.length;
      acc[date] = average;
      return acc;
    }, {});
  
    // Calculate monthly average
    const dailyAverageValues = Object.values(dailyAverages);
    const monthlyAverage = dailyAverageValues.reduce((sum, avg) => sum + avg, 0) / dailyAverageValues.length;
    return { dailyAverages, monthlyAverage };
  }

  function calculateCO2Averages(data) {
    // Group by date
    const dailyTemperatures = data.reduce((acc, entry) => {
      const date = entry.timestamp.split('T')[0];
      if (!acc[date]) {
        acc[date] = [];
      }
      acc[date].push(entry.cO2Emissions);
      return acc;
    }, {});
  
    // Calculate daily averages
    const dailyAverages = Object.entries(dailyTemperatures).reduce((acc, [date, temps]) => {
      const average = temps.reduce((sum, temp) => sum + temp, 0) / temps.length;
      acc[date] = average;
      return acc;
    }, {});
  
    // Calculate monthly average
    const dailyAverageValues = Object.values(dailyAverages);
    const monthlyAverage = dailyAverageValues.reduce((sum, avg) => sum + avg, 0) / dailyAverageValues.length;
    return { dailyAverages, monthlyAverage };
  }

  function calculateairPollutionAverages(data) {
    // Group by date
    const dailyTemperatures = data.reduce((acc, entry) => {
      const date = entry.timestamp.split('T')[0];
      if (!acc[date]) {
        acc[date] = [];
      }
      acc[date].push(entry.airPollution);
      return acc;
    }, {});
  
    // Calculate daily averages
    const dailyAverages = Object.entries(dailyTemperatures).reduce((acc, [date, temps]) => {
      const average = temps.reduce((sum, temp) => sum + temp, 0) / temps.length;
      acc[date] = average;
      return acc;
    }, {});
  
    // Calculate monthly average
    const dailyAverageValues = Object.values(dailyAverages);
    const monthlyAverage = dailyAverageValues.reduce((sum, avg) => sum + avg, 0) / dailyAverageValues.length;
    return { dailyAverages, monthlyAverage };
  }

  function calculateAverages(data) {
    // Group by date
    const dailyTemperatures = data.reduce((acc, entry) => {
      const date = entry.timestamp.split('T')[0];
      if (!acc[date]) {
        acc[date] = [];
      }
      acc[date].push(entry.temperature);
      return acc;
    }, {});
  
    // Calculate daily averages
    const dailyAverages = Object.entries(dailyTemperatures).reduce((acc, [date, temps]) => {
      const average = temps.reduce((sum, temp) => sum + temp, 0) / temps.length;
      acc[date] = average;
      return acc;
    }, {});
  
    // Calculate monthly average
    const dailyAverageValues = Object.values(dailyAverages);
    const monthlyAverage = dailyAverageValues.reduce((sum, avg) => sum + avg, 0) / dailyAverageValues.length;
    return { dailyAverages, monthlyAverage };
  }
  
  // daily avg labels for temperature data 
  const averages = calculateAverages(temperatureData);
  const dailyAvglabels = Object.keys(averages.dailyAverages);
  const dailyData = Object.values(averages.dailyAverages).map(avg => avg.toFixed(3));
  
  // daily avg labels for rainfall data 
  const rainfallAverages = calculateRainfallAverages(rainfallData);
  const rainfallDailyAvglabels = Object.keys(rainfallAverages.dailyAverages);
  const rainfallDailyData = Object.values(rainfallAverages.dailyAverages).map(avg => avg.toFixed(3));
  
  // daily avg labels for humidity data 
  const humidityAverages = calculateHumidityAverages(humidityData);
  const humidityDailyAvglabels = Object.keys(humidityAverages.dailyAverages);
  const humidityDailyData = Object.values(humidityAverages.dailyAverages).map(avg => avg.toFixed(3));
  
  // daily avg labels for co2 data 
  const cO2Averages = calculateCO2Averages(cO2Data);
  const cO2DailyAvglabels = Object.keys(cO2Averages.dailyAverages);
  const cO2DailyData = Object.values(cO2Averages.dailyAverages).map(avg => avg.toFixed(3));
  
  // daily avg labels for air pollution data 
  const airPollutionAverages = calculateairPollutionAverages(airPollutionData);
  const airPollutionDailyAvglabels = Object.keys(airPollutionAverages.dailyAverages);
  const airPollutionDailyData = Object.values(airPollutionAverages.dailyAverages).map(avg => avg.toFixed(3));

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
  
  const labels = generateLabels();
  
  if (!labels.length || !rainfallData.length === 0 || !temperatureData.length) {
    return null;
  }
    return (
      <Grid container  spacing={3} >
        
        {/* row 1  */}
          <Grid item xs={12} md={6} lg={12}>
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
                {
                  name: 'Humidity',
                  type: 'column',
                  fill: 'solid',
                  data: humidityData.map(r=>r.humidity),
                },
                {
                  name: 'CO2 Emissions',
                  type: 'column',
                  fill: 'solid',
                  data: cO2Data.map(r=>r.cO2Emissions),
                },
                {
                  name: 'Air Pollution',
                  type: 'column',
                  fill: 'solid',
                  data: airPollutionData.map(r=>r.airPollution),
                },
              ],
            }}
          />
          </Grid>

            {/* row 2 graph */}
          <Grid item xs={12} md={6} lg={8}>
          <LondonDailyMonthlyAverage
            title="London Temperature Sensors daily average values"
            // subheader="(+43%) than last year"
            chart={{
              labels: dailyAvglabels,
              series: [{
                  name: 'Daily Average Temperature',
                  type: 'area',
                  fill: 'gradient',
                  data: dailyData,
                },
              ]
            }}
          />
        </Grid>

            {/* row 2 avg */}
        <Grid item xs={12} md={6} lg={4}>
          <AppOrderTimeline
            title="Temperature daily and monthly average"
            list={[...Array(2)].map((_, index) => ({
              id: faker.string.uuid(),
              title: [
                `Daily Average: ${dailyData[0]} degree celcius`,
                `Monthly Average: ${averages.monthlyAverage.toFixed(3)} degree celcius`
              ][index],
              type: `order${index + 1}`,
            }))}
          />
        </Grid>

            {/* row 3 graph */}
          <Grid item xs={12} md={6} lg={8}>
          <LondonDailyMonthlyAverage
            title="London Rainfall Sensors daily average values"
            // subheader="(+43%) than last year"
            chart={{
              labels: rainfallDailyAvglabels,
              series: [{
                  name: 'Daily Average Rainfall',
                  type: 'area',
                  fill: 'gradient',
                  data: rainfallDailyData,
                },
              ],
            }}
          />
        </Grid>

            {/* row 3 avg */}
        <Grid item xs={12} md={6} lg={4}>
          <AppOrderTimeline
            title="Rainfall daily and monthly average"
            list={[...Array(2)].map((_, index) => ({
              id: faker.string.uuid(),
              title: [
                `Daily Average: ${rainfallDailyData[0]} mm`,
                `Monthly Average: ${rainfallAverages.monthlyAverage.toFixed(3)} mm`
              ][index],
              type: `order${index + 1}`,
            }))}
          />
        </Grid>

            {/* row 5 Air pollution graph */}
          <Grid item xs={12} md={6} lg={8}>
          <LondonDailyMonthlyAverage
            title="London Air pollution Sensors daily average values"
            // subheader="(+43%) than last year"
            chart={{
              labels: airPollutionDailyAvglabels,
              series: [{
                  name: 'Daily Average Air pollution',
                  type: 'area',
                  fill: 'gradient',
                  data: airPollutionDailyData,
                },
              ],
            }}
          />
        </Grid>

            {/* row 5 air pollution avg */}
        <Grid item xs={12} md={6} lg={4}>
          <AppOrderTimeline
            title="Air pollution daily and monthly average"
            list={[...Array(2)].map((_, index) => ({
              id: faker.string.uuid(),
              title: [
                `Daily Average: ${airPollutionDailyData[0]} AQI`,
                `Monthly Average: ${airPollutionAverages.monthlyAverage.toFixed(3)} AQI`
              ][index],
              type: `order${index + 1}`,
            }))}
          />
        </Grid>

            {/* row 6 co2 graph */}
          <Grid item xs={12} md={6} lg={8}>
          <LondonDailyMonthlyAverage
            title="London CO2 Emissions Sensors daily average values"
            // subheader="(+43%) than last year"
            chart={{
              labels: cO2DailyAvglabels,
              series: [{
                  name: 'Daily Average CO2',
                  type: 'area',
                  fill: 'gradient',
                  data: cO2DailyData,
                },
              ],
            }}
          />
        </Grid>

            {/* row 6 co2 avg */}
        <Grid item xs={12} md={6} lg={4}>
          <AppOrderTimeline
            title="CO2 emissions daily and monthly average"
            list={[...Array(2)].map((_, index) => ({
              id: faker.string.uuid(),
              title: [
                `Daily Average: ${cO2DailyData[0]} co2e`,
                `Monthly Average: ${cO2Averages.monthlyAverage.toFixed(3)} co2e`
              ][index],
              type: `order${index + 1}`,
            }))}
          />
        </Grid>

            {/* row 4 humidity graph */}
          <Grid item xs={12} md={6} lg={8}>
          <LondonDailyMonthlyAverage
            title="London Humidity Sensors daily average values"
            // subheader="(+43%) than last year"
            chart={{
              labels: humidityDailyAvglabels,
              series: [{
                  name: 'Daily Average Humidity',
                  type: 'area',
                  fill: 'gradient',
                  data: humidityDailyData,
                },
              ],
            }}
          />
        </Grid>

            {/* row 4 humidity avg */}
        <Grid item xs={12} md={6} lg={4}>
          <AppOrderTimeline
            title="Humidity daily and monthly average"
            list={[...Array(2)].map((_, index) => ({
              id: faker.string.uuid(),
              title: [
                `Daily Average: ${humidityDailyData[0]} %`,
                `Monthly Average: ${humidityAverages.monthlyAverage.toFixed(3)} %`
              ][index],
              type: `order${index + 1}`,
            }))}
          />
        </Grid>

            {/* row 7 historical data */}
        <Grid item xs={12} md={6 } lg={12}  >
          <AppNewsUpdate
          sx={{height:'500px', overflow:'auto'}}
            title='Weather Monitoring Sensors'
            list={londonTemperatureData.map((news, index) => ({
              id: news.id,
              title: 'London temperature',
              description: news.temperature.toString(),
              image: `/assets/icons/sensors/temperature.png`,
              postedAt: news.timestamp,
              highWarning: news.temperature>30,
              lowWarning: news.temperature<-9,   
              unit: 'Degree Celcius'               
            }))}
            list1={londonRainfallData.map((news, index) => ({
              id: news.id,
              title: 'London rainfall',
              description: news.rainfall.toString(),
              image: `/assets/icons/sensors/rainfall.png`,
              postedAt: news.timestamp,
              highWarning: news.rainfall>32, 
              lowWarning: news.rainfall<-9,   
              unit: 'Millimeters'               
            }))}
            list2={londonHumidityData.map((news, index) => ({
              id: news.id,
              title: 'London humidity',
              description: news.humidity.toString(),
              image: `/assets/icons/sensors/humidity.png`,
              postedAt: news.timestamp,
              highWarning: false, 
              lowWarning: false,   
              unit: 'Percent'               
            }))}
            list3={londonCO2EmissionsData.map((news, index) => ({
              id: news.id,
              title: 'London CO2 Emissions',
              description: news.cO2Emissions.toString(),
              image: `/assets/icons/sensors/co2.png`,
              postedAt: news.timestamp,
              highWarning: false, 
              lowWarning: false,   
              unit: 't CO2e'               
            }))}
            list4={londonAirPollutionData.map((news, index) => ({
              id: news.id,
              title: 'London Air Pollution',
              description: news.airPollution.toString(),
              image: `/assets/icons/sensors/air-pollution.png`,
              postedAt: news.timestamp,
              highWarning: news.airPollution>9, 
              lowWarning: news.airPollution<1,   
              unit: 'AQI'               
            }))}
            />
        </Grid>
      
      </Grid>
  );
};

