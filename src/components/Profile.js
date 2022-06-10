import React from 'react'
import axios from 'axios';
function Profile() {
      var flag = localStorage.getItem("isAuthenticated");
      var id = localStorage.getItem("userId");
      var mesaj = true;
      var obj;
      var link = "https://findastudybuddymds.azurewebsites.net/api/Profile/GetById?userId="+1;

      axios.get(link)
      .then(response =>
        {
            console.log(response);
            obj = response.data;
        })
        .catch(error =>
        {
            console.log("aici");
            console.log(error);
            mesaj = false;
        })
      
      return (
        <div >
        <br></br>
        <br></br>
        <br></br>
        <br></br>
        {flag == "true" ?
        <div className='homedescription'>
          <h1 className='desctext'>Profile</h1>
          {}
        </div>
        :
        <div className='homedescription'>
          <h1 className='desctext'>Profile</h1>
          <br></br>
          <br></br>
          <div>Please log in to see the content of the app.</div>
        </div>
        }
        
            
      </div>
      )
}

export default Profile