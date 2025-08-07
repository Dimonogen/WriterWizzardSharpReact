
export default class UserSettingsStore {
    constructor(){
        this._settings = {}
    }
    setSettings(code, value)
    {
        this._settings[code] = value;
    }

    getSettings(code)
    {
        return this._settings[code];
    }
}