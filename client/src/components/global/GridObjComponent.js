import {useNavigate, useParams} from "react-router-dom";
import {DataGrid, useGridApiRef} from "@mui/x-data-grid";
import {DeleteObj, DeleteObjList, GetAllOneTypeObjects} from "../../http/ObjAPI";
import {GetOneType} from "../../http/ObjTypeApi"
import React, {useContext, useEffect, useState} from "react";
import Box from '@mui/material/Box';
import {Button, Image, OverlayTrigger, Tooltip} from "react-bootstrap";
import {MENU_ROUTE} from "../../utils/consts";
import ModalYesNoMy from "../modals/ModalYesNoMy";
import ModalOkMy from "../modals/ModalOkMy";
import iconCreate from '../../assets/icons8-addFile.png'
import iconView from '../../assets/icons8-viewFile.png'
import iconDelete from '../../assets/icons8-delete.svg'
import iconUpdate from '../../assets/icons8-restart.svg'
import {Context} from "../../index";
import {getUserSettings, saveUserSettings} from "../../http/UserSettingsAPI";
import LoadingAnimComponent from "./LoadingAnimComponent";
import GridTheme from "../../CSS/GridTheme";
import {ThemeProvider} from "@mui/material";
import iconEdit from "../../assets/icons8-edit.svg";

const GridObjComponent = () => {

    const navigate = useNavigate();
    const {id, objId} = useParams()
    const {user} = useContext(Context);
    const apiRef = useGridApiRef();

    const [rights, SetRights] = useState(user.rights)
    //console.log(user.rights)

    //console.log(user)

    const [visible, SetVisible] = useState(false);
    const [IsLoading, SetIsLoading] = useState(false);
    const [show, SetShow] = useState(false);
    const [showOk, SetOkShow] = useState(false);

    useEffect(()=>{
        if(id != null) {
            //console.log(id);
            LoadData(id);
        }
        else
            SetVisible(false);
    }, [id, objId])

    const [objData, SetObjData] = useState([]);
    const [typeData, SetTypeData] = useState({});
    const [rows, SetRows] = useState([]);
    const [columns, SetColumns] = useState([
        { field: 'col1', headerName: 'Id', width: 50 },
        { field: 'col2', headerName: 'Название', width: 150 },
    ]);

    const [selectionIds, SetSelectionIds] = useState([]);

    const LoadData = (id) => {
        SetIsLoading(true);
        SetVisible(true);
        GetAllOneTypeObjects(id).then(data => {SetObjData(data);

            let arr = [];
            data.forEach(e => {
                let elem = {id: e.id, state:e.state, name: e.name};
                e.attributes.forEach(a => elem["c"+a.number] = a.value)
                //console.log(elem);
                arr.push(elem);
            });
            SetRows(arr);
        });
        GetOneType(id).then(data => {
            //console.log(data);
            SetTypeData(data);
            let arr = [{field: 'id', headerName: 'Id', width: 50},
                {field: 'state', headerName: 'Статус', width: 120},
                {field: 'name', headerName: 'Название', width: 150}
            ];
            data.attributes.forEach(e => arr.push({field: 'c'+e.number, headerName: e.name,
                width: 150
            }));
            SetColumns(arr);
            getUserSettings("GridTypeId"+data.id).then(dataI => {
                if (dataI != "")
                    apiRef.current.restoreState(dataI);
                SetIsLoading(false);
            })
            //console.log(rights)
            //console.log(rights.includes(typeData.code + '.' + 'Edit'))
        });
    }

    const ObjDelMsg = () => {
        let end;
        let count = selectionIds.length;
        if(count < 2)
            end = ' объект?';
        else if (count < 5)
            end = ' объекта?';
        else
            end = ' объектов?';

        return "Вы действительно хотите удалить "+ count + end;
    }


    let ActionList = [
        {id:1, icon: iconCreate, name: "Новый "+typeData.name, action: () => {
                navigate(MENU_ROUTE+'/'+id+'/0')
            } },
        {id:2, icon: iconView, name: "Открыть "+typeData.name, action: () => {
                if(selectionIds.length > 0)
                    navigate(MENU_ROUTE+'/'+id+'/'+selectionIds[0])
            } },
        {id:3, icon: iconUpdate, name: "Обновить "+typeData.name, action: () => {
                LoadData(id)
            } },
        {id:4, icon: iconDelete, name: "Удалить "+typeData.name, action: () => {
                if(selectionIds.length > 0)
                {
                    SetShow(true);
                }
                else
                {
                    SetOkShow(true);
                }
            } }
    ]


    return (

       visible ?
           <div className='W-100'>
               <div style={{overflowX: "auto", overflowY: "clip"}} className={'d-flex flex-row mb-3 ' + (IsLoading ? "d-none" : "d-block")}>

                   {
                       ActionList.map(e =>
                       <OverlayTrigger key={e.id} overlay={<Tooltip className="fs-6">{e.name}</Tooltip>} placement="top">
                           <Button onClick={e.action} className='p-2 ms-1 me-1'
                                   variant='outline-dark'><Image height='32px' width='32px' src={e.icon}/></Button>
                       </OverlayTrigger>
                       )
                   }

               </div>
               <ThemeProvider theme={GridTheme}>
           <Box className={(IsLoading ? "d-none" : "d-block")} sx={{height: Math.max(window.innerHeight-215, 500)+'px', width: '100%'}}>
               <DataGrid rows={rows} columns={columns} checkboxSelection
                         prop
                         onRowSelectionModelChange={(ids) =>{SetSelectionIds(ids)}}
                         getRowClassName={(params) =>
                             params.indexRelativeToCurrentPage % 2 === 0 ? 'even' : 'odd'
                         }
                         onRowDoubleClick={(params) => {navigate(MENU_ROUTE + '/' + id + '/' + params.id)} }
                         onColumnWidthChange={(params) => {
                             let state = apiRef.current.exportState();
                             state = JSON.stringify(state);
                             saveUserSettings("GridTypeId"+typeData.id, {settings:state}).then(data => console.log(data));
                         }}
                         apiRef={apiRef}

               />
           </Box>
               </ThemeProvider>
               { IsLoading ? <LoadingAnimComponent/> : null }
               <ModalYesNoMy title={ObjDelMsg()} notitle="Отмена" yestitle="Удалить"
                             show={show} onHide={() => SetShow(false)} final={() => {
                   DeleteObjList(selectionIds).then(data => LoadData(id));
               }}/>
               <ModalOkMy show={showOk} onHide={() => SetOkShow(false)}
                          title='Внимание! Не выбрано ни одно значение.' okTitle='Ок'/>
           </div>
           :
           null

    )
}


export default GridObjComponent