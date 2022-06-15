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
import Courses from "./components/Courses"
import Profile2 from "./components/Profile2";
import Addcourseneed from "./components/AddCourseNeed";
import Addcourseoffer from "./components/AddCourseOffer";
import Chat from "./components/Chat";
import Test from "./components/Test";

function App() {
  return (
    <div className="app">
      <Header />
      <Routes>
        <Route path="/" element={<Home/>} />
        <Route path='/login' element={<Login />} />
        <Route path='/test' element={<Test />} />
        <Route path='/register' element={<Register />} />
        <Route path='/resetpassword' element={<ResetPassword/>} />
        <Route path='/dashboard' element={<Dashboard />} />
        <Route path='/courses' element={<Courses />} />
        <Route path='/chat' element={<Chat />} />
        <Route path='/addcourseneed' element={<Addcourseneed/>}/>
        <Route path='/addcourseoffer' element={<Addcourseoffer/>}/>
        <Route path='/profile' element={<Profile2 />} />
        <Route path="*" element={<PageNotFound/>} />
      </Routes>
    </div>
  );
}
 
export default App;