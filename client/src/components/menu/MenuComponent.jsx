import {GetAllObjTypes} from "../../http/ObjTypeApi.jsx";
import React, {useContext, useEffect, useState} from "react";
import {Button, Image, Form} from "react-bootstrap";
import {useNavigate, useParams} from "react-router-dom";
import {MENU_ROUTE, SEARCH_ROUTE, TRESHCAN_ROUTE} from "../../utils/consts.jsx";
import {DeleteMenuElement, GetMenuForUser} from "../../http/MenuApi.jsx";
import {CreateType} from "../../http/ObjTypeApi.jsx";
import {Context} from "../../ContextApp.jsx";
import iconDelete from "../../assets/icons8-delete.svg";
import eyeClose from "../../assets/EyeClose.svg";
import eyeOpen from "../../assets/EyeOpen.svg";
import iconEdit from "../../assets/Edit_B.svg"
import MenuItemComponent from "./MenuItemComponent.jsx";
import ModalYesNoMy from "../modals/ModalYesNoMy.jsx";
import iconSearchW from "../../assets/Search_W.svg";
import iconSearchB from "../../assets/Search_B.svg";

const MenuComponent = ({onHide, onDelete, reloadHandle}) => {

    const navigate = useNavigate();
    const {id, objId} = useParams()

    const TrashCan = window.location.pathname.indexOf(TRESHCAN_ROUTE) !== -1;

    const {user} = useContext(Context)

    const [menuElements, SetMenuElements] = useState([]);

    const [IsAddNew, SetIsAddNew] = useState(false);
    const [NewTypeName, SetNewTypeName] = useState("");

    const [showConfig, setShowConfig] = useState(false);

    const [searchText, SetSearchText] = useState("");

    const SearchFun = (text) => {
        navigate(SEARCH_ROUTE + '?searchText='+encodeURIComponent(searchText));
        onHide();
        SetSearchText("");
    }

    useEffect(() => {
        ReloadMenu();
        //GetAllObjTypes().then(data => {SetTypes(data);//console.log(data)
        //     });
    }, [user.isAuth, reloadHandle])

    const ReloadMenu = () => {
        if (user.isAuth) {
            GetMenuForUser().then(data => {
                SetMenuElements(data);
                //console.log(data)
                if (id !== undefined && objId === undefined)
                {
                    let path = data.filter(obj => obj.objTypeId.toString() === id );
                    if(path.length > 0 && path[0].name !== undefined)
                        user.setPath([path[0].name], 0)
                }

            });
        } else {
            SetMenuElements([]);
        }
    };

    const CreateTypeLocal = (typeName) => {
        if (typeName === "") {

        }
        CreateType({name: typeName, description: typeName, code: typeName, attributes: [], createMenu: true})
            .then(data => {
                SetNewTypeName("");
                ReloadMenu();
                navigate(MENU_ROUTE + '/' + data.id)
            });
        SetIsAddNew(false);
    };


    return (
        <div className=''>
            <div className='d-flex fs-4'>
                <div className='ms-auto me-auto'>
                    <span>Меню</span>
                </div>
                <div className="">
                    <Button className="p-0 ms-2 mt-auto mb-auto me-0" variant="outline-secondary"
                            onClick={() => setShowConfig(!showConfig)}>
                        <Image className='m-0' height='32px' width='32px' src={showConfig ? eyeClose : iconEdit}/>
                    </Button>
                </div>
            </div>

            {
                user.isAuth ?
                    <>
                        {
                            menuElements.map(e =>
                                <div className='mt-2' key={e.id}>
                                    <MenuItemComponent name={e.name} onHide={onHide} path={MENU_ROUTE + '/' + e.objTypeId}
                                                       isShowConfig={showConfig} isSelected={id === e.objTypeId.toString()}
                                                       onChange={(name) => SetMenuElements((prev) => {
                                                           let index = prev.findIndex(x => x.id === e.id)
                                                           prev[index].name = name;
                                                           //console.log(prev, index)
                                                           return prev;
                                                       })} id={e.id} onDelete={onDelete}
                                    />
                                </div>
                            )
                        }
                        <div className='mt-4 d-flex' key={1000}>
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
                                        <Form className='ms-auto me-auto ' onSubmit={event => {
                                            event.preventDefault();
                                            CreateTypeLocal(NewTypeName);
                                        }}>
                                            <Form.Control className="" style={{
                                                background: '0',
                                                border: '0',
                                                borderBottom: '1px solid #ccc'
                                            }}
                                                          placeholder={"Введите название"} onChange={
                                                event => SetNewTypeName(event.target.value)}
                                                          value={NewTypeName}
                                            />
                                        </Form>
                                        <div className="d-flex">
                                            <Button className="mt-2 me-auto" variant="outline-secondary" onClick={
                                                () => {
                                                    SetIsAddNew(false)
                                                }}
                                            >Отмена</Button>
                                            <Button className="mt-2 ms-auto" variant="dark" onClick={
                                                () => {
                                                    CreateTypeLocal(NewTypeName)
                                                }}
                                            >Добавить</Button>
                                        </div>

                                        {//<Button className="W-100 text-start mt-2" variant="outline-dark">Добавить</Button>
                                        }
                                    </div>
                            }
                        </div>
                        {
                            user.isAuth  ?
                                <div className='MobileShow mt-4'>
                                    <div className='mt-auto mb-auto d-flex h-100 w-100'>
                                        <Form className='w-100' onSubmit={event => {
                                            event.preventDefault();
                                            SearchFun(searchText);
                                        }}>
                                            <Form.Control style={{
                                                background: '0',
                                                border: '0',
                                                borderBottom: '1px solid #ccc',
                                            }}
                                                          placeholder={"Поиск"}
                                                          value={searchText} onChange={(e) => {
                                                SetSearchText(e.target.value)
                                            }}
                                            />
                                        </Form>
                                        <Button className='ms-2 mt-auto mb-auto p-1' variant='outline-dark'
                                                onClick={() => SearchFun(searchText)}>
                                            <Image className='m-0 p-0 Black' src={iconSearchB}/>
                                            <Image className='m-0 p-0 White' src={iconSearchW}/>
                                        </Button>
                                    </div>
                                </div>: null
                        }
                        <div className='mt-5 d-flex' key={1001}>
                            <Button className='W-100 text-start' variant={TrashCan ? 'dark' : 'outline-dark'}
                                    onClick={() => {
                                        user.setPath(["Корзина"])
                                        navigate(TRESHCAN_ROUTE)
                                    }}>
                                Корзина
                            </Button>
                            <Image className='m-1 ms-2 me-1' height='32px' width='32px' src={iconDelete}/>
                        </div>
                    </>
                    :
                    <div className='fs-5'>
                        <br/>
                        Меню доступно только авторизованным пользователям
                    </div>
            }
        </div>
    )
}

export default MenuComponent