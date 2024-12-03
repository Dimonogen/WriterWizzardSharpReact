import {NavLink, useLocation, useNavigate, useParams} from "react-router-dom";
import {useContext, useEffect, useState} from "react";
import {Context} from "../../index";
import {observer} from "mobx-react-lite";
import {saveUserSettings} from "../../http/UserSettingsAPI";
import {getHistoryNames} from "../../http/HistoryAPI";

const HistoryBar = observer(() => {
    const navigate = useNavigate()
    const path = window.location.pathname

    const location = useLocation()

    const {user} = useContext(Context)

    const params = useParams()

    const GetName = (p_str) => {
        let name = "";
        let pos = p_str.lastIndexOf('/')
        if (pos == -1)
            name = p_str;
        else
            name = p_str.slice(pos + 1, p_str.length)

        let countSlesh = 0
        for (let i = 0; i < p_str.length; i++)
            if (p_str[i] == '/')
                countSlesh++;
        //console.log("params = ", params)
        //console.log("GetName name = "+name)
        let res = "";
        if (name == '')
            res = "Начальная страница";
        else if (name == 'menu')
            res = 'Меню';
        else if (name == 'config')
            res = 'Настройки';
        else if (name == 'treshcan')
            res = "Корзина";
        else if (name == 'search')
            res = "Страница поиска";
        else if (name == 'login')
            res = "Страница входа";
        else if (name == 'registration')
            res = "Страница регистрации";
        else if (name == 'home')
            res = "Домашняя страница";
        else if (('objId' in params || 'typeId' in params) && countSlesh == 3)
            res = user.path[1]
        else if ('id' in params && countSlesh == 2)
            res = user.path[0]
        else
            res = 'Неизвестный путь';

        return res;
    }

    const getElements = (p_str) => {
        let arr = []
        let position = p_str.indexOf('/')
        let count = 0
        while (position != -1)
        {
            arr.push({
                path: p_str.slice(0, position+1),
                id: count,
                name: GetName(p_str.slice(0, position)),
                position: position
            })
            count += 1
            position = p_str.indexOf('/', position+1)
        }
        if(arr.at(arr.length-1).position < p_str.length - 1)
            arr.push({
                path: p_str,
                id: count,
                name: GetName(p_str),
                position: p_str.length
            })

        return arr;
    }

    const [elements, SetElements] = useState(getElements(path))
    //console.log(elements)

    const funcE = () => {
        let arr = getElements(path);
        let lastE = arr[arr.length-1];
        //console.log(lastE, arr);
        SetElements(arr);

        let str = JSON.stringify({name: lastE.name, path: lastE.path});
        if(lastE.path != '/' && lastE.path != '/home' && user.isAuth)
            saveUserSettings("LastLocation",{settings: str}).then();
    }

    useEffect(() => {

        if(('id' in params) && (user.path[0] == undefined))
        {
            getHistoryNames(path).then(data => {
                if(data.length > 0)
                {
                    user.setPath(data[0], 0)
                    console.log('SetPath[0]')
                }
                if(data.length > 1)
                {
                    user.setPath(data[1], 1)
                    console.log('SetPath[1]')
                }
                //console.log(data);
            }).finally(() => funcE());
        }
        else
        {
            funcE();
        }


    }, [location])

  return (
      <div className="ms-4 d-flex flex-row" style={{position:"relative", top:"90px"}}>
          {
              elements.map(e =>
                  e.id + 1 != elements.length ?
              <NavLink className="alter me-3 fs-5" key={e.id} to={e.path}  >
                  {e.name}
              </NavLink>
                :
              <span className="fs-5 fw-bold" key={e.id}>
                  {"< "+e.name+" >"}
              </span>
              )
          }
      </div>
  )
});

export default HistoryBar