import {Button, Image} from "react-bootstrap";
import React from "react";
import {NavLink, useNavigate} from "react-router-dom";
import {BASE_ROUTE} from "../../utils/consts";
import logo from '../../assets/DS_logo_light_E.svg'


const ModalLeftMenuMobile = ({show, onHide}) => {
    let classModal = 'modal-my  ';

    const navigate = useNavigate()

    const links = [
        /*{text:'Книги', link:SHOP_ROUTE},
        {text:'Поиск', link: SEARCH_ROUTE},
        {text:'Writer Wizzard', link: WW_PAGE}*/
    ]

    const idMenu = 'leftmenulink';

  return (
      <div onClick={(e)=>{if(e.target.id === 'ModalLeftMenu'){onHide()}}}
           id="ModalLeftMenu" className={classModal + (show ? 'd-block' : 'd-none')} style={{}}>
        <div className='modal-content-my-left W-70'
        onClick={(e)=>{if(e.target.id === idMenu){onHide()}}}>
            <div id={idMenu} className='m-3 flex-row d-flex' style={{cursor:'pointer'}}
                 onClick={() => navigate(BASE_ROUTE)}
            >
                <Image id={idMenu} width={64} height={64} src={logo}/>
                <div  className='ms-2 mt-3'>
                    <h2 id={idMenu}>Dimonogen.ru</h2>
                </div>
            </div>
            <div className='flex-column ms-2'>
            {
                links.map(e =>
                    <div className='mt-4 mb-4' key={e.link}>
                        <NavLink id={idMenu} className='ms-2 mt-1 fs-3 alter' to={e.link}>{e.text}</NavLink>
                    </div>
                )
            }
            </div>
        </div>
      </div>
  )
}

export default ModalLeftMenuMobile