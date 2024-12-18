import {useNavigate, useParams} from "react-router-dom";
import {DataGrid, useGridApiRef} from "@mui/x-data-grid";
import {DeleteObj, DeleteObjList, GetAllOneTypeObjects} from "../../http/ObjAPI";
import {GetOneType} from "../../http/ObjTypeApi"
import React, {useContext, useEffect, useState} from "react";
import Box from '@mui/material/Box';
import {Button, Form, Image, OverlayTrigger, Tooltip} from "react-bootstrap";
import {MENU_ROUTE} from "../../utils/consts";
import ModalYesNoMy from "../modals/ModalYesNoMy";
import ModalOkMy from "../modals/ModalOkMy";
import iconCreate_B from '../../assets/Create_B.svg'
import iconCreate_W from '../../assets/Create_W.svg'
import iconSeacrch_B from '../../assets/Search_B.svg'
import iconSeacrch_W from '../../assets/Search_W.svg'
import iconOpen_B from '../../assets/Open_B.svg'
import iconOpen_W from '../../assets/Open_W.svg'
import iconReload_B from '../../assets/Reload_B.svg'
import iconReload_W from '../../assets/Reload_W.svg'
import iconDelete_W from '../../assets/Delete_W.svg'
import iconDelete_B from '../../assets/Delete_B.svg'
import {Context} from "../../index";
import {getUserSettings, saveUserSettings} from "../../http/UserSettingsAPI";
import LoadingAnimComponent from "./LoadingAnimComponent";
import GridTheme from "../../CSS/GridTheme";
import {Input, ThemeProvider} from "@mui/material";
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

    //Поиск и связанное с ним
    const [showSearch, SetShowSearch] = useState(false)
    const [searchText, SetSearchText] = useState("")
    const [filtretedRows, SetFiltretedRows] = useState([])

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
                e.attributes.forEach(a => elem["c"+a.number] = a.value);
                e.extAttributes.forEach(a => elem["ea"+a.number] = a.value);
                //console.log(elem);
                arr.push(elem);
            });
            SetRows(arr);
            if(showSearch)
            {
                SearchFun(searchText);
            }
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

    const SearchFun = (text) => {
        let arr = rows;
        arr = arr.filter((e) => {
            let find = false;
            for (const elem in e)
            {
                //console.log(elem, e[elem])
                if (typeof e[elem] == 'string' && e[elem].includes(text) && elem != 'state')
                {
                    find = true;
                    break;
                }
            }
            return find;});

        SetFiltretedRows(arr);
    }


    let ActionList = [
        {id:1, iconB: iconCreate_B, iconW: iconCreate_W, name: "Новый "+typeData.name, action: () => {
                navigate(MENU_ROUTE+'/'+id+'/0')
            } },
        /*{id:2, iconB: iconOpen_B, iconW: iconOpen_W, name: "Открыть "+typeData.name, action: () => {
                if(selectionIds.length > 0) {
                    user.setPath(objData.find((x) => x.id == selectionIds[0]).name, 1);
                    navigate(MENU_ROUTE + '/' + id + '/' + selectionIds[0])
                }
            } },*/
        {id:3, iconB: iconReload_B, iconW: iconReload_W, name: "Обновить "+typeData.name, action: () => {
                LoadData(id)
            } },
        {id:4, iconB: iconDelete_B, iconW: iconDelete_W, name: "Удалить "+typeData.name, action: () => {
                if(selectionIds.length > 0)
                {
                    SetShow(true);
                }
                else
                {
                    SetOkShow(true);
                }
            } },
        {
            id:5, name: "Поиск", iconB: (showSearch ? iconSeacrch_W: iconSeacrch_B), iconW: iconSeacrch_W, action: () => {
                SearchFun(searchText);
                SetShowSearch(!showSearch);
            },
            variant: showSearch? 'dark' : 'outline-dark'
        }
    ]


    return (

       visible ?
           <div className='W-100'>
               <div style={{overflowX: "auto", overflowY: "clip"}} className={'d-flex  mb-3 ' + (IsLoading ? "d-none" : "d-block")}>

                   {
                       ActionList.map(e =>
                       <OverlayTrigger key={e.id} overlay={<Tooltip className="fs-6">{e.name}</Tooltip>} placement="top">
                           <Button onClick={e.action} className='p-2 ms-1 me-1'
                                   variant={e.variant ? e.variant : 'outline-dark'}>
                               <Image className='Black' height='28px' width='28px' src={e.iconB}/>
                               <Image className='White' height='28px' width='28px' src={e.iconW}/>
                           </Button>
                       </OverlayTrigger>
                       )
                   }
                   {
                       showSearch ?
                           <div className='d-flex'>
                           <Form className='ms-2 mt-1 mb-1 fs-2' onSubmit={event => {event.preventDefault();
                                SearchFun(searchText);
                           }}>
                               <Form.Control value={searchText} onChange={(e) => {SetSearchText(e.target.value)}}
                                   style={{color: 'var(--color-dark)', background: '0', border: '0', borderBottom: '1px solid #ccc'}}
                                             placeholder={"Поиск"}
                               />

                           </Form>
                               <Button className='ms-2' variant='outline-dark' onClick={() => SearchFun(searchText)}>Поиск</Button>
                               <div className='d-flex ms-2'>
                                   <label className='mt-auto mb-auto'>Найдено {filtretedRows.length} из {rows.length} значений</label>
                               </div>

                           </div>
                           :
                           null
                   }
               </div>
               <ThemeProvider theme={GridTheme}>
           <Box className={(IsLoading ? "d-none" : "d-block")} sx={{height: Math.max(window.innerHeight-215, 500)+'px', width: '100%'}}>
               <DataGrid rows={showSearch? filtretedRows : rows} columns={columns} checkboxSelection
                         prop
                         onRowSelectionModelChange={(ids) =>{SetSelectionIds(ids)}}
                         getRowClassName={(params) =>
                             params.indexRelativeToCurrentPage % 2 === 0 ? 'even' : 'odd'
                         }
                         onRowDoubleClick={(params) => {
                             //console.log(objData.find((x) => x.id == params.id));
                             user.setPath(objData.find((x) => x.id == params.id).name, 1);
                             navigate(MENU_ROUTE + '/' + id + '/' + params.id)} }
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