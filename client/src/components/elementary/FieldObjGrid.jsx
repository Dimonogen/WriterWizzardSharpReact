import {Button, Form, Image} from "react-bootstrap";
import React, {useEffect, useState} from "react";
import {CreateLinkObj, GetAllOneTypeObjects, GetLinkedObj, GetOneObj} from "../../http/ObjAPI";
import {MENU_ROUTE} from "../../utils/consts";
import {NavLink, useNavigate} from "react-router-dom";
import ModalSelectObj from "../modals/ModalSelectObj";
import Box from '@mui/material/Box';
import {DataGrid} from "@mui/x-data-grid";
import {GetOneType} from "../../http/ObjTypeApi";
import iconView from "../../assets/icons8-viewFile.png";
import iconCreate from "../../assets/icons8-addFile.png";
import iconLink from "../../assets/icons8-add-link.png";
import ModalCreateObj from "../modals/ModalCreateObj";

const FieldObjGrid = ({type, objId, name, disable, SetMSelectD}) => {

    useEffect(() => {
        if(objId)
            LoadData()
    }, [type, objId])

    const [rows, SetRows] = useState([])
    const [columns, SetColumns] = useState([])
    const [selectionIds, SetSelectionIds] = useState([]);

    const [MCShow, SetMCShow] = useState(false);

    const navigate = useNavigate()

    const LoadData = () => {
        GetLinkedObj(objId).then(data => {
            //console.log(data)
            let arr = [];
            data.forEach(e => {
                let elem = {id: e.id, state:e.state, name: e.name};
                e.attributes.forEach(a => elem["c"+a.number] = a.value)
                arr.push(elem);
            });
            SetRows(arr);
            }
        )
        GetOneType(type).then(data => {
            let arr = [{field: 'id', headerName: 'Id', width: 50},
                {field: 'state', headerName: 'Статус', width: 120},
                {field: 'name', headerName: 'Название', width: 150}
            ];
            data.attributes.forEach(e => arr.push({field: 'c'+e.number, headerName: e.name,
                width: 150
            }));
            SetColumns(arr);
        });
    }

    return (
        <div>
            <ModalCreateObj title={'Создание документа'} objType={type} show={MCShow} onHide={() => SetMCShow(false)}
                            final={(id) => {CreateLinkObj(objId, id).then(data => LoadData())}} SetMSelectD={SetMSelectD}/>
            <Form.Group className="mb-3 w-100">
                <Form.Label style={{textTransform: "capitalize"}}>{name}</Form.Label>
                <div className='border rounded p-1'>
                    <div>
                        <Image className='m-2 ms-1 me-0' height='24px' width='24px' src={iconCreate}/>
                        <Button variant='dark' className='m-2 p-1'
                                onClick={()=>{SetMCShow(true)}}>Создать</Button>
                        <Image className='m-2 ms-1 me-0' height='24px' width='24px' src={iconView}/>
                        <Button variant='outline-dark' className='m-2 p-1'
                                onClick={()=>{
                                    if(selectionIds.length > 0)
                                        navigate(MENU_ROUTE+'/'+type+'/'+selectionIds[0])
                                }}
                        >Открыть</Button>
                        <Image className='m-2 ms-1 me-0' height='24px' width='24px' src={iconLink}/>
                        <Button variant='outline-dark' className='m-2 p-1'
                                onClick={()=>{navigate(MENU_ROUTE+'/'+type+'/0')}}>Добавить существующий</Button>
                    </div>

                    <Box sx={{height: window.innerHeight*0.4+'px', width: '100%'}}>
                        <DataGrid rows={rows} columns={columns}
                                  onRowSelectionModelChange={(ids) =>{SetSelectionIds(ids)}}  />
                    </Box>
                </div>
            </Form.Group>
        </div>
    )
}

export default FieldObjGrid