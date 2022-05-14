import React, { useState } from "react";
import ReactDOM from "react-dom";
import Login from './components/Login'
import Register from './components/Register'
import Header from './components/Header'
import Home from './components/Home'
import PageNotFound  from "./components/404";
import Dashboard from "./components/Dashboard";
import { Route, Routes} from 'react-router-dom';
import ResetPassword from './components/ResetPassword'
import './App.css';
import Profile from "./components/Profile";
import Courses from "./components/Courses"
import Profile2 from "./components/Profile2";
import Logout from "./components/Logout";
import Addcourseneed from "./components/AddCourseNeed";
import Addcourseoffer from "./components/AddCourseOffer";

function App() {
  return (
    <div className="app">
      <Header />
      <Routes>
        <Route path="/" element={<Home/>} />
        <Route path='/login' element={<Login />} />
        <Route path='/register' element={<Register />} />
        <Route path='/resetpassword' element={<ResetPassword/>} />
        <Route path='/dashboard' element={<Dashboard />} />
        <Route path='/courses' element={<Courses />} />
        <Route path='/addcourseneed' element={<Addcourseneed/>}/>
        <Route path='/addcourseoffer' element={<Addcourseoffer/>}/>
        <Route path='/profile' element={<Profile2 />} />
        <Route path="*" element={<PageNotFound/>} />
      </Routes>
      <Logout />
    </div>
  );
}
 
export default App;