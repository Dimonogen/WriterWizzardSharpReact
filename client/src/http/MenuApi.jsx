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

export const SaveEditMenuElement = async (id, name) =>
{
    const {data} = await $authHost.post('menu/edit', {id, name});
    return data;
}

export const DeleteMenuElement = async (id) =>
{
    const {data} = await $authHost.delete('menu?id=' + id);
    return data;
}