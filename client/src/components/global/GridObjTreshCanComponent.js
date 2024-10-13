import {useNavigate, useParams} from "react-router-dom";
import {DataGrid, useGridApiRef} from "@mui/x-data-grid";
import {DeleteObj, DeleteObjList, GetAllOneTypeObjects, GetObjectsTreshCan} from "../../http/ObjAPI";
import {GetOneType} from "../../http/ObjTypeApi"
import React, {useContext, useEffect, useState} from "react";
import Box from '@mui/material/Box';
import {Button, Image} from "react-bootstrap";
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

const GridObjTreshCanComponent = () => {

    const navigate = useNavigate();
    const {id} = useParams()
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

            LoadData(id);

    }, [id])

    const [objData, SetObjData] = useState([]);
    const [typeData, SetTypeData] = useState({});
    const [rows, SetRows] = useState([]);
    const [columns, SetColumns] = useState([
        { field: 'col1', headerName: 'Id', width: 50 },
        { field: 'col2', headerName: 'Название', width: 150 },
    ]);

    const [selectionIds, SetSelectionIds] = useState([]);

    const LoadData = () => {
        SetIsLoading(true);
        SetVisible(true);
        GetObjectsTreshCan().then(data => {SetObjData(data);

            let arr = [];
            data.forEach(e => {
                let elem = {id: e.id, state:e.state, name: e.name};
                //console.log(elem);
                arr.push(elem);
            });
            SetRows(arr);
        });

        //console.log(data);
        let arr = [{field: 'id', headerName: 'Id', width: 50},
            {field: 'state', headerName: 'Статус', width: 120},
            {field: 'name', headerName: 'Название', width: 150}
        ];

        SetColumns(arr);
        getUserSettings("GridTreshCan").then(dataI => {
            if (dataI != "")
                apiRef.current.restoreState(dataI);
            SetIsLoading(false);
        })
        //console.log(rights)
        //console.log(rights.includes(typeData.code + '.' + 'Edit'))

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

    return (

       visible ?
           <div className='W-100'>
               <div className={'d-flex flex-row ' + (IsLoading ? "d-none" : "d-block")}>
                   <div>
                   <Image className='m-2 ms-1 me-0' height='32px' width='32px' src={iconCreate}/>
                   <Button variant='dark' className='m-2'
                   onClick={()=>{navigate(MENU_ROUTE+'/'+id+'/0')}}>{"Новый "+typeData.name}</Button>
                   </div>
                   <Image className='m-2 ms-1 me-0' height='32px' width='32px' src={iconView}/>
                   <Button variant='outline-dark' className='m-2'
                           onClick={()=>{
                               if(selectionIds.length > 0)
                                   navigate(MENU_ROUTE+'/'+id+'/'+selectionIds[0])
                           }}
                   >{"Открыть "+typeData.name}</Button>
                   <Image className='m-2 ms-1 me-0' height='32px' width='32px' src={iconUpdate}/>
                   <Button variant='outline-dark' className='m-2'
                           onClick={()=>{LoadData(id)}}>Обновить грид</Button>

                   <div>
                       <Image className='m-2 ms-1 me-0' height='32px' width='32px' src={iconDelete}/>
                       <Button variant='outline-dark' className='m-2' onClick={() => {
                           if(selectionIds.length > 0)
                           {
                               SetShow(true);
                           }
                           else
                           {
                               SetOkShow(true);
                           }
                       }}>{"Удалить "+typeData.name}</Button>
                   </div>

               </div>
               <ThemeProvider theme={GridTheme}>
           <Box className={(IsLoading ? "d-none" : "d-block")} sx={{height: window.innerHeight-215+'px', width: '100%'}}>
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


export default GridObjTreshCanComponent