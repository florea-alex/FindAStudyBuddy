import React from 'react'
import {Link} from 'react-router-dom'

function Header() {
  return (
    <div className='header'>
        <h1>&nbsp;&nbsp;&nbsp;<Link to ="/" className='text-link'>Home</Link></h1>
        <h1><Link to ="/dashboard" className='text-link'>Dashboard</Link></h1>
        <img className="photo" src={require('../assets/logo.png')} />
        <h1><Link to ="/profile" className='text-link'>Profile</Link></h1>
        <h1><Link to ="/login" className='text-link'>Login</Link>&nbsp;&nbsp;&nbsp;</h1>
            {/* <h1><Link to ="/register" className='text-link'>Register</Link></h1> */}
        
    </div>
  )
}

export default Header