import React, { useContext, useState } from 'react';
import { Context } from '../index'
import {Container, Form, Card, Button} from 'react-bootstrap';
import {NavLink, useLocation, useNavigate, useParams, useSearchParams} from 'react-router-dom'
import {getInfo, getRights, login, registration} from '../http/userAPI';
import {LOGIN_ROUTE, REGISTRATION_ROUTE, BASE_ROUTE, MENU_ROUTE, HOME_ROUTE} from '../utils/consts'
import {observer} from 'mobx-react-lite'
import MenuPage from "./MenuPage";
import ModalOkMy from "../components/modals/ModalOkMy";

const Auth = observer( () => {
    const {user} = useContext(Context)
    const navigate = useNavigate()
    const location = useLocation()
    const isLogin = location.pathname === LOGIN_ROUTE
    const [email, setEmail] = useState('')
    const [password, setPassword] = useState('')
    const [name, setName] = useState('')
    const [projectName, setProjectName] = useState('')

    const params = new URLSearchParams( window.location.search);
    const path = params.get("path");
//console.log(params, path);

    const [messageText, SetMessageText] = useState('Ошибка.')
    const [showError, SetShowError] = useState(false);

    const click = async () => {

      try{
        let user_l;

        if (isLogin)
        {
          user_l = await login(email, password);
          //console.log(user_l)
        }
        else{
          user_l = await registration({email, name, password, projectName});
        }

        if(user_l.haveError) {
            SetMessageText(user_l.message)
            SetShowError(true);
            //alert("Неправильная комбинация логина и пароля");
            return
        }

        user.setUser(user_l.token)
        user.setIsAuth(true)
        //console.log(user.user.id)
        getInfo(user.user.id).then(data => {
            user.setInfo(data);
            if (path == undefined)
                navigate(HOME_ROUTE)
            else {
                //console.log(path, decodeURIComponent((path)))
                navigate(path)
            }
        })
        //getRights().then(data => {user.setRights(data);navigate(MENU_ROUTE)})
      } catch(e){
        console.log(e);
        alert(e.response.data.message)
      }
      
    }

    return (
      <Container className='d-flex justify-content-center align-items-center'
      style={{height: window.innerHeight-54}}>
        <Card style={{width: 600}} className='p-5'>
          <h2 className='m-auto'>{isLogin ? 'Авторизация':'Регистрация'}</h2>
          <Form className='d-flex flex-column'>
            <Form.Control 
              className='mt-3'
              placeholder='Введите ваш login...'
              value={email}
              onChange={e => setEmail(e.target.value)}
            />
            {isLogin ?
            ''
                :
                <div>
                    <Form.Control
                        className="mt-3"
                        placeholder="Введите имя пользователя"
                        value={name}
                        onChange={e => setName(e.target.value)}
                    />
                    <Form.Control
                        className="mt-3"
                        placeholder="Введите название проекта"
                        value={projectName}
                        onChange={e => setProjectName(e.target.value)}
                    />
                </div>

            }
            <Form.Control 
              className='mt-3'
              placeholder='Введите ваш пароль...'
              value={password}
              onChange={e => setPassword(e.target.value)}
              type={isLogin ? 'password' :''}
            />
            <div className='d-flex justify-content-between mt-3 pl-3 pr-3'>
              {isLogin ?
                <div>
                  Нет аккаунта? <NavLink className="alter" to={REGISTRATION_ROUTE}>Регистрация</NavLink>
                </div>
                :
                <div>
                  Есть аккаунт? <NavLink className="alter" to={LOGIN_ROUTE}>Авторизация</NavLink>
                </div>
              }
              <Button
                variant={'outline-success'}
                onClick={click}
              >
                {isLogin?
                  'Войти':'Регистрация'
                }
              </Button>
            </div>
          </Form>
        </Card>
          <ModalOkMy show={showError} onHide={() => SetShowError(false)} title={messageText} okTitle='Ок' />
      </Container>
    );
  })
  
export default Auth;