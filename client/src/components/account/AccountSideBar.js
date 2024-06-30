import { observer } from 'mobx-react-lite'
import React, {useContext, useState} from 'react'
import {Card, Col, Image, ListGroup} from 'react-bootstrap'
import { useNavigate } from "react-router-dom"
import {ACCOUNT_ROUTE} from '../../utils/consts'
import {Context} from "../../index";


const BookItem = () => {
    const {user} = useContext(Context)
    const navigate = useNavigate()

    //console.log(window.location)

    const selected = window.location.pathname;
    //console.log(book)

    return (
        <div>
            <div className='d-flex'>
                <Image
                    className=""
                    style={{ border:'5px white solid', borderRadius: '10px', zIndex:'0'}}
                    width={100} height={100} src={process.env.REACT_APP_STATIC_URL+user.info.icon}
                />
                <div className='ms-3 mt-auto mb-auto'>
                    <h3>{user.info.name}</h3>
                </div>
            </div>
            <hr/>
            <ListGroup>
                {
                    [
                        {id:1, name:"Мой профиль", href:ACCOUNT_ROUTE},
                        /*{id:2, name:"Произведения", href: ACCOUNT_WORKS_ROUTE}*/
                    ].map(elem =>
                        <ListGroup.Item
                            style={{cursor: 'pointer'}}
                            active={elem.href === selected}
                            onClick={() => navigate(elem.href)}
                            key={elem.id}
                        >
                            {elem.name}
                        </ListGroup.Item>
                    )}
            </ListGroup>
        </div>
    )
}

export default BookItem