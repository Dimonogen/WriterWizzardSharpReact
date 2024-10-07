import {GetAllObjTypes} from "../../http/ObjTypeApi";
import {useContext, useEffect, useState} from "react";
import {Button} from "react-bootstrap";
import {useNavigate, useParams} from "react-router-dom";
import {MENU_ROUTE} from "../../utils/consts";
import {GetMenuForUser} from "../../http/MenuApi";
import {Context} from "../../index";

const MenuComponent = () => {

    const navigate = useNavigate();
    const {id, objId} = useParams()

    const {user} = useContext(Context)

    const [menuElements, SetMenuElements] = useState([]);

    useEffect(() => {
        GetMenuForUser().then(data => {
            SetMenuElements(data);
            //console.log(data)
            if(id != undefined && objId == undefined)
                user.setPath( [data.filter(obj => {return obj.objTypeId.toString() == id})[0].name] )
        });
        //GetAllObjTypes().then(data => {SetTypes(data);//console.log(data)
        //     });
    }, [])

    return (
        <div className='Block W-20'>
            <div className='d-flex justify-content-center fs-4'>
                <span>Меню</span>
            </div>

            {
                menuElements.map(e =>
                    <div className='mt-2' key={e.id} >
                        <Button className='W-100 text-start' variant={id == e.objTypeId?'dark':'outline-dark'}
                                onClick={() => {
                                    user.setPath([e.name])
                                    navigate(MENU_ROUTE+'/'+e.objTypeId)}} >{e.name}
                        </Button>
                    </div>
                )
            }
        </div>
    )
}

export default MenuComponent