import React from 'react'
import {Link} from 'react-router-dom'
import Popup from 'reactjs-popup';
import 'reactjs-popup/dist/index.css';
function Header() {
  const handleLogout = (event) => {
    localStorage.clear();
    window.location.pathname = "/";
    console.log(localStorage);
  }

  var flag = localStorage.getItem("isAuthenticated");
  return (
    <div className='header'>
        <h1>&nbsp;&nbsp;&nbsp;<Link to ="/" className='text-link'>Home</Link></h1>
        <Popup
              trigger={<h1><Link to ="/dashboard" className='text-link'>Dashboard</Link></h1>}
              modal
              nested
            >
              {close => (
                <div className="modal">
                  <button className="close" onClick={close}>
                    &times;
                  </button>
                  <div className="header1">
                    <h2>Welcome to your dashboard!</h2></div>
                  <div className='content'>
                  <h3>
                    If you want to connect with someone, swipe their card right.
                    <br></br>
                    Otherwise, swipe left.
                  </h3>
                  </div>
                  <div className="actions">
                    <button
                      className="button-logout5"
                      onClick={() => {
                        console.log('modal closed ');
                        close();
                      }}
                    >
                    Understood!
                    </button>
                  </div>
                </div>
              )}
            </Popup>
        {/* <h1><Link to ="/dashboard" className='text-link'>Dashboard</Link></h1> */}
        <img className="photo" src={require('../assets/logo.png')} />
        <h1><Link to ="/profile" className='text-link'>Profile</Link></h1>
        {flag == "true" ?
        <h1 style={{cursor: "pointer"}} onClick={handleLogout} className='text-link'>Logout&nbsp;&nbsp;&nbsp;</h1>
        :
        <h1><Link to ="/login" className='text-link'>Login</Link>&nbsp;&nbsp;&nbsp;</h1>
        } 
    </div>
  )
}

export default Header