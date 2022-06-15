import React from 'react'

function HomeDescription() {
  var flag = localStorage.getItem("isAuthenticated");
  return (
    <div >
    <br></br>
    <br></br>
    <br></br>
    <br></br>
    <div className='homedescription'>
      <h2 className='desctext'>Welcome to the Find a Study Buddy app!</h2>
      <br></br>
      <br></br>
      <div>Here, you will find all the help you need to make your student life</div>
        <div>
          easier and more enjoyable.</div>
          <br></br>
          <br></br>
        {flag == "true" ? 
        <div>
          Go to <a style={{textDecoration: "none"}} href="/dashboard">dashboard</a> to find the connections that suit you the best.
        </div>
        :
        <div>
          <div>
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Please log in or register with an existing account 
          </div>
          <div>
          in order to access the app and connect with your peers!
        </div>
        </div>
        }
        </div>
  </div>
  )
}

export default HomeDescription