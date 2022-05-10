import React from 'react'

function Logout() {
    const handleLogout = (event) => {
        localStorage.clear();
        window.location.pathname = "/";
        console.log(localStorage);
      }
      //localStorage.setItem("isAuthenticated", "true");

      var flag = localStorage.getItem("isAuthenticated");
  return (
        <div className='logout'>
          <button className="button-logout" onClick={handleLogout}>Logout</button>
        </div>
  )
}

export default Logout