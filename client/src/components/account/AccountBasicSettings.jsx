import FieldText from "../elementary/FieldText";
import {Button} from "react-bootstrap";
import React, {useContext, useEffect, useState} from "react";
import {EditUserInfo, getInfo, getMyUserInfo} from "../../http/userAPI";
import {Context} from "../../ContextApp";


const AccountBasicSettings = () => {

    const {user} = useContext(Context)

    //Поля для редактирования инфы
    const [name, setName] = useState('')
    const [status, setStatus] = useState('')
    const [aboutSelf, setAboutSelf] = useState('')

    useEffect(() => {
        getMyUserInfo().then(data => {
            setName(data.name)
            setStatus(data.title || '')
            setAboutSelf(data.description || '')
            //console.log("load")
        })
    }, [])

    const saveInfo = () => {
        try{

            let values = {};
            values.name = name;
            values.description = aboutSelf;
            values.title = status;
            EditUserInfo(values).then(
                data1 => getInfo(user.user.id).then(data => user.setInfo(data))
            );
        }
        catch (e) {
            alert(e.message)
        }

    }

  return (
    <div>
        <h4>Мой профиль</h4>
        <hr/>
        <h5>Общие сведения</h5>
        <FieldText setValue={setName} value={name} name="Псевдоним" minlen={1} maxlen={255}
                   placeholder="Введите псевдоним для аккаунта" nullable={false} />
        <FieldText setValue={setStatus} value={status} name="Статус" minlen={0} maxlen={255}
                   placeholder="Заполните статус (надпись в профиле ниже имени)" nullable={true} />
        <FieldText setValue={setAboutSelf} value={aboutSelf} name="О себе" minlen={0} maxlen={255}
                   placeholder="Расскажите о себе" nullable={true} rows={4} />
        <Button className="mt-1 mb-4" variant='outline-success' onClick={() => {saveInfo()}}>Сохранить изменения</Button>
        <div className="mt-3"></div>


        <div style={{opacity:0.2}}>
            <h5>Вход</h5>
            <FieldText setValue={setName} value={name} name="Логин" minlen={0} maxlen={255}
                       placeholder="Заполните статус (надпись в профиле ниже имени)" nullable={true} />
            <FieldText setValue={setName} value={name} name="Пароль" minlen={0} maxlen={255}
                       placeholder="Расскажите о себе" nullable={true} />
        </div>
    </div>
  )
}

export default AccountBasicSettings