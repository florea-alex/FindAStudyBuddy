import React from 'react'
import Main from './SwipeCarousel/Main';
import {RemoveScrollBar} from 'react-remove-scroll-bar';

function Dashboard() {
  const imageClick = () => {
    window.location.pathname='/chat';
  }
  console.log(localStorage);
  var flag = localStorage.getItem("isAuthenticated");
  return (
    <div>
    {flag == "true" ?
    <div className='swipe'>
    <RemoveScrollBar />
    <Main />
    
    </div>
    :
    <div className='homedescription'>
      <h1 className='desctext'>Dashboard</h1>
      <br></br>
      <br></br>
      <div>Please log in to see the content of the app.</div>
    </div>
    }
    {flag == "true" &&
    <div>
      <img  style={{cursor: "pointer"}} className="chat" src={require('../assets/chat.png')} onClick={() => imageClick()}/>
    </div>
    }
    </div>
  )
}

export default Dashboard