import {Button, Image} from "react-bootstrap";
import {useNavigate} from "react-router-dom";
import {LOGIN_ROUTE, MENU_ROUTE} from "../utils/consts";
import logo from '../assets/MaiLogo.svg'
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
                        <div>
                            <Image src={logo}/>
                        </div>
                        <div className='mb-3 fs-3'>Информационная система</div>
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