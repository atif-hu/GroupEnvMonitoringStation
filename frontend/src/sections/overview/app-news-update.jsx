import PropTypes from 'prop-types';

import Box from '@mui/material/Box';
import Link from '@mui/material/Link';
import Card from '@mui/material/Card';
import Stack from '@mui/material/Stack';
import Button from '@mui/material/Button';
import Divider from '@mui/material/Divider';
import Typography from '@mui/material/Typography';
import CardHeader from '@mui/material/CardHeader';

import { fToNow } from 'src/utils/format-time';

import Iconify from 'src/components/iconify';
import Scrollbar from 'src/components/scrollbar';

// ----------------------------------------------------------------------

export default function AppNewsUpdate({ title, subheader, list, list1, list2, list3, list4, ...other }) {
  return (
    <Card {...other}>
      <CardHeader title={title} subheader={subheader}/>

      <Scrollbar>
        <Stack spacing={3} sx={{ p: 3, pr: 0, }} >
          {list.map((news) => (
            <NewsItem key={news.id} news={news} highWarning={news.highWarning} lowWarning={news.lowWarning} />
          ))}
        </Stack>
        <Stack spacing={3} sx={{ p: 3, pr: 0, pt:0 }}>
          {list1.map((news) => (
            <NewsItem key={news.id} news={news} />
          ))}
        </Stack>
        <Stack spacing={3} sx={{ p: 3, pr: 0, pt:0 }}>
          {list2.map((news) => (
            <NewsItem key={news.id} news={news} />
          ))}
        </Stack>
        <Stack spacing={3} sx={{ p: 3, pr: 0, pt:0 }}>
          {list3.map((news) => (
            <NewsItem key={news.id} news={news} />
          ))}
        </Stack>
        <Stack spacing={3} sx={{ p: 3, pr: 0, pt:0 }}>
          {list4.map((news) => (
            <NewsItem key={news.id} news={news} />
          ))}
        </Stack>
      </Scrollbar>

      <Divider sx={{ borderStyle: 'dashed' }} />

      <Box sx={{ p: 2, textAlign: 'right' }}>
        <Button
          size="small"
          color="inherit"
          endIcon={<Iconify icon="eva:arrow-ios-forward-fill" />}
        >
          View all
        </Button>
      </Box>
    </Card>
  );
}

AppNewsUpdate.propTypes = {
  title: PropTypes.string,
  subheader: PropTypes.string,
  list: PropTypes.array.isRequired,
  list1: PropTypes.array.isRequired,
  list2: PropTypes.array.isRequired,
  list3: PropTypes.array.isRequired,
  list4: PropTypes.array.isRequired,
};

// ----------------------------------------------------------------------

function NewsItem({ news }) {
  const { image, title, description, postedAt, highWarning, lowWarning, unit } = news;
  
  return (
    <Stack direction="row" alignItems="center" spacing={2} sx={{
      ...((highWarning || lowWarning) && {
        color: 'red',
        backgroundColor: '#ffcccc',
        borderRadius: '10px',
      }),
    }}>
      <Box
        component="img"
        alt={title}
        src={image}
        sx={{ width: 48, height: 48, borderRadius: 1.5, flexShrink: 0 }}
      />

      <Box sx={{ minWidth: 240, flexGrow: 1 }}>
        <Link color="inherit" variant="subtitle2" underline="none" noWrap>
          {title}
        </Link>

        <Typography variant="body2" sx={{ color: 'text.secondary' }} noWrap>
          {description} {unit}
        </Typography>
      </Box>

      {highWarning &&(
        <Typography variant="subtitle2" sx={{ color: 'red', fontWeight: 'bold' }}>
          HIGH {title.toUpperCase()} :     {description}
        </Typography>
      )}

      {lowWarning &&(
        <Typography variant="subtitle2" sx={{ color: 'red', fontWeight: 'bold' }}>
         LOW {title.toUpperCase()} :     {description}
        </Typography>
      )}

      <Typography variant="caption" sx={{ pr: 3, flexShrink: 0, color: 'text.secondary' }}>
        {fToNow(postedAt)}
      </Typography>
    </Stack>
  );
}

NewsItem.propTypes = {
  news: PropTypes.shape({
    image: PropTypes.string,
    title: PropTypes.string,
    description: PropTypes.string,
    postedAt: PropTypes.string,
    highWarning: PropTypes.bool,
    lowWarning: PropTypes.bool,
    unit: PropTypes.string,
  })
};
