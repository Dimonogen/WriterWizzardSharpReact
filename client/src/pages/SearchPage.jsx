import {Button, Form, Image} from "react-bootstrap";
import {NavLink, useNavigate} from "react-router-dom";
import {CONFIG_ROUTE, LOGIN_ROUTE, MENU_ROUTE} from "../utils/consts";
import logo from '../assets/DS_logo_light_E.svg'
import React, {useContext, useEffect, useState} from "react";
import {Context} from "../ContextApp";
import {getUserSettings} from "../http/UserSettingsAPI";
import {getSearchObjects} from "../http/SearchAPI";
import ModalOkMy from "../components/modals/ModalOkMy";
import SearchLabel from "../components/elementary/SearchLabel";


const SearchPage = () => {

    const navigate = useNavigate()
    const {user} = useContext(Context)

    const [searchText, SetSearchText] = useState("");
    const [Results, SetResults] = useState([]);
    const [Stats, SetStats] = useState({all:-1, find: -1})

    const [ShowMessage, SetShowMessage] = useState(false);
    const [Message, SetMessage] = useState('');

    useEffect(() => {
        const params = new URLSearchParams( window.location.search);
        const text = params.get("searchText");
        SetSearchText( text );
        SearchFun(text);
    }, [])


    const SearchFun = (text) => {
        if(text == '')
        {
            SetMessage("Поиск не возможен. Задайте текст поиска.");
            SetShowMessage(true);
            return;
        }
        getSearchObjects(text).then(data => {SetResults(data.results);SetStats({all: data.all, find: data.find});console.log(data)});
    }

    return(
        <div className='MContent'>
            <div className='Block'>
                <div className='d-flex'>
                    <h3 className='ms-auto me-auto'>
                        Страница поиска
                    </h3>
                </div>

                <div className='d-flex'>
                    <Form className='ms-2 ms-auto mt-auto mb-auto ' onSubmit={event => {event.preventDefault();
                        SearchFun(searchText);
                    }}>
                        <Form.Control style={{color: 'var(--color-dark)', background: '0', border: '0', backgroundColor:'#eee', borderBottom: '2px solid #ccc', minWidth:'300px'}}
                                      placeholder={"Поиск"} className='fs-5'
                                      value={searchText} onChange={(e) => {SetSearchText(e.target.value)} }
                        />
                    </Form>
                    <Button className='ms-2 mt-auto mb-auto me-auto h-50' variant='outline-dark' onClick={() => SearchFun(searchText)}>Поиск</Button>
                </div>



            </div>

            <div className='mt-4 ms-3 fs-5 mb-3'>
                {
                    Stats.all == -1 ?
                    <label>Поиск не производился</label>
                        :
                    <label>{'Найдено в результате поиска: ' + Stats.find + ' Всего в системе: ' + Stats.all}</label>
                }
            </div>

            <div className=''>
                {
                    Results.map((e) =>
                        <div className='Block mt-3 d-flex' onDoubleClick={() =>{
                            navigate(MENU_ROUTE+'/'+e.type.id+'/'+e.object.id);
                            if (window.getSelection) {
                                window.getSelection().removeAllRanges();
                            } else if (document.selection) {
                                document.selection.empty();
                            }
                        }}>
                            <div className='d-flex W-20'>
                                <div className='d-flex flex-column'>
                                    <label className='fw-semibold me-2'>Тип:</label>
                                    <label className='fw-semibold me-2'>Название:</label>
                                    <label className='fw-semibold me-2'>Статус:</label>
                                </div>
                                <div className='d-flex flex-column'>
                                    <label>{e.type.name}</label>
                                    <SearchLabel text={e.object.name} searchText={searchText}/>
                                    <label>{e.object.state}</label>
                                </div>
                            </div>
                            <div className='ms-5 W-80'>
                            {
                                e.inAttributes.map(a =>
                                <div className='d-flex'>
                                    <label className='fw-semibold me-2'>{a.name+':'}</label>
                                    <SearchLabel text={a.value} searchText={searchText} />
                                </div>
                                )
                            }
                            </div>
                            <div className='ms-auto mt-auto mb-auto'>
                                <Button variant='outline-dark' onClick={() => navigate(MENU_ROUTE+'/'+e.type.id+'/'+e.object.id)} >Открыть</Button>
                            </div>
                        </div>
                    )
                }
            </div>
            <ModalOkMy show={ShowMessage} onHide={() => SetShowMessage(false)} okTitle={'Ок'} title={Message} />
        </div>
    )
}

export default SearchPage