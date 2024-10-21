import './App.css';
import './CSS/Book.css';
import './CSS/anim.css';
import {BrowserRouter, Navigate, Route, Routes} from 'react-router-dom';
import AppRouter from './components/global/AppRouter';
import NavBar from './components/global/NavBar';
import { observer } from 'mobx-react-lite';
import React, { useContext, useEffect, useState } from 'react';
import { Context } from './index';
import {check, getInfo, getRights} from './http/userAPI';
import { Spinner } from 'react-bootstrap';
import UnderBar from "./components/global/UnderBar";
import {authRoutes, noBarsRoutes, publicRoutes} from "./routes";
import {BASE_ROUTE} from "./utils/consts";
import AuthComponent from "./components/global/AuthComponent";
import HistoryBar from "./components/global/HistoryBar";
import LoadingAnimComponent from "./components/global/LoadingAnimComponent";

const App = observer( () => {
  const {user} = useContext(Context)
  const [loading, setLoading] = useState(true)

  useEffect( () => {
      //localStorage.removeItem('token')
      //console.log(localStorage.getItem('token'))

      //console.log(user.info)
      //console.log(user.icon)
      //console.log(user.isAuth)

      if(localStorage.getItem('token') != null)
       check().then(data => {
       user.setUser(data)
       user.setIsAuth(true)
       getInfo(user.user.id).then(data => user.setInfo(data))
           //getRights().then(data => user.setRights(data))
     }).catch(e => console.log(e))
       .finally(()=>setLoading(false))
      else
      {
          setLoading(false)
      }
      //setLoading(false)
  }, [user])


  if(loading){
    return <LoadingAnimComponent/>
  }

  return (
    <BrowserRouter>
        <Routes>
            {authRoutes.map(({path, Component}) =>
                <Route key = {path} path = {path} element = {<div><NavBar/><HistoryBar/><AuthComponent child={<Component/>}/><UnderBar/></div>} exact/>
            )}
            {publicRoutes.map(({path, Component}) =>
                <Route key = {path} path = {path} element = {<div><NavBar/><Component/><UnderBar/></div>} exact/>
            )}
            {/*noBarsRoutes.map( ({path, Component}) =>
                <Route key = {path} path={path} element={<Component/>} exact/>
            )*/}
            <Route path='*' element={<Navigate to={BASE_ROUTE}/>}/>
        </Routes>
    </BrowserRouter>
  );
})

export default App;
/*
*/