import {Button, Image} from "react-bootstrap";
import {NavLink, useNavigate} from "react-router-dom";
import {CONFIG_ROUTE, LOGIN_ROUTE, MENU_ROUTE} from "../utils/consts";
import logo from '../assets/DS_logo_light_E.svg'
import {useContext, useEffect, useState} from "react";
import {Context} from "../index";
import {getUserSettings, saveUserSettings} from "../http/UserSettingsAPI";
import FieldTextArea from "../components/elementary/FieldTextArea";


const HomePage = () => {

    const navigate = useNavigate()
    const {user} = useContext(Context)

    const [last, SetLast] = useState({name:"", path:""})

    const [Notes, SetNotes] = useState('')
    const [HaveChanges, SetHaveChanges] = useState(false)
    const [OneSave, SetOneSave] = useState()

    useEffect(() => {
        getUserSettings("LastLocation").then(data => SetLast(data))
        getUserSettings("Notes").then(data => SetNotes(data))
    }, [])

    const arr = [
        {id: 1, name: "Меню объектов", path: MENU_ROUTE, title: "В основном меню находятся все объекты базы знаний. Это могут быть персонажи, локации, событии и всё, что угодно."},
        {id: 2, name: "Настройки", path: CONFIG_ROUTE, title: "В меню настроек можно добавить новые типы или настроить существующие"}
    ]

    const SaveNotes = () => {
        if(HaveChanges) {
            console.log('HC ', HaveChanges)
            console.log('save')
            saveUserSettings('Notes', {settings: Notes}).then(data => {
                SetHaveChanges(false)
            });
        }
    }

    const SaveTimer = () => {

    }

    return(
        <div className='MContent'>
            {last.name == undefined ?
                <div className='Block mb-4'>
                    <div className='d-flex'>
                        <div className='ms-auto me-auto'>
                            <span className='fs-2'>Не найдена история посещения страниц.</span>
                        </div>
                    </div>
                    <div className='d-flex'>
                        <Button className='fs-3 ms-auto me-auto' variant='outline-dark'
                                onClick={() => navigate(MENU_ROUTE + '/1')}>Перейти в меню Персонажей</Button>
                    </div>
                </div>
                :
                <div className='Block mb-4'>
                    <div className='d-flex'>
                        <div className='ms-auto me-auto'>
                            <span className='fs-2'>В прошлый раз вы остановились на <label className='alter'
                                                                                           to={last.path}>{"< " + last.name + " >"}</label></span>
                        </div>
                    </div>
                    <div className='d-flex'>
                        <Button className='fs-3 ms-auto me-auto' variant='outline-dark'
                                onClick={() => navigate(last.path)}>Продолжить c прошлого места</Button>
                    </div>
                </div>
            }
            <div className='Block mb-5'>
                <div className='d-flex'>
                    <h3 className='ms-auto me-auto'>Заметки</h3>
                </div>
                <div>
                    <FieldTextArea useProp={true} value={Notes} setValue={(val) => { SetHaveChanges(true); SaveTimer(); SetNotes(val);console.log('Changed'); } } rows={2} />
                </div>
                <div className='d-flex mt-3'>
                    <Button variant='outline-dark' onClick={() => { SaveNotes();}} >Сохранить заметки</Button>
                    { HaveChanges?<label className='text-danger mt-auto mb-auto ms-3 fw-semibold fs-5'>Есть несохранённые измения</label>:null}
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