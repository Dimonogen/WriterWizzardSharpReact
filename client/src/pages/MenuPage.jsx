import {useNavigate, useParams} from "react-router-dom";
import {Button, Image} from "react-bootstrap";
import logo from "../assets/MaiLogo.svg";
import {LOGIN_ROUTE, MENU_ROUTE, TRESHCAN_ROUTE} from "../utils/consts";
import MenuComponent from "../components/menu/MenuComponent.jsx";
import GridObjComponent from "../components/global/GridObjComponent";
import ObjCardComponent from "../components/global/ObjCardComponent";
import GridObjTreshCanComponent from "../components/global/GridObjTreshCanComponent";
import ModalOkMy from "../components/modals/ModalOkMy";
import {DeleteObjList} from "../http/ObjAPI.jsx";
import ModalYesNoMy from "../components/modals/ModalYesNoMy.jsx";
import React, {useState} from "react";
import {DeleteMenuElement} from "../http/MenuApi.jsx";

const MenuPage = () => {

    const navigate = useNavigate()

    const {id, objId} = useParams()

    const [showDelete, setShowDelete] = useState(false)
    const [deleteText, setDeleteText] = useState("")
    const [deleteId, setDeleteId] = useState(null)
    const [reloadHandle, setReloadHandle] = useState(Date.now())

    const ReloadGrid = () => {

    }

    return(
        <div className='MContent d-flex'>
            <div className='W-100 d-flex'>
                <div className={"Block LeftMenuSize " + (id != null  ? " MobileHide" : " ")}>
                    <MenuComponent onDelete={(id, name) => {
                        setDeleteText(name);
                        setDeleteId(id);
                        setShowDelete(true)
                    }} reloadHandle={reloadHandle} />
                </div>

                {
                    window.location.pathname.indexOf(TRESHCAN_ROUTE) === -1 ?
                        id !== undefined ?
                        <div className={"d-flex " + (window.innerWidth > 800? "W-80": "W-100")}> {/** TODO: тут можно переписать на media запросы*/}
                            <div className={ (window.innerWidth > 800? "ms-3": "") + ' overflow-auto ' + (objId != null ? "GridObj" : "W-100")}>
                                <GridObjComponent />
                            </div>
                            {objId != null ?
                                <div className="ObjCard d-flex">
                                        <ObjCardComponent/>
                                </div>
                             : null
                            }
                        </div> : null
                        :
                        <div className={"d-flex " + (window.innerWidth > 800? "W-80": "W-100")}>
                            <div className={'d-flex ms-3 ' + (objId != null ? "W-50" : "W-100")}>
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
            {
                showDelete &&
                <ModalYesNoMy title={"Вы действительно хотите удалить пункт меню?"} notitle="Отмена" yestitle="Удалить"
                              show={true} onHide={() => setShowDelete(false)} final={() => {
                    DeleteMenuElement(deleteId).then((data) => {setReloadHandle(Date.now())})
                }}/>
            }
        </div>
    )
}

export default MenuPage