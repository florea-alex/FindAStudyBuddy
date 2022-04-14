import React, { useState } from "react";
import ReactDOM from "react-dom";
import Login from './components/Login'
import Register from './components/Register'
import Header from './components/Header'
import Home from './components/Home'
import PageNotFound  from "./components/404";
import Dashboard from "./components/Dashboard";
import { Route, Routes} from 'react-router-dom';
import './App.css';

function App() {
  return (
    <div className="app">
      <Header />
      <Routes>
        <Route path="/" element={<Home/>} />
        <Route path='/login' element={<Login />} />
        <Route path='/register' element={<Register />} />
        <Route path='/dashboard' element={<Dashboard />} />
        <Route path="*" element={<PageNotFound/>} />
      </Routes>
    </div>
  );
}
 
export default App;