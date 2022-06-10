import React from 'react'
import {useState} from 'react'
import { useNavigate } from "react-router-dom";
import Dashboard from './Dashboard';
import axios from 'axios';
function ResetPassword() {

    // React States
  const [isSubmitted, setIsSubmitted] = useState(false);

  const handleSubmit = (event) => {
    //Prevent page reload
    event.preventDefault();

    var { uname, newPass, confirmNewPass } = document.forms[0];
    var token_res = null;

   const object = {
       email: uname.value
   }
    axios.post('https://findastudybuddymds.azurewebsites.net/api/Auth/reset-pass-token', object)
       .then(response => {
            token_res = response.data.message;
            const obj = {
                email: uname.value, 
                newPassword: newPass.value,
                confirmNewPassord: confirmNewPass.value,
                token: token_res
             };
             axios.post('https://findastudybuddymds.azurewebsites.net/api/Auth/Reset-Password', obj)
                .then(response => {
                    console.log(response);
                })
                .catch(error => {
                    console.log(error.response.data.message);
                    alert("There was a problem with the reset. Please try again.\n" + "Error: " + error.response.data.message);
                });
            })
        .catch(error => {
             console.log(error.response.data.message);
             alert("There was a problem with the reset. Please try again.\n" + "Error: " + error.response.data.message);
            });

    
    

  };

  // JSX code for login form
  const renderForm = (
    <div className="form">
      <form onSubmit={handleSubmit}>
        <div className="input-container">
          <label>E-mail </label>
          <input type="text" name="uname" required />
        </div>
        <div className="input-container">
          <label>New password</label>
          <input type="password" name="newPass" required />
        </div>
        <div className="input-container">
          <label>Confirm new password</label>
          <input type="password" name="confirmNewPass" required />
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
        <h1 className="title">Reset password</h1>
        {isSubmitted ? navigate("/dashboard") : renderForm}
      </div>
      </div>
  )
}

export default ResetPassword