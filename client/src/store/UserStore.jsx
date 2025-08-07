import {makeAutoObservable} from 'mobx'

const defaultIcon = 'iconNotFound.png'

export default class UserStore {
    constructor(){
        this._isAuth = false
        this._user = {}
        this._info = {}
        this._icon = defaultIcon
        this._rights = []
        this._path = []
        this._settings = {}
        makeAutoObservable(this)
    }

    setIsAuth(bool){
        this._isAuth = bool
    }

    setUser(user){
        this._user = user
    }

    setInfo(info){
        this._info = info
        if(info && info.icon)
            this.setIcon(info.icon)
    }

    setIcon(icon){
        if(icon == null || icon == '')
            this._icon = defaultIcon
        else
            this._icon = icon
    }

    setRights(arr){
        this._rights = arr
    }

    setPath(path, index){
        //console.log(path)
        this._path[index] = path
    }

    get path(){
        return this._path;
    }

    get isAuth(){
        return this._isAuth
    }

    get user(){
        return this._user
    }

    get info(){
        return this._info
    }

    get icon(){
        return this._icon
    }

    get rights(){
        return this._rights
    }

    setSettings(code, value)
    {
        this._settings[code] = value;
    }

    getSettings(code)
    {
        return this._settings[code];
    }

    logOut(){
        this.setUser(null);
        this.setInfo(null);
        this.setIcon(defaultIcon);
        this.setRights(null);
        this.setIsAuth(false);
        localStorage.removeItem('token')
    }
}