import {Button, Form, Image} from "react-bootstrap";
import {NavLink, useNavigate} from "react-router-dom";
import {CONFIG_ROUTE, LOGIN_ROUTE, MENU_ROUTE} from "../utils/consts";
import logo from '../assets/DS_logo_light_E.svg'
import React, {useContext, useEffect, useState} from "react";
import {Context} from "../index";
import {getUserSettings} from "../http/UserSettingsAPI";


const SearchPage = () => {

    const navigate = useNavigate()
    const {user} = useContext(Context)

    const [searchText, SetSearchText] = useState("");

    useEffect(() => {
        const params = new URLSearchParams( window.location.search);
        SetSearchText( params.get("searchText") );
    }, [])


    const SearchFun = (text) => {

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
                    <Form className='ms-2 W-100 mt-auto mb-auto' onSubmit={event => {event.preventDefault();
                        SearchFun(searchText);
                    }}>
                        <Form.Control style={{color: 'var(--color-dark)', background: '0', border: '0', borderBottom: '1px solid #ccc', minWidth:'300px'}}
                                      placeholder={"Поиск"}
                                      value={searchText} onChange={(e) => {SetSearchText(e.target.value)} }
                        />
                    </Form>
                    <Button className='ms-2 mt-auto mb-auto h-50' variant='outline-dark' onClick={() => SearchFun(searchText)}>Поиск</Button>
                </div>

            </div>

        </div>
    )
}

export default SearchPage