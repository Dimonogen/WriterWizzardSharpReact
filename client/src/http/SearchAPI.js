import { $authHost, $host } from "./index";

export const getSearchObjects = async (text) => {
    const {data} = await $authHost.get('/search?text='+text)
    return data;
}
