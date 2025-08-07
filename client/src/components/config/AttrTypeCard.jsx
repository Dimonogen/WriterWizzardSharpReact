import {useNavigate, useParams} from "react-router-dom";
import React, {useContext, useEffect, useState} from "react";
import {CreateType, DeleteType, GetAllObjTypes, GetOneType, UpdateObjType} from "../../http/ObjTypeApi";
import {Button} from "react-bootstrap";
import {CONFIG_ROUTE, MENU_ROUTE} from "../../utils/consts";
import FieldText from "../elementary/FieldText";
import ModalYesNoMy from "../modals/ModalYesNoMy";
import AttributeComponent from "../elementary/AttributeComponent";
import {CreateAttrType, DeleteAttrType, GetOneAttrType, UpdateAttrType} from "../../http/AttrTypeApi";
import {Form} from "react-bootstrap";
import {Context} from "../../ContextApp";

const AttrTypeCard = ({reloadGrid}) => {

    const navigate = useNavigate()
    const {id, typeId} = useParams()

    const {user} = useContext(Context)

    let isNewType = typeId == 0;

    const [objTypes, SetObjTypes] = useState([])

    const [isEdit, SetIsEdit] = useState(false);
    const [showDel1, SetShowDel1] = useState(false);
    const [showDel2, SetShowDel2] = useState(false);

    const [typeData, SetTypeData] = useState({})
    const [typeName, SetTypeName] = useState("");
    const [isComplex, SetIsComplex] = useState(false);
    const [typeVariant, SetTypeVariant] = useState(0);
    const [typeRegEx, SetTypeRegEx] = useState(".+");


    const getData = () => {
      return {id: typeData.id, name: typeName, isComplex: isComplex, type: typeVariant, regEx: typeRegEx}
    }

    useEffect(()=>{
        isNewType = typeId == 0;
        GetAllObjTypes().then(data =>{ data.push({id: 0, name: 'Все типы'}); SetObjTypes(data)});
        if(!isNewType)
            GetOneAttrType(typeId).then(data=>{
                user.setPath(data.name, 1);
                SetTypeData(data);
                console.log(data);
                SetIsEdit(false);
                SetTypeName(data.name);
                SetIsComplex(data.isComplex);
                SetTypeRegEx(data.regEx);
                SetTypeVariant(data.type);
            });
        else {
            user.setPath("Новый атрибут", 1);
            SetTypeName("");
            SetIsEdit(true);
        }
    }, [typeId])

    return (
        <div>
            <div className='W-100 d-flex h-100'>
                <span>{typeName + " - тип Атрибута"}</span>
                <Button className='p-1 ms-auto' variant={isEdit?'dark':'outline-dark'} onClick={() => {
                    if(isEdit) {//save data
                        //console.log(attrValues);
                        if (isNewType) {
                            CreateAttrType(getData()).then(data => {});
                        } else {
                            UpdateAttrType(getData()).then(data => {});
                        }
                        reloadGrid();
                    }
                    SetIsEdit(!isEdit);
                }}>
                    {isEdit?"Сохранить":"Редактировать"}</Button>
                <Button className='p-1 ms-2' variant='outline-dark' onClick={()=>SetShowDel1(true)}>Удалить</Button>
            </div>
            <hr/>

            <div className='ps-2 pe-2' style={{overflowY: 'auto', maxHeight: '500px'}}>
                <FieldText setValue={SetTypeName} value={typeName} name="Название" minlen={1} maxlen={255} id={0}
                           placeholder="Название типа атрибута" nullable={false} key={1} disabled={!isEdit} useProp={true}/>
                <Form.Group>
                    <Form.Switch disabled={!isEdit} checked={isComplex} onChange={e => {SetIsComplex(e.target.checked);}} label="Сложный тип" type='switch'/>
                </Form.Group>
                {
                    isComplex?
                        <Form.Group className="mb-3">
                            <Form.Select disabled={!isEdit} value={typeVariant} onChange={(e)=>{SetTypeVariant(Number(e.target.value));} } >
                                {
                                    objTypes.map(e =>
                                        <option key={e.id} value={e.id}>{e.name}</option>
                                    )
                                }
                            </Form.Select>
                        </Form.Group>
                        :
                        <Form.Group className="mb-3">
                            <Form.Select disabled={!isEdit} value={typeVariant} onChange={(e) => {SetTypeVariant(Number(e.target.value))}}>
                                <option value={0}>Текст</option>
                                <option value={1}>Число</option>
                                <option value={2}>Дата</option>
                            </Form.Select>



                        </Form.Group>
                }
                <FieldText setValue={SetTypeRegEx} value={typeRegEx} name="Регулярное выражение" minlen={1} maxlen={255} id={0}
                           placeholder="Регулярное выражение для типа" nullable={true} key={1} disabled={!isEdit} useProp={true}/>
            </div>
            <ModalYesNoMy title="Внимание! Все объекты удаляемого типа будут удалены! Вы действительно хотите удалить тип?" notitle="Отмена" yestitle="Удалить"
                          show={showDel1} onHide={() => SetShowDel1(false)} final={() => {
                SetShowDel2(true);
            }}/>
            <ModalYesNoMy title="Внимание! Данное действие не обратимо! Вы действительно хотите удалить тип?" notitle="Отмена" yestitle="Удалить"
                          show={showDel2} onHide={() => SetShowDel2(false)} final={() => {
                DeleteAttrType(typeId).then(data => {reloadGrid(); navigate(CONFIG_ROUTE+'/'+id)})
            }}/>
        </div>
    )
}

export default AttrTypeCard