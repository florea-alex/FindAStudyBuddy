import React from 'react'
import {Link} from 'react-router-dom'

function Header() {
  return (
    <div className='header'>
        <h1><Link to ="/" className='text-link'>&nbsp;&nbsp;Home</Link></h1>
        <div className='header-element'>
            <h1><Link to ="/login" className='text-link'>Login</Link></h1>
            <h1><Link to ="/register" className='text-link'>Register</Link></h1>
        </div>
    </div>
  )
}

export default Header