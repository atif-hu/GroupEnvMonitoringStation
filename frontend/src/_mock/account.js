export const account = {
  initials : document.cookie!==''?document.cookie.split('; ').find(row => row.startsWith('username_initials')).split('=')[1]:'',
  displayName: 'Jaydon Frankie',
  email: 'demo@minimals.cc',
  photoURL: '/assets/images/avatars/avatar_25.jpg',
};

export const fetchUser = async()=>{
  let userId;

  if(document.cookie!==''){
    userId = document.cookie.split('; ').find(row => row.startsWith('user_id')).split('=')[1];
  }

  const response = await fetch(`https://localhost:7132/api/User/${userId}`);
  const data = await response.json();
  return data
}