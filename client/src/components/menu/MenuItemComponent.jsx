import {Button, Form, Image} from "react-bootstrap";
import {MENU_ROUTE} from "../../utils/consts.jsx";
import iconEdit from "../../assets/Edit_B.svg";
import iconSave from "../../assets/SaveIcon.svg"
import React, {useContext, useState} from "react";
import {Context} from "../../ContextApp.jsx";
import {useNavigate} from "react-router-dom";
import {SaveEditMenuElement} from "../../http/MenuApi.jsx";


const MenuItemComponent = ({isSelected, id, path, name, isShowConfig, onHide, onChange, onDelete}) => {

    const {user} = useContext(Context)
    const navigate = useNavigate();

    const [isEdit, setIsEdit] = useState(false);
    const [MenuName, SetMenuName] = useState(name)

    const EditMenuLocal = () => {
        SaveEditMenuElement(id, MenuName).then((data) => {onChange(MenuName)});
        setIsEdit(false);
    }

    return (
        <div className='mt-2 d-flex'>
            {
                isEdit && isShowConfig ?
                    <div className="W-100">
                        <Form className='ms-auto me-auto ' onSubmit={event => {
                            event.preventDefault();
                            EditMenuLocal(MenuName);
                        }}>
                            <Form.Control className="" style={{
                                background: '0',
                                border: '0',
                                borderBottom: '1px solid #ccc'
                            }}
                                          placeholder={"Введите название"} onChange={
                                event => SetMenuName(event.target.value)}
                                          value={MenuName}
                            />
                        </Form>
                        <div className="d-flex">
                            <Button className="mt-2 me-auto" variant="outline-danger" onClick={
                                () => {
                                    onDelete(id, MenuName)
                                }}
                            >Удалить</Button>
                        </div>
                    </div>
                    :
                <Button className={'W-100 text-start '}
                        variant={isSelected ? 'dark' : 'outline-dark'}
                        onClick={() => {
                            user.setPath(MenuName, 0);
                            navigate(path)
                            if (onHide !== undefined)
                                onHide();
                        }}>{MenuName}
                </Button>
            }
            { isShowConfig && <Button variant='outline-secondary' className='m-0 p-0 ps-1 pe-1 ms-1'
                                      onClick={() => {
                                          if(isEdit)
                                          {
                                              EditMenuLocal(MenuName)
                                          }
                                          setIsEdit(!isEdit )
                                      } }>
                <Image className='m-0' height='32px' width='32px' src={ isEdit ? iconSave : iconEdit}/>
            </Button> }
        </div>
    )
}


export default MenuItemComponent