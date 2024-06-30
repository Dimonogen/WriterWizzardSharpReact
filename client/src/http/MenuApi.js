import {$authHost} from "./index";

export const GetMenuForUser = async () =>
{
    const {data} = await $authHost.get('menu');
    return data;
}