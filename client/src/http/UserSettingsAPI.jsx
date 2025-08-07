import { $authHost, $host } from "./index";

export const getUserSettings = async (code) => {
    const {data} = await $authHost.get('/userSettings?gridCode='+code)
    return data;
}

export const saveUserSettings = async (code, value) => {
    const {data} = await $authHost.put('/userSettings/save?gridCode='+code, value)
    return data;
}