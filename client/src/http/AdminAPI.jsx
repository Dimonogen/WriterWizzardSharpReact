import { $authHost, $host } from "./index";


export const AdmGetAllUsers = async () => {
    const {data} = await $authHost.post('/admin/getAllUsers')
    return data;
}

export const AdmGetAllBooks = async () => {
    const {data} = await $authHost.post('/admin/getAllBooks')
    return data;
}

export const AdmDeleteUser = async (id) => {
    const {data} = await $authHost.post('/admin/deleteUser/'+id )
    return data;
}

export const AdmSetPassword = async (id, password) => {
    const {data} = await $authHost.post('/admin/setPassword/'+id, password)
    return data;
}