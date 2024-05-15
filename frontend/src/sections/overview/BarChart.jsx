import axios from 'axios';
import React, { useState, useEffect } from 'react';
import { Bar, XAxis, YAxis, Legend, Tooltip, BarChart, CartesianGrid, ResponsiveContainer } from 'recharts';

export default function AppWebsiteVisit() {
  const [barChartData, setBarChartData] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const { data } = await axios.get('https://localhost:7298/api/MonitoringStation');
        
        // Assuming your API response is an array of objects with parameters and values
        const chartData = data.map((item) => ({
          name: item.environmentParameter,
          value: item.value,
        }));

        // Keep track of the latest 10 data points
        const latestData = chartData.slice(-10);
        setBarChartData(latestData);
      } catch (error) {
        console.error('Error fetching data:', error);
      }
    };

    const interval = setInterval(fetchData, 5000); // Fetch data every 5 seconds

    return () => clearInterval(interval); // Cleanup interval on component unmount
  }, []);

  return (
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
  );
}
