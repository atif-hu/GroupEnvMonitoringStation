import React from 'react';
import PropTypes from 'prop-types';
import { Line } from 'react-chartjs-2';

import { Typography } from '@mui/material';

export default function Chart ({ title, data }){
    <div>
        <Typography variant="h5">{title}</Typography>
        <Line data={data} />
    </div>
};

Chart.propTypes = {
    title: PropTypes.string.isRequired,
    data: PropTypes.object.isRequired,
};

