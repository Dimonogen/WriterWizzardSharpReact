import React, {useContext} from 'react'
import {Context} from "../../ContextApp";
import {NavLink, useNavigate} from "react-router-dom";
import {LOGIN_ROUTE, USER_ROUTE} from "../../utils/consts";
import {Button} from "react-bootstrap";

const AuthComponent = ({child}) => {

    const {user} = useContext(Context);
    const navigate = useNavigate();
    const path = window.location.pathname;

    if(user.isAuth)
        return child;
    else
        return <div className='MContent'>
            <div className=' fs-5'>Эта страница доступна только авторизованным пользователям.
                <Button className='ms-3 fs-6' onClick={() => {navigate(LOGIN_ROUTE+"?path="+encodeURIComponent(path) )} } variant='alter-dark'>Авторизация</Button>
            </div>
    </div>

}

export default AuthComponent