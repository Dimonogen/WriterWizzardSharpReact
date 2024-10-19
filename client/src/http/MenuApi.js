import {$authHost} from "./index";

export const GetMenuForUser = async () =>
{
    const {data} = await $authHost.get('menu');
    return data;
}

export const GetAllMenuElements = async () =>
{
    const {data} = await $authHost.get('menu/all');
    return data;
}