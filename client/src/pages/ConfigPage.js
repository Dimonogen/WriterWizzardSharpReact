import {useNavigate, useParams} from "react-router-dom";
import {Button} from "react-bootstrap";
import {CONFIG_ROUTE} from "../utils/consts";
import ConfigObjTypeComponent from "../components/config/ConfigObjTypeComponent";
import ConfigAttrTypeComponent from "../components/config/ConfigAttrTypeComponent";
import GridComp from "../components/elementary/GridComp";
import {useContext} from "react";
import {Context} from "../index";
import {GetAllMenuElements} from "../http/MenuApi";
import {GetAllObjStates} from "../http/ObjAPI";

const MenuPage = () => {

    const navigate = useNavigate()
    const {user} = useContext(Context)

    const list = [
        {id:1, name:'Типы объектов', loadF: () => {return {row:[], column:[]} } },
        {id:2, name:'Типы атрибутов', loadF: () => {return {row:[], column:[]} } },
        {id:3, name:'Пункты меню', loadF: async () => {
                let arr = []
                let data = await GetAllMenuElements();
                data.forEach(e => {
                    arr.push({id: e.id, name: e.name, descr: e.description, type: e.objTypeId})
                });
                //console.log(arr)
                return {
                    row: arr, column: [
                        {field: 'id', headerName: 'Id', width: 50},
                        {field: 'name', headerName: 'Название', width: 150},
                        {field: 'type', headerName: 'Тип объекта', width: 150},
                        {field: 'descr', headerName: 'Описание', width: 230},
                    ]
                }
            }
        },
        {id:4, name:'Состояния объектов', loadF: async () => {
                let arr = []
                let data = await GetAllObjStates();
                data.forEach(e => {
                    arr.push({id: e.id, name: e.name, descr: e.description, code: e.code})
                });
                //console.log(arr)
                return {
                    row: arr, column: [
                        {field: 'id', headerName: 'Id', width: 50},
                        {field: 'name', headerName: 'Название', width: 150},
                        {field: 'code', headerName: 'Код', width: 100},
                        {field: 'descr', headerName: 'Описание', width: 300},
                    ]
                }
            }
        },
    ]

    const {id} = useParams()

    return(
        <div className='MContent d-flex'>
            <div className='W-100 d-flex'>
                <div className='Block W-20 me-3'>
                    <div className='d-flex justify-content-center'>
                        <span>Настройки</span>
                    </div>
                    {
                        list.map(e =>
                            <div className='mt-2' key={e.id} >
                                <Button className='W-100 text-start' variant={id == e.id?'dark':'outline-dark'}
                                        onClick={() => {
                                            navigate(CONFIG_ROUTE+'/'+e.id);
                                            user.setPath(e.name, 0);
                                        }} >{e.name}
                                </Button>
                            </div>
                        )
                    }
                </div>
                {
                    id == null ?
                    null
                    :
                    id == 1?
                    <ConfigObjTypeComponent />
                    :
                    id == 2?
                    <ConfigAttrTypeComponent />
                        :<GridComp LoadData={() => list[id-1].loadF()} />
                }
            </div>
        </div>
    )
}

export default MenuPage