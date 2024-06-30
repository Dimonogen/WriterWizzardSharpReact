import {GetAllObjTypes} from "../../http/ObjTypeApi";
import {useEffect, useState} from "react";
import {Button} from "react-bootstrap";
import {useNavigate, useParams} from "react-router-dom";
import {MENU_ROUTE} from "../../utils/consts";
import {GetMenuForUser} from "../../http/MenuApi";

const MenuComponent = () => {

    const navigate = useNavigate();
    const {id} = useParams()

    const [types, SetTypes] = useState([]);

    useEffect(() => {
        GetMenuForUser().then(data => {SetTypes(data)});
        //GetAllObjTypes().then(data => {SetTypes(data);//console.log(data)
        //     });
    }, [])

    return (
        <div className='Block W-20'>
            <div className='d-flex justify-content-center'>
                <span>Меню</span>
            </div>

            {
                types.map(e =>
                    <div className='mt-2' key={e.id} >
                        <Button className='W-100 text-start' variant={id == e.objTypeId?'dark':'outline-dark'}
                                onClick={() => {navigate(MENU_ROUTE+'/'+e.objTypeId)}} >{e.name}
                        </Button>
                    </div>
                )
            }
        </div>
    )
}

export default MenuComponent