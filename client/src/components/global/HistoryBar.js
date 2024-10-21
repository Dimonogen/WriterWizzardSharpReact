import {NavLink, useNavigate, useParams} from "react-router-dom";
import {useContext} from "react";
import {Context} from "../../index";
import {observer} from "mobx-react-lite";

const HistoryBar = observer(() => {
    const navigate = useNavigate()
    const path = window.location.pathname

    const {user} = useContext(Context)

    const params = useParams()

    const GetName = (p_str) => {
        let name = "";
        let pos = p_str.lastIndexOf('/')
        if(pos == -1)
            name = p_str;
        else
            name = p_str.slice(pos+1, p_str.length)

        let countSlesh = 0
        for (let i = 0; i < p_str.length; i++)
            if(p_str[i] == '/')
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

    const elements = getElements(path)
    //console.log(elements)

  return (
      <div className="ms-4 d-flex flex-row" style={{position:"relative", top:"90px"}}>
          {
              elements.map(e =>
                  e.id + 1 != elements.length ?
              <NavLink className="alter me-3 fs-5" key={e.id} to={e.path}  >
                  {e.name}
              </NavLink>
                :
              <span className="fs-5" key={e.id}>
                  {e.name}
              </span>
              )
          }
      </div>
  )
});

export default HistoryBar