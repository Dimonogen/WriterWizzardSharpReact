import {Button, Image} from "react-bootstrap";
import React, {useContext} from "react";
import {NavLink, useNavigate} from "react-router-dom";
import {BASE_ROUTE, HOME_ROUTE, MENU_ROUTE} from "../../utils/consts.jsx";
import logo from '../../assets/DS_logo_light_E.svg'
import MenuComponent from "./MenuComponent.jsx";
import {Context} from "../../ContextApp.jsx";


const ModalLeftMenuMobile = ({show, onHide}) => {
    let classModal = 'modal-my  ';

    const navigate = useNavigate()

    const {user} = useContext(Context)

    const idMenu = 'leftmenulink';

  return (
      <div onClick={(e)=>{if(e.target.id === 'ModalLeftMenu'){onHide()}}}
           id="ModalLeftMenu" className={classModal + (show ? 'd-block' : 'd-none')} style={{}}>
        <div className='modal-content-my-left W-70'
        onClick={(e)=>{if(e.target.id === idMenu){onHide()}}}>
            <div id={idMenu} className='m-3 flex-row d-flex' style={{cursor:'pointer'}}
                 onClick={() => { user.isAuth ? navigate(HOME_ROUTE) : navigate(BASE_ROUTE) }}
            >
                <Image id={idMenu} width={64} height={64} src={logo}/>
                <div  className='ms-2 mt-3'>
                    <h2 id={idMenu}>Litbase.ru</h2>
                </div>
            </div>
            <div className='flex-column m-3 '>
                <MenuComponent onHide={() => onHide()}/>
            </div>
        </div>
      </div>
  )
}

export default ModalLeftMenuMobile