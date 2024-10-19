import {DataGrid} from "@mui/x-data-grid";
import Box from "@mui/material/Box";
import React, {useEffect, useState} from "react";
import {Button, Image} from "react-bootstrap";
import iconCreate from "../../assets/icons8-addFile.png";
import {CONFIG_ROUTE, MENU_ROUTE} from "../../utils/consts";
import iconView from "../../assets/icons8-viewFile.png";
import iconUpdate from "../../assets/icons8-restart.svg";
import {useNavigate, useParams} from "react-router-dom";
import {GetAllAttrTypes, GetOneAttrType} from "../../http/AttrTypeApi"
import ObjTypeCard from "./ObjTypeCard";
import AttrTypeCard from "./AttrTypeCard";
import GridTheme from "../../CSS/GridTheme";
import {ThemeProvider} from "@mui/material";

const ConfigAttrTypeComponent = () => {

    const navigate = useNavigate()
    const {id, typeId} = useParams()

    const [visible, SetVisible] = useState(false);

    useEffect(()=>{
        if(id != null) {
            //console.log(id);
            LoadData();
        }
        else
            SetVisible(false);
    }, [id])

    const [typesData, SetTypesData] = useState({});
    const [rows, SetRows] = useState([]);
    const [columns, SetColumns] = useState([
        { field: 'id', headerName: 'Id', width: 50 },
        { field: 'name', headerName: 'Название', width: 150 },
    ]);

    const [selectionIds, SetSelectionIds] = useState([]);

    const LoadData = () => {
        GetAllAttrTypes().then(data => {
            //console.log(data);
            SetTypesData(data);
            let arr = [];
            data.forEach(e => {
                let elem = {id: e.id, name: e.name};
                //console.log(elem);
                arr.push(elem);
            });
            SetRows(arr);
            SetVisible(true);
        });
    }

    return (
        <div className=' W-80 d-flex'>
            <div className=' W-50'>
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
                <ThemeProvider theme={GridTheme}>
                <Box sx={{height: window.innerHeight-215+'px', width: '100%'}}>
                    <DataGrid rows={rows} columns={columns}
                              onRowSelectionModelChange={(ids) =>{SetSelectionIds(ids)}}/>
                </Box>
                </ThemeProvider>

            </div>
            <div className='ms-2 W-50 Block'>
                {
                    typeId?
                        <AttrTypeCard reloadGrid={() => LoadData()} />
                        : null
                }
            </div>
        </div>
    )
}

export default ConfigAttrTypeComponent