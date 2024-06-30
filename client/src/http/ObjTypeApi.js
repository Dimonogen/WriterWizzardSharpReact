import {$authHost} from "./index";

export const GetAllObjTypes = async () =>
{
    const {data} = await $authHost.get('objType/all');
    return data;
}

export const GetOneType = async (typeId) =>
{
    const {data} = await $authHost.get('objType/'+typeId);
    return data;
}

export const UpdateObjType = async (typeData) =>
{
    const {data} = await $authHost.post('objType/edit', typeData);
    return data;
}

export const DeleteType = async (typeId) =>
{
    const {data} = await $authHost.delete('objType/'+typeId);
    return data;
}

export const CreateType = async (typeData) =>
{
    const {data} = await $authHost.post('objType/create', typeData);
    return data;
}