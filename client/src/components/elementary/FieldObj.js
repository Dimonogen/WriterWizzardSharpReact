import {Button, Form} from "react-bootstrap";
import React, {useEffect, useState} from "react";
import {GetOneObj} from "../../http/ObjAPI";
import {MENU_ROUTE} from "../../utils/consts";
import {NavLink} from "react-router-dom";
import ModalSelectObj from "../modals/ModalSelectObj";

const FieldObj = ({type, setValue, value, name, disable, number, SetModalD}) => {


    useEffect(() => {
        if(value != null && value != "")
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
            <Form.Group className="mb-1 mt-1 w-100 d-flex">
                <div className={"FieldLeft d-flex text-end"}>
                    <Form.Label className="ms-auto me-3 mt-2" style={{textTransform: "capitalize"}}>{name}</Form.Label>
                </div>
                <div className='border rounded d-flex p-1 W-100'>
                    <NavLink className='ms-2 mt-auto mb-auto alter' to={MENU_ROUTE+'/'+obj.typeId+'/'+obj.id}>{obj.name}</NavLink>
                    <Button className='ms-auto p-1' variant='outline-dark' disabled={disable}
                            onClick={() => SetModalD({show:true, objType:type, title:'Выберите ' + name, final: (id) => LoadData(id)})}
                        >Выбрать</Button>
                </div>
            </Form.Group>
        </div>
    )
}

export default FieldObj