import {Button, Image} from "react-bootstrap";
import {useNavigate} from "react-router-dom";
import {LOGIN_ROUTE, MENU_ROUTE} from "../utils/consts";
import logo from '../assets/DS_logo_light_E.svg'
import {useContext} from "react";
import {Context} from "../index";


const StartPage = () => {

    const navigate = useNavigate()
    const {user} = useContext(Context)

    return(
        <div className='MContent d-flex'>
            <div className='W-100 d-flex justify-content-center align-content-center'>
                <div className='d-inline-block mt-auto mb-auto Block'>
                    <div className='d-flex flex-column p-3'>
                        <div className='d-flex justify-content-center'>
                            <Image src={logo} width='256px'/>
                        </div>
                        <div className='mb-3 fs-3'>База знаний писателя</div>
                        <Button className='fs-4' onClick={() => {
                            if(user.isAuth)
                                navigate(MENU_ROUTE)
                            else
                                navigate(LOGIN_ROUTE)
                        }}>Вход</Button>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default StartPage