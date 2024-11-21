import {Button, Image} from "react-bootstrap";
import {NavLink, useNavigate} from "react-router-dom";
import {CONFIG_ROUTE, LOGIN_ROUTE, MENU_ROUTE} from "../utils/consts";
import logo from '../assets/DS_logo_light_E.svg'
import {useContext, useEffect, useState} from "react";
import {Context} from "../index";
import {getUserSettings} from "../http/UserSettingsAPI";


const HomePage = () => {

    const navigate = useNavigate()
    const {user} = useContext(Context)

    const [last, SetLast] = useState({name:"", path:""})

    useEffect(() => {
        getUserSettings("LastLocation").then(data => SetLast(data))
    }, [])

    const arr = [
        {id: 1, name: "Меню объектов", path: MENU_ROUTE, title: "В основном меню находятся все объекты базы знаний. Это могут быть персонажи, локации, событии и всё, что угодно."},
        {id: 2, name: "Настройки", path: CONFIG_ROUTE, title: "В меню настроек можно добавить новые типы или настроить существующие"}
    ]

    return(
        <div className='MContent'>
            <div className='Block mb-5'>
                <div className='d-flex'>
                    <div className='ms-auto me-auto'>
                        <span className='fs-2'>Вы остановились на <NavLink className='alter' to={last.path}>{"< "+last.name+" >"}</NavLink></span>
                    </div>
                </div>
                <div className='d-flex'>
                    <Button className='fs-3 ms-auto me-auto' variant='outline-dark' onClick={() => navigate(last.path)}>Продолжить</Button>
                </div>


            </div>
            <div className='W-100 d-flex justify-content-center align-content-center'>
                {
                    arr.map(e =>
                        <div key={e.id} className='d-inline-block W-40 mt-auto mb-auto Block ms-auto me-auto'>
                            <div className='d-flex flex-column p-3'>
                                <div className='mb-3 fs-2 ms-auto me-auto'>{e.name}</div>
                                <div className="mb-3"><span className='fs-4'>{e.title}</span></div>
                                <Button variant="outline-dark" className='fs-4 ' onClick={() => {
                                    navigate(e.path)
                                }}>Открыть</Button>
                            </div>
                        </div>
                    )
                }
            </div>
        </div>
    )
}

export default HomePage