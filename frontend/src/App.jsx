//import { useState } from 'react'
//import reactLogo from './assets/react.svg'
//import viteLogo from '/vite.svg'
import './App.css'
import { useState, useEffect } from 'react';
function App() {
  //const [count, setCount] = useState(0)

  //return (
  //    <>
  //    <div>
  //      <a href="https://vitejs.dev" target="_blank">
  //        <img src={viteLogo} className="logo" alt="Vite logo" />
  //      </a>
  //      <a href="https://react.dev" target="_blank">
  //        <img src={reactLogo} className="logo react" alt="React logo" />
  //      </a>
  //    </div>
  //    <h1>Vite + React + Assignment</h1>
  //    <div className="card">
  //      <button onClick={() => setCount((count) => count + 1)}>
  //        count is {count}
  //      </button>
  //      <p>
  //        Edit <code>src/App.jsx</code> and save to test HMR
  //      </p>
  //    </div>
  //    <p className="read-the-docs">
  //      Click on the Vite and React logos to learn more
  //    </p>

  //  </>
    //)

        const [data, setData] = useState([]);
        const [loading, setLoading] = useState(true);

        useEffect(() => {
            fetchData();
        }, []);

        const fetchData = async () => {
            try {
                const response = await fetch('https://localhost:7051/api/temperature-monitoring');
                if (!response.ok) {
                    throw new Error('Failed to fetch data');
                }
                const jsonData = await response.json();
                setData(jsonData);
                setLoading(false);
            } catch (error) {
                console.error('Error fetching data:', error);
                setLoading(false);
            }
        };

        if (loading) {
            return <div>Loading...</div>;
        }
        return (
            <div>
                <h1>Data from Database</h1>
                <table>
                <thead>
                    <tr>
                        <th>Sensor ID</th>
                        <th>Timestamp</th>
                        <th>Temperature</th>
                        <th>Warning</th>
                    </tr>
                    </thead>
                <tbody>
                    {data.map((item,index) => (
                        <tr key={index}>
                            <td>{ item.sensorId}</td>
                            <td >{ item.timestamp}</td>
                            <td >{ item.temperature}</td>
                            <td >{item.triggerThresholdWarning?"True":"False"}</td>
                        </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        );


}

export default App
