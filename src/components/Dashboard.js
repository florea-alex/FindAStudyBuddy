import React from 'react'
import Main from './SwipeCarousel/Main';
import {RemoveScrollBar} from 'react-remove-scroll-bar';

function Dashboard() {
  const imageClick = () => {
    window.location.pathname='/chat';
  }
  var flag = localStorage.getItem("isAuthenticated");
  return (
    <div>
    <div className='swipe'>
      <RemoveScrollBar />
      <Main />
    {/* <br></br>
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
    </div>
    :
    <div className='homedescription'>
      <h1 className='desctext'>Dashboard</h1>
      <br></br>
      <br></br>
      <div>Please log in to see the content of the app.</div>
    </div>
    } */}
  
  </div>
  <img  style={{cursor: "pointer"}} className="chat" src={require('../assets/chat.png')} onClick={() => imageClick()}/>
  </div>
  )
}

export default Dashboard