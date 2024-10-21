import {Button, Image, OverlayTrigger, Spinner, Tooltip} from "react-bootstrap";
import {useNavigate, useParams} from "react-router-dom";
import {MENU_ROUTE, TRESHCAN_ROUTE} from "../../utils/consts";
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
import AttributeComponent from "../elementary/AttributeComponent";
import {GetAllAttrTypes} from "../../http/AttrTypeApi";
import AttributeValAndConfComponent from "../elementary/AttributeValAndConfComponent";
import {wait} from "@testing-library/user-event/dist/utils";

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

    const [attributeEdits, SetAttributeEdits] = useState({});
    const [attributeExt, SetAttributeExt] = useState({});

    const [newAttrib, SetNewAttrib] = useState([]);
    const [LastId, SetLastId] = useState(1);
    const [attrValues, SetAttrValues] = useState({});

    const [attribTypes, SetAttibTypes] = useState([])

    const setHistory = (name) => {
        //let arr = user.path;
        //if(arr.length == 1)
        //    arr.push(name);
        //else
        //    arr[1] = name;
        user.setPath(name, 1);
    }

    useEffect(() => {
        LoadData();
    }, [id, objId])



    const LoadExtAttrib = (data) => {
        let ids = []
        let lastid = 1;
        let obj = attrValues;
        //console.log(data);
        data.extAttributes.forEach(e => {
            ids.push({id:e.number, value: e.value})
            obj[e.number] = {isComplex: e.isComplexType, value: e.value, number: e.number, name:e.name, typeId: e.typeId};
            if(e.number > lastid)
                lastid = e.number;
        })
        //console.log(obj, lastid);
        SetAttrValues(obj);
        SetNewAttrib(ids);
        SetLastId(lastid + 1);
    }

    const LoadData = () => {
        SetObj({attributes:[]});
        SetType({attributes:[]});
        SetAttrValues({});
        SetNewAttrib([]);
        GetAllAttrTypes().then(data => SetAttibTypes(data));
        if(!isNewObj) {
            GetOneObj(objId).then(data => {
                //console.log(data)
                SetObj(data);
                SetObjName(data.name);
                setHistory(data.name);

                LoadExtAttrib(data);

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
    }

    const attrTrans = (attributes) => {
        //console.log(attributes)
        let list = []
        for (const [key, value] of Object.entries(attributes)) {
            list.push({number: key, value: value});
        }
        //console.log(list)
        return list;
    }

    const attrTransExt = (attributes) => {
        //console.log(attributes)
        let list = []
        for (const [key, value] of Object.entries(attributes)) {
            list.push({number: key, value: value.value, name: value.name, typeId: value.typeId});
        }
        //console.log(list)
        return list;
    }

    let ActionList = [
        {id:1, icon: iconEdit, name: isEdit?"Сохранить":"Изменить", action: () => {
            if(isEdit)
            {//сохранить
                //console.log(attrTrans(attributeEdits))
                if(objId != 0)
                UpdateObj({id: objId, typeId: id, name: objName,
                    attributes: attrTrans(attributeEdits), extAttributes: attrTransExt(attributeExt)})
                    .then(data => {SetObj(data); LoadExtAttrib(data)}).catch(e => console.log(e));
                else
                {//create obj
                    //console.log('create obj attributes', attributeEdits);
                CreateObj({typeId: id, name: objName,
                    attributes: attrTrans(attributeEdits), extAttributes: attrTransExt(attributeExt)})
                    .then(data =>
                {
                    SetObj(data);
                    navigate(MENU_ROUTE + '/' + type.id + '/' + data.id);
                });
                }
            }
                SetIsEdit(!isEdit);
            }},
        //{id:2, icon: iconState, name: "Состояние", action: () => {}},

        {id:3, icon: iconUpdate, name: "Обновить", action: () => {LoadData()}},
        {id:4, icon: iconDelete, name: "Удалить", action: () => {SetShow(true)}},
    ]

    const setValue = (value, id) => {
        let edit = attributeEdits;
        edit[id] = value.toString();
        //console.log("value = " + value + ' number = '+ id)
        //console.log("AE = ", edit);
        SetAttributeEdits(edit);
    }

    const setValueExt = (value, id) => {
        let edit = attributeExt;
        edit[id] = value;
        //console.log("value = " + value + ' number = '+ id)
        //console.log("AE = ", edit);
        let obj = attrValues;
        obj[id].name = value.name;
        obj[id].value = value.value;
        obj[id].typeId = value.typeId;
        SetAttrValues(obj);
        SetAttributeExt(edit);
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
            <div className='W-100 d-flex fs-4'>
                <span>{objName + ' - ' + type.name}</span>
                <div className='d-flex ms-auto me-auto'>
                    {ActionList.map(e =>
                        //(e.id == 1 || e.id == 3) && user.rights.includes(type.code + '.' + 'Edit') || e.id == 2 || e.id == 4 ?
                        <div key={e.id}>

                            <OverlayTrigger overlay={<Tooltip className="fs-6">{e.name}</Tooltip>} placement="top">
                                <Button onClick={e.action} className='p-1 ms-1 me-1 '
                                        variant='outline-dark'><Image height='25px' width='25px' src={e.icon}/></Button>
                            </OverlayTrigger>

                        </div> //: null
                    )}
                </div>
                <div className="ms-3">
                    <OverlayTrigger overlay={<Tooltip className="fs-6">Закрыть окно</Tooltip>} placement="top">
                        <Button className='pt-1 fs-5 p-0 ps-2 pb-1 pe-2' variant='outline-dark'
                                onClick={()=> {
                                    if(id == undefined)
                                        navigate(TRESHCAN_ROUTE);
                                    else
                                        navigate(MENU_ROUTE+'/'+id)
                                }}><span className="ms-1 me-1">X</span></Button>
                    </OverlayTrigger>
                </div>

            </div>
            <hr className='mt-3 mb-4'/>
            <div>
                <div style={{marginRight: "42px"}}>
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
                {
                    newAttrib.map(e =>
                        <AttributeValAndConfComponent key={e.id} e={e} value={attrValues[e.id]} global={{isEdit: isEdit,
                            attribTypes: attribTypes, SetModal: (data) => SetMSelectD(data), setValue: setValueExt}}/>
                    )
                }
                <div className="d-flex mt-3">
                    <Button className="ms-auto me-auto" variant="outline-dark" onClick={() => {
                        SetIsEdit(true);
                        let arr = newAttrib;
                        arr.push({id: LastId});
                        let obj = attrValues;
                        obj[LastId] = {isComplex: false, number: LastId, name:"", typeId: 1, value: ""};
                        SetAttrValues(obj);
                        SetLastId(LastId + 1);
                        SetNewAttrib(arr);

                    }}>Добавить поле</Button>
                </div>

            </div>
            <ModalYesNoMy title="Вы действительно хотите удалить объект?" notitle="Отмена" yestitle="Удалить"
                          show={show} onHide={() => SetShow(false)} final={() => {
                              DeleteObj(objId).then(data => navigate(MENU_ROUTE + '/'+id))
                          }}/>

        </div>
    )
}

export default ObjCardComponent