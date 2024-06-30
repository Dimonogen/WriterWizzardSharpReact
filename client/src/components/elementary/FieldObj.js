import {Button, Form} from "react-bootstrap";
import React, {useEffect, useState} from "react";
import {GetOneObj} from "../../http/ObjAPI";
import {MENU_ROUTE} from "../../utils/consts";
import {NavLink} from "react-router-dom";
import ModalSelectObj from "../modals/ModalSelectObj";

const FieldObj = ({type, setValue, value, name, disable, number, SetModalD}) => {


    useEffect(() => {
        if(value != null)
            GetOneObj(value).then(data => {SetObj(data); //console.log(data)
                 })
    }, [value, name])

    const [obj, SetObj] = useState({name:""})
    const [objId, SetObjId] = useState(value)
    const [showModal, SetShowModal] = useState(false)

    const LoadData = (newId) => {
        GetOneObj(newId).then(data => {SetObj(data); SetObjId(newId); setValue(newId, number)})
    }

    return (
        <div>
            <Form.Group className="mb-3 w-100">
                <Form.Label style={{textTransform: "capitalize"}}>{name}</Form.Label>
                <div className='border rounded d-flex p-1'>
                    <NavLink className='ms-2 mt-1 alter' to={MENU_ROUTE+'/'+obj.typeId+'/'+obj.id}>{obj.name}</NavLink>
                    <Button className='ms-auto p-1' variant='outline-dark' disabled={disable}
                            onClick={() => SetModalD({show:true, objType:type, title:'Выберите ' + name, final: (id) => LoadData(id)})}
                        >Выбрать</Button>
                </div>
            </Form.Group>
        </div>
    )
}

export default FieldObj