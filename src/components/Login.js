import React from 'react'
import {useState} from 'react'
import { useNavigate } from "react-router-dom";
import Dashboard from './Dashboard';
import axios from 'axios';
function Login() {

    // React States
  const [errorMessages, setErrorMessages] = useState({});
  const [isSubmitted, setIsSubmitted] = useState(false);

  const handleSubmit = (event) => {
    //Prevent page reload
    event.preventDefault();

    var { uname, pass } = document.forms[0];

    const object = {
      userName: uname.value, 
      password: pass.value, 
    };
   
    axios.post('https://findastudybuddymds.azurewebsites.net/api/Auth/Login', object)
       .then(response => {
         console.log(response)
            if (response.status != 200) {
                alert("There was a problem with the registration. Please try again.");
              }
              console.log(response);
              localStorage.setItem("userId", response.data.id);
              localStorage.setItem("isAuthenticated", "true");
              console.log(localStorage)
              setIsSubmitted(true);
              //console.log(response)
              //console.log(response.status)
            })
        .catch(error => {
             console.log(error.response.data.message);
             alert("There was a problem with the registration. Please try again.\n" + "Error: " + error.response.data.message);
            })
        setTimeout(() => {window.location.pathname = "/profile";}, 1000);
  };

  // Generate JSX code for error message
  const renderErrorMessage = (name) =>
    name === errorMessages.name && (
      <div className="error">{errorMessages.message}</div>
    );

  const resetpass = (event) => {
      window.location.pathname = "/resetpassword";
    }
  const registerbut = (event) => {
      window.location.pathname = "/register";
    }

  // JSX code for login form
  const renderForm = (
    <div className="form">
      <form onSubmit={handleSubmit}>
        <div className="input-container">
          <label>E-mail </label>
          <input type="text" name="uname" required />
          {renderErrorMessage("uname")}
        </div>
        <div className="input-container">
          <label>Password </label>
          <input type="password" name="pass" required />
          {renderErrorMessage("pass")}
        </div>
        <br></br>
        <div className="button-container">
          <input type="submit" />
        </div>
      </form>
      <br></br>
      <div className="buttons-login">
      <button className="button-logout" onClick={resetpass}>Forgot password</button>
      <button className="button-logout" onClick={registerbut}>Register</button>
      </div>
    </div>
  );

  const navigate = useNavigate();
  
  var flag = localStorage.getItem("isAuthenticated");

  return (
      <div className='login-page'>
        <br></br>
        <br></br>
        <br></br>
        <br></br>
        <br></br>
    {flag == "true" ?
    <div className='homedescription'>
    <h1 className='desctext'>You are already logged in.</h1>
    </div>
    : 
    <div className="login-form">
        <h1 className="title">Log In</h1>
        {isSubmitted ? navigate("/dashboard") : renderForm}
      </div>
  }
      </div>
  )
}

export default Login