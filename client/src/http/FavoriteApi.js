import {$authHost} from "./index";

export const GetAllFavorite = async () =>
{
    const {data} = await $authHost.get('favorite/all');
    return data;
}