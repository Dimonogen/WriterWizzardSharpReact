import {useNavigate, useParams} from "react-router-dom";
import React, {useEffect, useState} from "react";
import {CreateType, DeleteType, GetOneType, UpdateObjType} from "../../http/ObjTypeApi";
import {Button} from "react-bootstrap";
import {CONFIG_ROUTE, MENU_ROUTE} from "../../utils/consts";
import FieldText from "../elementary/FieldText";
import ModalYesNoMy from "../modals/ModalYesNoMy";
import AttributeComponent from "../elementary/AttributeComponent";
import {GetAllAttrTypes} from "../../http/AttrTypeApi";

const ObjTypeCard = ({reloadGrid}) => {

    const navigate = useNavigate()
    const {id, typeId} = useParams()

    let isNewType = typeId == 0;

    const [attributesMap,SetAttributesMap] = useState([]);
    const [attrValues, SetAttrValues] = useState({});

    const [attribTypes, SetAttibTypes] = useState([])

    const [isEdit, SetIsEdit] = useState(false);
    const [showDel1, SetShowDel1] = useState(false);
    const [showDel2, SetShowDel2] = useState(false);

    const [typeData, SetTypeData] = useState({attributes:[]})
    const [typeName, SetTypeName] = useState("")
    const [typeCode, SetTypeCode] = useState("")
    const [typeDescr, SetTypeDescr] = useState("")

    useEffect(()=>{
        isNewType = typeId == 0;
        GetAllAttrTypes().then(data => SetAttibTypes(data));
        if(!isNewType)
        GetOneType(typeId).then(data=>{
            SetAttrValues({});
            SetIsEdit(false);
            SetTypeData(data);
            SetTypeName(data.name);
            SetTypeDescr(data.description);
            SetAttributesMap(data.attributes);
        });
        else {
            SetTypeName("");
            SetTypeDescr("");
            SetTypeData({name: "Новый тип"});
            SetIsEdit(true);
            SetAttrValues({});
            SetAttributesMap([]);
        }
    }, [typeId])


    const SetValue = (attribute, id) => {
        //console.log(attribute, id)
        let n = attrValues;
        n[id] = attribute;
        SetAttrValues(n);
    }

    const attrTrans = (attributes) => {
        let list = []
        for (const [key, value] of Object.entries(attributes)) {
            list.push(value);
        }
        console.log(list)
        return list;
    }


    return (
        <div>
            <div className='W-100 d-flex h-100'>
                <span>{"Тип - " + typeData.name}</span>
                <Button className='p-1 ms-auto' variant={isEdit?'dark':'outline-dark'} onClick={() => {
                    if(isEdit) {//save data
                        //console.log(attrValues);
                        if (isNewType) {
                            console.log("code:", typeCode);
                            CreateType({name: typeName, description: typeDescr, code: typeCode,
                                attributes: attrTrans(attrValues)
                            }).then(data => {
                                SetTypeData(data);
                                SetTypeName(data.name);
                                SetTypeDescr(data.description);
                                navigate(CONFIG_ROUTE+'/'+id+'/'+data.id);
                                reloadGrid();
                            });

                        } else {
                            UpdateObjType({
                                id: typeData.id, name: typeName, description: typeDescr, code: typeCode,
                                attributes: attrTrans(attrValues)
                            })
                                .then(data => {
                                    SetTypeData(data);
                                    SetTypeName(data.name);
                                    SetTypeDescr(data.description);
                                    reloadGrid()
                                });
                        }
                    }
                    SetIsEdit(!isEdit);
                }}>
                    {isEdit?"Сохранить":"Редактировать"}</Button>
                <Button className='p-1 ms-2' variant='outline-dark' onClick={()=>SetShowDel1(true)}>Удалить</Button>
            </div>
            <hr/>

            <div className='ps-2 pe-2' style={{overflowY: 'auto', maxHeight: '500px'}}>
                <FieldText setValue={SetTypeName} value={typeName} name="Название" minlen={1} maxlen={255} id={0}
                           placeholder="Название типа" nullable={false} key={-3} disabled={!isEdit} useProp={true}/>
                <FieldText setValue={SetTypeCode} value={typeCode} name="Код" minlen={1} maxlen={255} id={0}
                           placeholder="Описание типа" nullable={false} key={-2} disabled={!isEdit} useProp={true}/>
                <FieldText setValue={SetTypeDescr} value={typeDescr} name="Описание" minlen={1} maxlen={255} id={0}
                           placeholder="Описание типа" nullable={false} key={-1} disabled={!isEdit} useProp={true}/>
                {
                    attributesMap.map(e =>
                    <AttributeComponent key={e.number} value={e} setValue={SetValue} isEdit={isEdit} attrTypes={attribTypes} />
                    )
                }
                {
                    isEdit?
                    <div className='d-flex justify-content-center'>
                        <Button variant='outline-dark' onClick={() => {
                            //console.log(attributesMap);
                            let new_num = 0;
                            for (const [key, value] of Object.entries(attributesMap)) {
                                if(new_num <= value.number)
                                    new_num = value.number + 1;
                            }
                            SetAttributesMap(attributesMap.concat([{number:new_num, name:"", typeId:1}]))
                        }}>
                            Добавить атрибут
                        </Button>
                    </div>:null
                }
            </div>
            <ModalYesNoMy title="Внимание! Все объекты удаляемого типа будут удалены! Вы действительно хотите удалить тип?" notitle="Отмена" yestitle="Удалить"
                          show={showDel1} onHide={() => SetShowDel1(false)} final={() => {
                SetShowDel2(true);
            }}/>
            <ModalYesNoMy title="Внимание! Данное действие не обратимо! Вы действительно хотите удалить тип?" notitle="Отмена" yestitle="Удалить"
                          show={showDel2} onHide={() => SetShowDel2(false)} final={() => {
                DeleteType(typeId).then(data => {reloadGrid();navigate(CONFIG_ROUTE + '/'+id)})
            }}/>
        </div>
    )
}

export default ObjTypeCard