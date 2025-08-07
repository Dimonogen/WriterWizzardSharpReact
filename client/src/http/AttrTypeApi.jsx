import {$authHost} from "./index";

export const GetAllAttrTypes = async () =>
{
    const {data} = await $authHost.get('attrType/all');
    return data;
}

export const GetOneAttrType = async (typeId) =>
{
    const {data} = await $authHost.get('attrType/'+typeId);
    return data;
}

export const UpdateAttrType = async (typeData) =>
{
    const {data} = await $authHost.post('attrType/edit', typeData);
    return data;
}

export const DeleteAttrType = async (typeId) =>
{
    const {data} = await $authHost.delete('attrType/'+typeId);
    return data;
}

export const CreateAttrType = async (typeData) =>
{
    const {data} = await $authHost.post('attrType/create', typeData);
    return data;
}