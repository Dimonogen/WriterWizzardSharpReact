import {useNavigate, useParams} from "react-router-dom";
import {Button, Image} from "react-bootstrap";
import logo from "../assets/MaiLogo.svg";
import {LOGIN_ROUTE} from "../utils/consts";
import MenuComponent from "../components/global/MenuComponent";
import GridObjComponent from "../components/global/GridObjComponent";
import ObjCardComponent from "../components/global/ObjCardComponent";

const MenuPage = () => {

    const navigate = useNavigate()

    const {id, objId} = useParams()

    return(
        <div className='MContent d-flex'>
            <div className='W-100 d-flex'>
                <MenuComponent/>
                {
                    objId == null ?
                        <div className='ms-3 d-flex W-80'>
                            <GridObjComponent/>
                        </div>
                        :
                        <div className='ms-3 d-flex W-80'>
                            <ObjCardComponent/>
                        </div>
                }
            </div>
        </div>
    )
}

export default MenuPage