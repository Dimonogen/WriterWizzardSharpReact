import {GetAllObjTypes} from "../../http/ObjTypeApi";
import React, {useContext, useEffect, useState} from "react";
import {Button, Image, Form} from "react-bootstrap";
import {useNavigate, useParams} from "react-router-dom";
import {MENU_ROUTE, TRESHCAN_ROUTE} from "../../utils/consts";
import {GetMenuForUser} from "../../http/MenuApi";
import {CreateType} from "../../http/ObjTypeApi";
import {Context} from "../../index";
import iconDelete from "../../assets/icons8-delete.svg";

const MenuComponent = ({onHide}) => {

    const navigate = useNavigate();
    const {id, objId} = useParams()

    const TreshCan = window.location.pathname.indexOf(TRESHCAN_ROUTE) != -1;

    const {user} = useContext(Context)

    const [menuElements, SetMenuElements] = useState([]);

    const [IsAddNew, SetIsAddNew] = useState(false);
    const [NewTypeName, SetNewTypeName] = useState("");

    useEffect(() => {
        ReloadMenu();
        //GetAllObjTypes().then(data => {SetTypes(data);//console.log(data)
        //     });
    }, [])

    const ReloadMenu = () => {
        GetMenuForUser().then(data => {
            SetMenuElements(data);
            //console.log(data)
            if(id != undefined && objId == undefined)
                user.setPath( [data.filter(obj => {return obj.objTypeId.toString() == id})[0].name] )
        });
    };

    const CreateTypeLocal = (typeName) => {
        if(typeName == "")
        {

        }
        CreateType({name: typeName, description: typeName, code: typeName, attributes: [], createMenu: true})
            .then(data => { SetNewTypeName(""); ReloadMenu(); navigate(MENU_ROUTE + '/' + data.id) });
        SetIsAddNew(false);
    };


    return (
        <div className=''>
            <div className='d-flex justify-content-center fs-4'>
                <span>Меню</span>
            </div>

            {
                menuElements.map(e =>
                    <div className='mt-2' key={e.id} >
                        <Button className='W-100 text-start' variant={id == e.objTypeId?'dark':'outline-dark'}
                                onClick={() => {
                                    user.setPath(e.name, 0);
                                    navigate(MENU_ROUTE+'/'+e.objTypeId)
                                    if(onHide != undefined)
                                        onHide();
                                }} >{e.name}
                        </Button>
                    </div>
                )
            }
            <div className='mt-4 d-flex' key={1000} >
                {
                    !IsAddNew ?
                    <Button className='W-100 text-start' variant={'outline-secondary'}
                            onClick={() => {
                                SetIsAddNew(true)
                            }}>
                        + Добавить пункт меню
                    </Button>
                        :
                    <div className="W-100">
                        <Form className='ms-auto me-auto ' onSubmit={event => {event.preventDefault();
                            CreateTypeLocal(NewTypeName);
                        }}>
                        <Form.Control className="" style={{background: '0', border: '0', borderBottom: '1px solid #ccc'}}
                                      placeholder={"Введите название"} onChange={
                                            event => SetNewTypeName(event.target.value)}
                                      value={NewTypeName}
                        />
                        </Form>
                        <div className="d-flex">
                            <Button className="mt-2 me-auto" variant="outline-secondary" onClick={
                                () => {SetIsAddNew(false)} }
                                >Отмена</Button>
                            <Button className="mt-2 ms-auto" variant="dark" onClick={
                                () => {CreateTypeLocal(NewTypeName)} }
                                >Добавить</Button>
                        </div>

                        {//<Button className="W-100 text-start mt-2" variant="outline-dark">Добавить</Button>
                            }
                    </div>
                }
            </div>
            <div className='mt-5 d-flex' key={1001} >
                <Button className='W-100 text-start' variant={TreshCan?'dark':'outline-dark'}
                        onClick={() => {
                            user.setPath(["Корзина"])
                            navigate(TRESHCAN_ROUTE)}} >
                    Корзина
                </Button>
                <Image className='m-1 ms-2 me-1' height='32px' width='32px' src={iconDelete}/>
            </div>
        </div>
    )
}

export default MenuComponent