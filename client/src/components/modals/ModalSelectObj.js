import React, {useEffect, useState} from 'react';
import {Button} from "react-bootstrap";
import {DataGrid} from "@mui/x-data-grid";
import Box from "@mui/material/Box";
import {GetAllObjects, GetAllOneTypeObjects} from "../../http/ObjAPI";
import {GetOneType} from "../../http/ObjTypeApi";

const ModalSelectObj = ({show, onHide, final, title, objType}) => {

    let classModal = 'modal-my ';

    useEffect(() => {
        //console.log(objType)
        if (objType != null) {
            if (objType == 0)
            {
                GetAllObjects().then(data => {
                    let arr = [];
                    data.forEach(e => {
                        let elem = {id: e.id, name: e.name, type: e.typeName};

                        //console.log(elem);
                        arr.push(elem);
                    });
                    SetRows(arr);
                    let arrC = [{field: 'id', headerName: 'Id', width: 50},
                        {field: 'name', headerName: 'Название', width: 150},
                        {field: 'type', headerName: 'Тип', width: 150}
                    ];
                    SetColumns(arrC);
                });
            }
            else {
                GetAllOneTypeObjects(objType).then(data => {
                    let arr = [];
                    data.forEach(e => {
                        let elem = {id: e.id, name: e.name};
                        e.attributes.forEach(a => elem["c" + a.number] = a.value)
                        //console.log(elem);
                        arr.push(elem);
                    });
                    SetRows(arr);
                });
                GetOneType(objType).then(data => {
                    let arr = [{field: 'id', headerName: 'Id', width: 50},
                        {field: 'name', headerName: 'Название', width: 150}
                    ];
                    data.attributes.forEach(e => arr.push({
                        field: 'c' + e.number, headerName: e.name,
                        width: 150
                    }));
                    SetColumns(arr);
                });
            }
        }
    }, [objType])

    const [rows, SetRows] = useState([])
    const [columns, SetColumns] = useState([])
    const [selectionIds, SetSelectionIds] = useState([]);

    return (
        <div onClick={(e)=>{if(e.target.id == 'myModalSelectObj'){onHide()}}}
             id="myModalSelectObj" className={classModal + (show ? 'd-block' : 'd-none')}>
            <div className="modal-content-center-my W-90">
                <div className="modal-header-my ">
                    <h4 className='text-dark'>{title}</h4>
                </div>
                <div className="modal-body-my">
                    {
                        show ?
                        <Box sx={{height: Math.max(window.innerHeight * 0.5, 300) + 'px', width: '100%'}}>
                            <DataGrid rows={rows} columns={columns}
                                      onRowSelectionModelChange={(ids) => {
                                          SetSelectionIds(ids)
                                      }}/>
                        </Box>
                            : null
                    }
                </div>
                <div className="modal-footer-my">

                    <Button className='me-2' variant='outline-dark' onClick={() => onHide()}>Отмена</Button>
                    <Button variant='dark' onClick={() => {final(selectionIds[0]); onHide()}}>Выбрать</Button>
                </div>
            </div>
        </div>

    );
};

export default ModalSelectObj;