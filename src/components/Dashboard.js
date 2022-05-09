import React from 'react'

function Dashboard() {

  const handleLogout = (event) => {
    localStorage.clear();
    window.location.pathname = "/";
    console.log(localStorage);
  }

  var flag = localStorage.getItem("isAuthenticated");

  return (
    <div >
    <br></br>
    <br></br>
    <br></br>
    <br></br>
    {flag == "true" ?
    <div className='homedescription'>
      <h1 className='desctext'>Dashboard</h1>
      <br></br>
      <br></br>
      <div>Here, you will find all the help you need to make your student life</div>
        <div>
          easier and more enjoyable.
        </div>
          <br></br>
          <br></br>
          <div className="button-container">
          <button className="button-logout" onClick={handleLogout}>Logout</button>
          </div>
    </div>
    :
    <div className='homedescription'>
      <h1 className='desctext'>Dashboard</h1>
      <br></br>
      <br></br>
      <div>Please log in to see the content of the app.</div>
    </div>
    }
    
        
  </div>
  )
}

export default Dashboard