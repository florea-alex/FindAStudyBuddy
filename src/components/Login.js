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
   
    axios.post('https://findastudybuddy.azurewebsites.net/api/Auth/Login', object)
       .then(response => {
         console.log(response)
            if (response.status != 200) {
                alert("There was a problem with the registration. Please try again.");
              }
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
  };

  // Generate JSX code for error message
  const renderErrorMessage = (name) =>
    name === errorMessages.name && (
      <div className="error">{errorMessages.message}</div>
    );

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
        <div className="button-container">
          <input type="submit" />
        </div>
      </form>
    </div>
  );

  const navigate = useNavigate();

  return (
      <div className='login-page'>
        <br></br>
        <br></br>
        <br></br>
        <br></br>
        <br></br>
        <br></br>
        <br></br>
        <br></br>
    <div className="login-form">
        <h1 className="title">Log In</h1>
        {isSubmitted ? navigate("/dashboard") : renderForm}
      </div>
      </div>
  )
}

export default Login