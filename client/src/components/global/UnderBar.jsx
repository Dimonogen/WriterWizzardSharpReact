import React, { useContext } from 'react'
import { Context } from '../../ContextApp'
import { Form, Navbar, Nav, Container, Button, Col, Row, Image} from 'react-bootstrap'
import { NavLink, useNavigate } from 'react-router-dom'
import { LOGIN_ROUTE} from '../../utils/consts'
import {observer} from 'mobx-react-lite'

const UnderBar = observer( () => {
    return (
        <div className="bg-dark mt-3 p-2 pt-3 pb-4">
            <Container>
                <div className="text-center text-white">Систему разработал <a href="https://t.me/Dimonogen">Дмитрий Толстиков</a> 2024</div>
            </Container>
        </div>
    )
})

export default UnderBar