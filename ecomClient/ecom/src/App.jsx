import React from 'react'
import { useState } from 'react'
import './App.css'
import Navbar from './components/Home/Navbar'
import Home from './components/Home/Home'
import { AuthContextProvider } from './contexts/authContext'
import Login from './components/auth/Login'
import Register from './components/auth/Register'
import Products from './components/products/Products'
import Cart from './components/products/Cart'
import ProductList from './components/Admin/ProductList'
import { BrowserRouter, Routes, Route } from 'react-router-dom';

function App() {

  return (
    <BrowserRouter>
    <Navbar/>
    <Routes>
      <Route path = "/" element={<Home/>}/>
      <Route path = "/Login" element={<Login/>}/>
      <Route path = "/Register" element={<Register/>}/>
      <Route path = "/Products" element={<Products/>}/>
      <Route path = "/Admin/ProductList" element={<ProductList/>}/>
      {/* <Route path = "/Products/Cart" element={<Cart/>}/> */}
    </Routes>
   </BrowserRouter>
  )
}

export default App
