import React from 'react'
import { useState } from 'react'
import './App.css'
import Navbar from './components/Home/Navbar'
import Home from './components/Home/Home'
import Login from './components/auth/Login'
import Register from './components/auth/Register'
import Products from './components/Products/Products'
import ProductList from './components/Products/ProductList'
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import ProtectedRoute from './routes/ProtectedRoute'
import ProductOverView from './components/products/ProductOverView'

function App() {

  return (
    <BrowserRouter>
      <Navbar />
      <Routes>
      <Route path="/" element={<Home />} />
        <Route path="/Login" element={<Login />} />
        <Route path="/Register" element={<Register />} />
        <Route path="/ProductList" element={
          <ProtectedRoute roles={["Customer","Admin"]}>
            <ProductList />
          </ProtectedRoute>
        }
        />
        <Route
          path="/Admin/ProductList" element={
            <ProtectedRoute roles={["Admin"]}>
              <Products />
            </ProtectedRoute>
          }
        />
      <Route path="/ProductOverView" element={<ProductOverView />} />
      </Routes>
    </BrowserRouter>
  )
}

export default App
