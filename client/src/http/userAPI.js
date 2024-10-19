import { $authHost, $host } from "./index";
import jwt_decode from 'jwt-decode'

export const registration = async (dataR) => {
    const {data} = await $host.post('/user/registration', dataR)
    localStorage.setItem('token', data.token)
    return jwt_decode(data.token)
}

export const login = async (email, password) => {
    try {
        const {data} = await $host.post('/user/login', {email, password})
        localStorage.setItem('token', data.token)
        return jwt_decode(data.token)
    }
    catch (e) {
        return "error";
    }
}

export const check = async () => {
    const {data} = await $authHost.get('/user/auth')
    localStorage.setItem('token', data.token)
    return jwt_decode(data.token)
}

export const getInfo = async (id) => {
    const {data} = await $host.get('/user/'+id+'/info')
    return data;
}

export const getRights = async () => {
    const {data} = await $authHost.get('/user/rights')
    return data;
}

export const getMyUserInfo = async () => {
    const {data} = await $authHost.get('/user/myInfo')
    return data;
}

export const EditUserLogo = async (icon) => {
    const {data} = await $authHost.post('/user/logo/update', icon)
    return data;
}

export const EditUserInfo = async (values) => {
    const {data} = await $authHost.post('/user/editMyInfo', values)
    return data;
}

export const DeleteUserLogo = async () => {
    const {data} = await $authHost.post('/user/logo/delete')
    return data;
}