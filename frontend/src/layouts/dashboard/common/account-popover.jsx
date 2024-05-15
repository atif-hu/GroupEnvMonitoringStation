import { useState, useEffect } from 'react';

import Box from '@mui/material/Box';
import Avatar from '@mui/material/Avatar';
import Divider from '@mui/material/Divider';
import Popover from '@mui/material/Popover';
import { alpha } from '@mui/material/styles';
import MenuItem from '@mui/material/MenuItem';
import Typography from '@mui/material/Typography';
import IconButton from '@mui/material/IconButton';

import { useRouter } from 'src/routes/hooks';

import { account, fetchUser } from 'src/_mock/account';
import UserProfileModal from 'src/modals/UserProfileModal';


// ----------------------------------------------------------------------

const MENU_OPTIONS = [
  {
    label: 'Home',
    icon: 'eva:home-fill',
    action: 'home'
  },
  {
    label: 'Profile',
    icon: 'eva:person-fill',
    action: 'profile'
  },
  {
    label: 'Settings',
    icon: 'eva:settings-2-fill',
    action: 'settings'
  },
];

// ----------------------------------------------------------------------

export default function AccountPopover() {
  const [open, setOpen] = useState(null);
  const [userData, setUserData] = useState('');
  const [openModal, setOpenModal] = useState(false); // State for modal
  const router = useRouter();

  const handleOpen = (event) => {
    setOpen(event.currentTarget);
  };

  const handleClose = () => {
    setOpen(null);
  };
  

  useEffect(() => {
    if(document.cookie==='') router.push('/login')
    const fetchData = async () => {
      const data = await fetchUser();
      setUserData(data);
    };

    fetchData();
  });

  const handleMenuItemClick = (action) => {
    if (action === 'profile') {
      setOpenModal(true); // Open modal when clicking on "Profile"
      handleClose(); // Close popover
    }
  };

  const handleCloseModal = () => {
    setOpenModal(false);
  };

  const handleLogout = () => {
    setOpenModal(false);
    document.cookie = `access_token=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;`;
    document.cookie = `user_id=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;`;
    document.cookie = `username_initials=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;`;
    setUserData(null);
    router.push('/login')
  };

  

  return (
    <>
      <IconButton
        onClick={handleOpen}
        sx={{
          width: 40,
          height: 40,
          background: (theme) => alpha(theme.palette.grey[500], 0.08),
          ...(open && {
            background: (theme) =>
              `linear-gradient(135deg, ${theme.palette.primary.light} 0%, ${theme.palette.primary.main} 100%)`,
          }),
        }}
      >
        <Avatar
          src={account.initials}
          alt={account.initials}
          sx={{
            width: 40,
            height: 40,
            border: (theme) => `solid 0px`,
            bgcolor: '#1877F2'
          }}
        >
          {userData?`${userData.firstName[0].toUpperCase()}${userData.lastName[0].toUpperCase()}`:''}
        </Avatar>
      </IconButton>

      <Popover
        open={!!open}
        anchorEl={open}
        onClose={handleClose}
        anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }}
        transformOrigin={{ vertical: 'top', horizontal: 'right' }}
        PaperProps={{
          sx: {
            p: 0,
            mt: 1,
            ml: 0.75,
            width: 200,
          },
        }}
      >
        <Box sx={{ my: 1.5, px: 2 }}>
          <Typography variant="subtitle2" noWrap>
          {userData.firstName} {userData.lastName}
          </Typography>
          <Typography variant="body2" sx={{ color: 'text.secondary' }} noWrap>
            {userData.email}
          </Typography>
        </Box>

        <Divider sx={{ borderStyle: 'dashed' }} />

        {MENU_OPTIONS.map((option) => (
          <MenuItem key={option.label} onClick={() => handleMenuItemClick(option.action)}>
            {option.label}
          </MenuItem>
        ))}

        <Divider sx={{ borderStyle: 'dashed', m: 0 }} />

        <MenuItem
          disableRipple
          disableTouchRipple
          onClick={handleLogout}
          sx={{ typography: 'body2', color: 'error.main', py: 1.5 }}
        >
          Logout
        </MenuItem>
      </Popover>

      <UserProfileModal open={openModal} onClose={handleCloseModal} userData={userData} userId = {userData.id} />
 
    </>
  );
}
