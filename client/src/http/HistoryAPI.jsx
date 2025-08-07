import { $authHost, $host } from "./index";

export const getHistoryNames = async (path) => {
    const {data} = await $authHost.get('/history?path='+path)
    return data;
}
