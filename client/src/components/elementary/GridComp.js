import {Button, Image} from "react-bootstrap";
import iconCreate from "../../assets/icons8-addFile.png";
import {CONFIG_ROUTE} from "../../utils/consts";
import iconView from "../../assets/icons8-viewFile.png";
import iconUpdate from "../../assets/icons8-restart.svg";
import Box from "@mui/material/Box";
import {DataGrid} from "@mui/x-data-grid";
import React, {useEffect, useState} from "react";
import {useNavigate, useParams} from "react-router-dom";


const GridComp = ({LoadData}) => {

    const navigate = useNavigate()
    const {id, typeId} = useParams()

    const [visible, SetVisible] = useState(false);

    useEffect(()=>{
        if(id != null) {
            //console.log(id);
            const {row, column} = LoadData();
            SetRows(row);
            SetColumns(column);
        }
        else
            SetVisible(false);
    }, [id])

    const [typesData, SetTypesData] = useState({});
    const [rows, SetRows] = useState([]);
    const [columns, SetColumns] = useState([
        { field: 'id', headerName: 'Id', width: 50 },
        { field: 'name', headerName: 'Название', width: 150 },
        { field: 'descr', headerName: 'Описание', width: 230 },
    ]);

    const [selectionIds, SetSelectionIds] = useState([]);

    return (
        <div className='W-100'>
            <div className='d-flex flex-row'>
                <Image className='m-2 ms-1 me-0' height='32px' width='32px' src={iconCreate}/>
                <Button variant='dark' className='m-2'
                        onClick={()=>{navigate(CONFIG_ROUTE+'/'+id+'/0')}}>Создать</Button>
                <Image className='m-2 ms-1 me-0' height='32px' width='32px' src={iconView}/>
                <Button variant='outline-dark' className='m-2'
                        onClick={()=>{
                            if(selectionIds.length > 0)
                                navigate(CONFIG_ROUTE+'/'+id+'/'+selectionIds[0])
                        }}
                >Открыть</Button>
                <Image className='m-2 ms-1 me-0' height='32px' width='32px' src={iconUpdate}/>
                <Button variant='outline-dark' className='m-2'
                        onClick={()=>{LoadData(id)}}>Обновить</Button>
            </div>
            <Box sx={{height: window.innerHeight-150+'px', width: '100%'}}>
                <DataGrid rows={rows} columns={columns}
                          onRowSelectionModelChange={(ids) =>{SetSelectionIds(ids)}}/>
            </Box>
        </div>
    )
}

export default GridComp