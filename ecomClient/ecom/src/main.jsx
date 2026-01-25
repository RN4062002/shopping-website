import React from 'react'
import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import { AuthContextProvider } from './contexts/authContext.jsx'
import './index.css'
import App from './App.jsx'

createRoot(document.getElementById('root')).render(
  <AuthContextProvider>
  <StrictMode>
    <App />
  </StrictMode>,
  </AuthContextProvider>
)
