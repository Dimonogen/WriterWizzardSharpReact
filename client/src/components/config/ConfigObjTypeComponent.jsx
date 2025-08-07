import {DataGrid} from "@mui/x-data-grid";
import Box from "@mui/material/Box";
import React, {useContext, useEffect, useState} from "react";
import {Button, Image} from "react-bootstrap";
import iconCreate from "../../assets/Create_B.svg";
import {CONFIG_ROUTE, MENU_ROUTE} from "../../utils/consts";
import iconView from "../../assets/Open_B.svg";
import iconUpdate from "../../assets/icons8-restart.svg";
import {useNavigate, useParams} from "react-router-dom";
import {GetAllObjTypes, GetOneType} from "../../http/ObjTypeApi"
import ObjTypeCard from "./ObjTypeCard";
import GridTheme from "../../CSS/GridTheme";
import {ThemeProvider} from "@mui/material";
import {Context} from "../../ContextApp";

const ConfigObjTypeComponent = () => {

    const navigate = useNavigate()
    const {id, typeId} = useParams()

    const [visible, SetVisible] = useState(false);

    const {user} = useContext(Context);

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
        { field: 'descr', headerName: 'Описание', width: 230 },
    ]);

    const [selectionIds, SetSelectionIds] = useState([]);

    const LoadData = () => {
        GetAllObjTypes().then(data => {
            //console.log(data);
            SetTypesData(data);
            let arr = [];
            data.forEach(e => {
                let elem = {id: e.id, name: e.name, descr: e.description};
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
                    <Box sx={{height: Math.max(window.innerHeight-215,400)+'px', width: '100%'}}>
                        <DataGrid rows={rows} columns={columns}
                                  onRowDoubleClick={(params) => {
                                      //console.log(objData.find((x) => x.id == params.id));
                                      user.setPath(typesData.find((x) => x.id == params.id).name, 1);
                                      navigate(CONFIG_ROUTE + '/' + id + '/' + params.id)} }
                              onRowSelectionModelChange={(ids) =>{SetSelectionIds(ids)}}/>
                    </Box>
                </ThemeProvider>
            </div>
            <div className='ms-2 W-50 Block'>
                {
                    typeId?
                        <ObjTypeCard reloadGrid={() => LoadData()} />
                        : null
                }
            </div>
        </div>
    )
}

export default ConfigObjTypeComponent