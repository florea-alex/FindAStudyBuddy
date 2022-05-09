import React, { useState } from "react";
import ReactDOM from "react-dom";
import validator from 'validator'
import axios from 'axios'

function Register() {
  // React States
  const [errorMessages, setErrorMessages] = useState({});
  const [isSubmitted, setIsSubmitted] = useState(false);

  // User Login info
  var message = "User was successfully registered.";

  const errors = {
    email: "E-mail is not valid",
    pass: "Password is not strong enough"
  };

  const handleSubmit = (event) => {
    //Prevent page reload
    event.preventDefault();

    var { userName, email, pass, firstName, lastName} = document.forms[0];

    const object = {
      userName: userName.value, 
      email: email.value, 
      firstName: firstName.value, 
      lastName: lastName.value,
      password: pass.value, 
      role: "user"
    };

     if (!validator.isEmail(email.value))
      // Username not found
      setErrorMessages({ name: "email", message: errors.email });
        else
      if (!validator.isStrongPassword(pass.value)) {
        // Invalid password
        setErrorMessages({ name: "pass", message: errors.pass });
      }
      else {

          axios.post('https://findastudybuddy.azurewebsites.net/api/Auth/Register', object)
            .then(response => {
              if (response.status != 200) {
                alert("There was a problem with the registration. Please try again.");
              }
              setIsSubmitted(true);
              //console.log(response)
              //console.log(response.status)
            })
            .catch(error => {
              console.log(error.response.data.message);
              alert("There was a problem with the registration. Please try again.\n" + "Error: " + error.response.data.message);
            })
          // database.push({
          //     username: userName.value,
          //     password: pass.value
          // });
          // console.log(database);
          
          }
  };

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
          <input type="text" name="email" required />
          {renderErrorMessage("email")}
        </div>
        <div className="input-container">
          <label>Username </label>
          <input type="text" name="userName" required />
        </div>
        <div className="input-container">
          <label>Password </label>
          <input type="password" name="pass" required />
          {renderErrorMessage("pass")}
        </div>
        <div className="input-container">
          <label>First name </label>
          <input type="text" name="firstName" required />
        </div>
        <div className="input-container">
          <label>Last name </label>
          <input type="text" name="lastName" required />
        </div>
        <div className="button-container">
          <input type="submit" />
        </div>
      </form>
    </div>
  );

  return (
    <div className="register">
              <br></br>
              <br></br>
              <br></br>
              <br></br>

      <div className="login-form">
        <h1 className="title">Register</h1>
        {isSubmitted ? <h2>{message}</h2> : renderForm}
      </div>
    </div>
  );
}
 
export default Register;