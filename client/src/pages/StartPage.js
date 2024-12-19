import {Button, Image, OverlayTrigger, Tooltip} from "react-bootstrap";
import {useNavigate} from "react-router-dom";
import {LOGIN_ROUTE, MENU_ROUTE} from "../utils/consts";
import logo from '../assets/DS_logo_light_E.svg'
import React, {useContext} from "react";
import {Context} from "../index";
import iconCreate_B from "../assets/Create_B.svg";
import iconCreate_W from "../assets/Create_W.svg";
import iconReload_B from "../assets/Reload_B.svg";
import iconReload_W from "../assets/Reload_W.svg";
import iconDelete_B from "../assets/Delete_B.svg";
import iconDelete_W from "../assets/Delete_W.svg";
import iconView from "../assets/icons8-viewFile.png";
import iconSave_B from "../assets/Save_B.svg";
import iconEdit_B from "../assets/Edit_B.svg";
import iconSave_W from "../assets/Save_W.svg";
import iconEdit_W from "../assets/Edit_W.svg";
import {CreateObj, UpdateObj} from "../http/ObjAPI";
import PopupTooltip from "../components/elementary/PopupTooltip";


const StartPage = () => {

    const navigate = useNavigate()
    const {user} = useContext(Context)

    let ActionList = [
        {id:1, iconB: iconCreate_B, iconW: iconCreate_W, name: "Новый", text: "Позволяет создать новый экземпляр объекта."  },
        {id:2, iconB: iconReload_B, iconW: iconReload_W, name: "Обновить", text: "Перезгрузить данные с сервера." },
        {id:3, iconB: iconDelete_B, iconW: iconDelete_W, name: "Удалить", text: "Перемести к корзину объект." },
        {id:4, iconB: iconSave_B, iconW: iconSave_W, name: "Сохранить",
            style: {borderColor: "var(--c-alter)", borderWidth: "5px"}, text: "Сохранить локальные изменения, отправить их на сервер." },
        {id:5, iconB: iconEdit_B, iconW: iconEdit_W, name: "Изменить",
            style: {}, text: "Начать редактировать объект." },
    ]

    return(
        <div className='MContent d-flex'>
            <div className='Block W-100'>
                <div className='d-flex'>
                    <h1 className='ms-auto me-auto'>База знаний писателя</h1>
                </div>
                <span className='fs-3'>- это возможность записывать информацию о мире книги в структурированном виде.</span>
                <br/>
                <div className='mt-4 fs-4'>
                    <span className=''>Отдельные разделы системы для записи:</span>
                    <div className='ms-4'>
                        <p>- Персонажей</p>
                        <p>- Мест действия</p>
                        <p>- Событий</p>
                        <p>- Предметов</p>
                    </div>

                </div>

                <div className='mt-4 fs-4'>
                    <span>Основные преимущества Базы знаний:</span>
                    <div className='ms-4'>
                        <p>- Возможность создавать ссылки на объекты</p>
                        <p>- Можно добавлять свойства на лету</p>
                        <p>- Система динамических типов объектов</p>
                    </div>
                </div>

                <div className='mt-4 fs-4'>
                    <span>Интуитивный интерфейс с пиктограммами:</span>
                    <div className='d-flex flex-row'>
                    {
                        ActionList.map(e =>
                            <div  key={e.id} className=''>
                                <Button onClick={e.action} className='p-2 ms-1 me-1 MytooltipAncor'
                                        variant={e.variant ? e.variant : 'outline-dark'}>
                                    <PopupTooltip name={e.name} text={e.text} />
                                    <Image className='Black' height='28px' width='28px' src={e.iconB}/>
                                    <Image className='White' height='28px' width='28px' src={e.iconW}/>
                                </Button>
                            </div>
                        )
                    }
                    </div>
                </div>

            </div>
        </div>
    )
}

export default StartPage