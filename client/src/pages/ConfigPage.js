import {useNavigate, useParams} from "react-router-dom";
import {Button} from "react-bootstrap";
import {CONFIG_ROUTE} from "../utils/consts";
import ConfigObjTypeComponent from "../components/config/ConfigObjTypeComponent";
import ConfigAttrTypeComponent from "../components/config/ConfigAttrTypeComponent";
import GridComp from "../components/elementary/GridComp";

const MenuPage = () => {

    const navigate = useNavigate()

    const list = [
        {id:1, name:'Типы объектов', loadF: () => {return {row:[], column:[]} } },
        {id:2, name:'Типы атрибутов', loadF: () => {return {row:[], column:[]} } },
        {id:3, name:'Пункты меню', loadF: () => {return {row: [], column: []} } },
        {id:4, name:'Состояния объектов', loadF: () => {return {row:[], column:[]} } },
        {id:5, name:'Переходы состояний', loadF: () => {return {row:[], column:[]} } },
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
                                        onClick={() => {navigate(CONFIG_ROUTE+'/'+e.id)}} >{e.name}
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