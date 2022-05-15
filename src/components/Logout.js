import axios from 'axios';
import React from 'react'

function Logout() {
    const handleLogout = (event) => {
        localStorage.clear();
        window.location.pathname = "/";
        console.log(localStorage);
      }
      //localStorage.setItem("isAuthenticated", "true");

      const handleDelete = (event) => {        
        var id = localStorage.getItem("userId");
        localStorage.clear();
        axios.delete("https://findastudybuddy.azurewebsites.net/api/User/DeleteUser?id="+id)
        .then(response =>
          {
              console.log(response);
          })
          .catch(error =>
          {
              console.log(error);
          });
          setTimeout(() => {window.location.pathname = "/";}, 1000);
      }

      var flag = localStorage.getItem("isAuthenticated");
  return (
        <div className='logout'>
          <button className="button-logout3" onClick={handleLogout}>Logout</button>
          <button className="button-logout4" onClick={handleDelete}>Delete account</button>
        </div>
  )
}

export default Logout