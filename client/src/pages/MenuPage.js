import {useNavigate, useParams} from "react-router-dom";
import {Button, Image} from "react-bootstrap";
import logo from "../assets/MaiLogo.svg";
import {LOGIN_ROUTE, MENU_ROUTE, TRESHCAN_ROUTE} from "../utils/consts";
import MenuComponent from "../components/global/MenuComponent";
import GridObjComponent from "../components/global/GridObjComponent";
import ObjCardComponent from "../components/global/ObjCardComponent";
import GridObjTreshCanComponent from "../components/global/GridObjTreshCanComponent";

const MenuPage = () => {

    const navigate = useNavigate()

    const {id, objId} = useParams()


    const ReloadGrid = () => {

    }

    return(
        <div className='MContent d-flex'>
            <div className='W-100 d-flex'>
                <MenuComponent/>
                {
                    window.location.pathname.indexOf(TRESHCAN_ROUTE) == -1 ?
                        <div className="ms-3 d-flex W-80">
                            <div className={'d-flex ' + (objId != null ? "W-50" : "W-100")}>
                                <GridObjComponent />
                            </div>
                            {objId != null ?
                            <div className='ms-3 d-flex W-50'>
                                <ObjCardComponent/>
                            </div> : null
                            }
                        </div>
                        :
                        <div className="ms-3 d-flex W-80">
                            <div className={'d-flex ' + (objId != null ? "W-50" : "W-100")}>
                                <GridObjTreshCanComponent/>
                            </div>
                            {objId != null ?
                                <div className='ms-3 d-flex W-50'>
                                    <ObjCardComponent/>
                                </div> : null
                            }
                        </div>
                }
            </div>
        </div>
    )
}

export default MenuPage