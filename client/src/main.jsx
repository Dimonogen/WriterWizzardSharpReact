import React, {createContext, StrictMode} from 'react'
import { createRoot } from 'react-dom/client'
import ContextApp from "./ContextApp.jsx";

createRoot(document.getElementById('root')).render(
  <StrictMode>
    <ContextApp />
  </StrictMode>,
)
