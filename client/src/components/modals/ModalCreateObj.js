import React, {useEffect, useState} from 'react';
import {Button, Spinner} from "react-bootstrap";
import {DataGrid} from "@mui/x-data-grid";
import Box from "@mui/material/Box";
import {CreateObj, GetAllOneTypeObjects} from "../../http/ObjAPI";
import {GetOneType} from "../../http/ObjTypeApi";
import FieldText from "../elementary/FieldText";
import Field from "../elementary/Field";

const ModalCreateObj = ({show, onHide, final, title, objType, SetMSelectD}) => {

    let classModal = 'modal-my ';

    useEffect(() => {
        if (objType != null) {
            GetOneType(objType).then(data => {SetType(data);
                //console.log(data)
                SetObjName('Новый объект')
                SetIsEdit(true);
                SetIsLoading(false);
            });
        }
    }, [objType])

    const [IsLoading, SetIsLoading] = useState(true);

    const [type, SetType] = useState({attributes:[]});
    const [isEdit, SetIsEdit] = useState(false);
    const [objName, SetObjName] = useState("");

    const attributeEdits = {};

    const attrTrans = (attributes) => {
        let list = []
        for (const [key, value] of Object.entries(attributes)) {
            list.push({number: key, value: value});
        }
        return list;
    }

    const setValue = (value, id) => {
        attributeEdits[id] = value.toString();
        //console.log("value = " + value + ' number = '+ id)
    }

    return (
        <div onClick={(e)=>{if(e.target.id == 'myModalCreateObj'){onHide()}}}
             id="myModalCreateObj" className={classModal + (show ? 'd-block' : 'd-none')}>
            <div className="modal-content-center-my W-90">
                <div className="modal-header-my ">
                    <h4 className='text-dark'>{title}</h4>
                </div>
                <div className="modal-body-my">
                    {IsLoading ?
                        <div className='Block W-100 d-flex'>
                            <div className='ms-auto me-auto d-flex flex-column'>
                                <div className='ms-auto me-auto'>Загрузка...</div>
                                <Spinner className='ms-auto me-auto'/>
                            </div>
                        </div>
                        :
                        <div>
                            {
                                type.id ?
                                    <FieldText setValue={SetObjName} value={objName} name="Название" minlen={1}
                                               maxlen={255} id={0}
                                               placeholder="Название объекта" nullable={false} key={-1}
                                               disabled={!isEdit}/>
                                    : null
                            }
                            {
                                type.attributes.map(e =>
                                    <Field isComplex={e.isComplex} SetModalD={(data) => SetMSelectD(data)} type={e.type}
                                           setValue={setValue}
                                           value={e.value} name={e.name} minlen={1} maxlen={255} id={e.number}
                                           regex={e.regEx} objId={0}
                                           placeholder="Не заполнено" nullable={false} key={e.number}
                                           disabled={!isEdit}/>
                                )
                            }
                        </div>
                    }
                </div>
                <div className="modal-footer-my">

                    <Button className='me-2' variant='outline-dark' onClick={() => onHide()}>Отмена</Button>
                    <Button variant='dark' onClick={() => {CreateObj({typeId: objType, name: objName,
                        attributes: attrTrans(attributeEdits)}).then(data => {final(data.id); onHide()}); }}>Создать</Button>
                </div>
            </div>
        </div>

    );
};

export default ModalCreateObj;