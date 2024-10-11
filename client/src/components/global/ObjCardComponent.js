import {Button, Image, Spinner} from "react-bootstrap";
import {useNavigate, useParams} from "react-router-dom";
import {MENU_ROUTE} from "../../utils/consts";
import React, {useContext, useEffect, useState} from "react";
import {CreateObj, GetOneObj, UpdateObj, DeleteObj} from "../../http/ObjAPI";
import {GetOneType} from "../../http/ObjTypeApi"
import iconEdit from '../../assets/icons8-edit.svg'
import iconState from '../../assets/icons8-network.png'
import iconDelete from '../../assets/icons8-delete.svg'
import iconUpdate from '../../assets/icons8-restart.svg'
import FieldText from "../elementary/FieldText";
import ModalYesNoMy from "../modals/ModalYesNoMy";
import Field from "../elementary/Field";
import ModalSelectObj from "../modals/ModalSelectObj";
import {Context} from "../../index";

const ObjCardComponent = () => {

    const {id, objId} = useParams()
    const isNewObj = objId == 0;

    const navigate = useNavigate()
    const {user} = useContext(Context)

    const [IsLoading, SetIsLoading] = useState(true);

    const [obj, SetObj] = useState({attributes:[]});
    const [type, SetType] = useState({attributes:[]});
    const [isEdit, SetIsEdit] = useState(false);
    const [objName, SetObjName] = useState("");

    const [show, SetShow] = useState(false);
    const [MSelectD, SetMSelectD] = useState({show:false})

    const attributeEdits = {};

    const setHistory = (name) => {
        let arr = user.path;
        if(arr.length == 1)
            arr.push(name);
        else
            arr[1] = name;
        user.setPath(arr);
    }

    useEffect(() => {
        if(!isNewObj) {
            GetOneObj(objId).then(data => {
                console.log(data)
                SetObj(data);
                SetObjName(data.name);
                setHistory(data.name);
                GetOneType(data.typeId).then(data => {SetType(data); SetIsLoading(false) });
            });
        }
        else
        {
            GetOneType(id).then(data => {SetType(data);
                //console.log(data)
                SetObjName('Новый объект')
                setHistory('Создание нового объекта');
                SetIsEdit(true);
                SetIsLoading(false);
            });

        }
    }, [id, objId])

    const attrTrans = (attributes) => {
        let list = []
        for (const [key, value] of Object.entries(attributes)) {
            list.push({number: key, value: value});
        }
        return list;
    }

    let ActionList = [
        {id:1, icon: iconEdit, name: isEdit?"Сохранить":"Изменить", action: () => {
            if(isEdit)
            {//сохранить
                if(objId != 0)
                UpdateObj({id: objId, typeId: id, name: objName,
                    attributes: attrTrans(attributeEdits)})
                    .then(data => SetObj(data)).catch(e => console.log(e));
                else
                {//create obj
                    //console.log('create obj attributes', attributeEdits);
                CreateObj({typeId: id, name: objName,
                    attributes: attrTrans(attributeEdits)}).then(data => SetObj(data));
                }
            }
                SetIsEdit(!isEdit);
            }},
        {id:2, icon: iconState, name: "Состояние", action: () => {}},
        {id:3, icon: iconDelete, name: "Удалить", action: () => {SetShow(true)}},
        {id:4, icon: iconUpdate, name: "Обновить", action: () => {}},
    ]

    const setValue = (value, id) => {
        attributeEdits[id] = value.toString();
        //console.log("value = " + value + ' number = '+ id)
    }

    return (
        IsLoading ?
            <div className='Block W-100 d-flex'>
                <div className='ms-auto me-auto d-flex flex-column'>
                    <div className='ms-auto me-auto'>Загрузка...</div>
                    <Spinner className='ms-auto me-auto'/>
                </div>
            </div>
            :
        <div className='Block W-100'>
            <ModalSelectObj show={MSelectD.show} onHide={() => SetMSelectD({show:false})} title={MSelectD.title}
                            objType={MSelectD.objType} final={(data) => MSelectD.final(data)} />
            <div className='W-100 d-flex'>
                <span>{objName + ' - ' + type.name}</span>
                <Button className='ms-auto p-0 ps-2 pe-2' variant='outline-dark'
                        onClick={()=> {navigate(MENU_ROUTE+'/'+id)}}>X</Button>
            </div>
            <hr className='mt-1 mb-2'/>
            <div className='d-flex'>
                {ActionList.map(e =>
                    //(e.id == 1 || e.id == 3) && user.rights.includes(type.code + '.' + 'Edit') || e.id == 2 || e.id == 4 ?
                    <div key={e.id}>
                        <Image height='25px' width='25px' src={e.icon}/>
                        <Button onClick={e.action} className='p-1 ms-1 me-1'
                                variant='outline-dark'>{e.name}</Button>
                    </div> //: null
                )}
            </div>
            <hr className='mt-2 mb-1'/>
            <div>
                {
                    type.id?
                    <FieldText setValue={SetObjName} value={objName} name="Название" minlen={1} maxlen={255} id={0}
                               placeholder="Название объекта" nullable={false} key={-1} disabled={!isEdit}/>
                        :null
                }
                {
                    !isNewObj?
                    obj.attributes.map(e =>
                        <Field isComplex={e.isComplexType} SetModalD={(data) => SetMSelectD(data)} type={e.type} setValue={setValue}
                               value={e.value} name={e.name} minlen={1} maxlen={255} id={e.number} regex={e.regEx} objId={objId}
                                   placeholder="Не заполнено" nullable={false} key={e.number} disabled={!isEdit} />
                    )
                        :
                    type.attributes.map(e =>
                        <Field isComplex={e.isComplex} SetModalD={(data) => SetMSelectD(data)} type={e.type} setValue={setValue}
                               value={e.value} name={e.name} minlen={1} maxlen={255} id={e.number} regex={e.regEx} objId={objId}
                                   placeholder="Не заполнено" nullable={false} key={e.number} disabled={!isEdit}/>
                    )
                }
            </div>
            <ModalYesNoMy title="Вы действительно хотите удалить объект?" notitle="Отмена" yestitle="Удалить"
                          show={show} onHide={() => SetShow(false)} final={() => {
                              DeleteObj(objId).then(data => navigate(MENU_ROUTE + '/'+id))
                          }}/>

        </div>
    )
}

export default ObjCardComponent