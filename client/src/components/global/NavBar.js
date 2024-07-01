import React, {useContext, useEffect, useState} from 'react'
import { Context } from '../../index'
import {Form, Navbar, Nav, Container, Button, Image, NavDropdown} from 'react-bootstrap'
import { NavLink, useNavigate } from 'react-router-dom'
import {
    ADMIN_ROUTE,
    LOGIN_ROUTE,
    USER_ROUTE,
    ACCOUNT_ROUTE,
    BASE_ROUTE, MENU_ROUTE, CONFIG_ROUTE
} from '../../utils/consts'

import logo from '../../assets/DS_logo_dark_E.svg'
import iconMenu from '../../assets/MobileMenu.svg'
import {observer} from "mobx-react-lite";
import ModalLeftMenuMobile from "../modals/ModalLeftMenuMobile";
//import Nav from 'react-bootstrap/Navbar'
//import  from 'react-bootstrap/Navbar'

const NavBar = observer(() => {
    const {user} = useContext(Context)
    const navigate = useNavigate()

    let DDactive = false;

    //const [userLogo, SetUserLogo] = useState('iconNotFound.png');

    const [show, setShow] = useState(false);
    const showDropdown = (e) => {
        //console.log('test')
        //if(user.info)
        {
            setShow(true);
            DDactive = true;
        }
    }
    const hideDropdown = (e) => {
        DDactive = false;
        setTimeout(()=>{
            if(!DDactive)
                setShow(false);
        }, 400)
    }

    const logOut = () => {
        setShow(false);
      user.logOut();
      navigate(BASE_ROUTE);
    }

    const [showMenu, setshowMenu] = useState(false)

    useEffect(()=>{
        //console.log(user.info)
        //setTimeout(()=>{
        //    SetUserLogo(user.info.icon);
        //}, 1000)

    },[user])

    //console.log(user)

    return (
      <Navbar fixed="top" bg="dark" variant="dark">
        <div className='d-flex W-100 flex-wrap '>
          <div className='d-flex flex-wrap ms-3 m-2 '>
              <div className='NavBarLeftIcon' style={{cursor:'pointer'}}
                   onClick={() => navigate(BASE_ROUTE)}
              >
                  <Image width={64} height={64} src={logo} style={{position:'absolute', top:'3px', left:'20px'}}/>
              </div>

              <div className='NavBarLeftMenu ms-1' style={{cursor:'pointer'}} onClick={() => {setshowMenu(true)}}>
                  <Image width={32} height={32} src={iconMenu}/>
              </div>
              <ModalLeftMenuMobile onHide={() => setshowMenu(false)} show={showMenu}/>
              <div className='NavBarLeftDiv flex-row'>
                  <div className='ms-5 me-4'></div>
                  <span className='fs-4 me-2 text-light'>База знаний писателя</span>
                  {
                      user.isAuth ?
                      <div>
                          <Button variant='outline-light' onClick={()=>navigate(MENU_ROUTE)}>Объекты</Button>
                          <Button variant='outline-light ms-2' onClick={()=>navigate(CONFIG_ROUTE)}>Настройки</Button>
                      </div>
                          : null
                  }

              </div>


          </div>

            {/*<Form className='ms-auto me-auto'>
            <Form.Control style={{color: '#ccc', background: '0', border: '0', borderBottom: '1px solid #ccc'}}
              placeholder={"Поиск"}
            />
          </Form>*/}
          {user.info && user.isAuth ?
            <Nav className="ms-auto me-3">
              <span className='m-2 text-white d-flex align-items-center'>{user.info.name}</span>
                {user.user.role === 'ADMIN'
                    ?
                    <Button className='m-2' variant={'outline-light'} onClick={() => navigate(ADMIN_ROUTE)}>Админ
                        панель</Button>
                    :
                    null
                }
                <NavDropdown align={'end'} title={<Image width={36} height={36} src={process.env.REACT_APP_STATIC_URL+
                    //user.info ? user.info.icon : 'iconNotFound.png'}/>
                    user.icon}/>
                }
                 show={show}
                 onMouseEnter={showDropdown}
                 onMouseLeave={hideDropdown}
                >
                    {
                        /*<NavDropdown.Item href={"/user/"+user.info.id}>Профиль</NavDropdown.Item>*/
                    }
                    <NavDropdown.Item onClick={() => navigate(USER_ROUTE+'/'+user.info.id)} >Мой профиль</NavDropdown.Item>
                    <NavDropdown.Item onClick={() => navigate(ACCOUNT_ROUTE)} >Личный кабинет</NavDropdown.Item>
                    <NavDropdown.Divider />
                    <NavDropdown.Item onClick={() => logOut()}>Выход</NavDropdown.Item>
                </NavDropdown>
            </Nav>
            :
            <Nav className="ms-auto me-3">
              <span className='m-2 text-white d-flex align-items-center'>Не авторизован</span>
              <Button className='m-2' variant={'outline-light'} onClick={() => navigate(LOGIN_ROUTE)}>Войти</Button>
            </Nav>
            }
          </div>
        </Navbar>
  )
});

export default NavBar