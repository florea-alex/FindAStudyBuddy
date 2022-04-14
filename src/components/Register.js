import React, { useState } from "react";
import ReactDOM from "react-dom";
import validator from 'validator'

function Register() {
  // React States
  const [errorMessages, setErrorMessages] = useState({});
  const [isSubmitted, setIsSubmitted] = useState(false);

  // User Login info
  const database = [
    {
      username: "user1",
      password: "pass1"
    },
    {
      username: "user2",
      password: "pass2"
    }
  ];

  const errors = {
    email: "e-mail is not valid",
    pass: "password is not strong enough"
  };

  const handleSubmit = (event) => {
    //Prevent page reload
    event.preventDefault();

    var { uname, email, pass, name, faculty} = document.forms[0];

     if (!validator.isEmail(email.value))
      // Username not found
      setErrorMessages({ name: "email", message: errors.email });
        else
      if (!validator.isStrongPassword(pass.value)) {
        // Invalid password
        setErrorMessages({ name: "pass", message: errors.pass });
      }
else {
    database.push({
        username: uname.value,
        password: pass.value
    });
    console.log(database);
    setIsSubmitted(true);
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
          <input type="text" name="uname" required />
        </div>
        <div className="input-container">
          <label>Password </label>
          <input type="password" name="pass" required />
          {renderErrorMessage("pass")}
        </div>
        <div className="input-container">
          <label>Name </label>
          <input type="text" name="name" required />
        </div>
        <div className="input-container">
          <label>Faculty </label>
          <input type="text" name="faculty" required />
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
        {isSubmitted ? <h2>User is successfully registered!</h2> : renderForm}
      </div>
    </div>
  );
}
 
export default Register;