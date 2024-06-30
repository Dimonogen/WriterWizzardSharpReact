import React, { Component, useContext } from 'react';
import {Routes, Route, Navigate} from 'react-router-dom'
import { authRoutes, publicRoutes } from '../../routes';
import { BASE_ROUTE } from '../../utils/consts';
import {Context} from '../../index'
import NavBar from "./NavBar";

const AppRouter = () => {
  const {user} = useContext(Context)
    return (
      <Routes>
        {user.isAuth && authRoutes.map(({path, Component}) => 
            <Route element={<NavBar />}>
              <Route key = {path} path = {path} element = {<Component/>} exact/>
            </Route>
        )}
        {publicRoutes.map(({path, Component}) =>
            <Route element={<NavBar />}>
              <Route key = {path} path = {path} element = {<Component/>} exact/>
            </Route>

        )}
        <Route path='*' element={<Navigate to={BASE_ROUTE}/>}/>
      </Routes>
    );
  };
  
export default AppRouter;