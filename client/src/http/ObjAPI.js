import {$authHost} from "./index";

export const GetAllOneTypeObjects = async (id) =>
{
    const {data} = await $authHost.get('obj/type/'+id);
    return data;
}

export const GetObjectsTreshCan = async () =>
{
    const {data} = await $authHost.get('obj/treshcan');
    return data;
}

export const GetLinkedObj = async (id) =>
{
    const {data} = await $authHost.get('obj/link/'+id);
    return data;
}

export const CreateLinkObj = async (idParent, idChild) =>
{
    const {data} = await $authHost.post('obj/link/', {parent: idParent, child: idChild});
    return data;
}

export const GetOneObj = async (objId) => {
    const {data} = await $authHost.get('obj/'+objId);
    return data;
}

export const UpdateObj = async (obj) =>
{
    const {data} = await $authHost.post('obj/edit', obj);
    return data;
}

export const CreateObj = async (obj) =>
{
    const {data} = await $authHost.post('obj/create', obj);
    return data;
}

export const DeleteObj = async (objId) => {
    const {data} = await $authHost.delete('obj/'+objId);
    return data;
}

export const RestoreObj = async (objId) => {
    const {data} = await $authHost.get('obj/restore?id='+objId);
    return data;
}

export const RestoreObjList = async (objList) => {
    const {data} = await $authHost.post('obj/restoreList', objList);
    return data;
}

export const DeleteObjList = async (objIdList) => {
    const {data} = await $authHost.post('obj/deleteList', objIdList);
    return data;
}

export const GetAllObjStates = async () => {
    const {data} = await $authHost.get('obj/states');
    return data;
}