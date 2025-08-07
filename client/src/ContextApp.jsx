import UserStore from "./store/UserStore.jsx";
import App from "./App.jsx";
import React, {createContext} from "react";
import UserSettingsStore from "./store/UserSettingsStore.jsx";

export const Context = createContext(null)

const ContextApp = () => {
    return (
        <Context.Provider value={{
            user: new UserStore(),
            userSettings: new UserSettingsStore(),
        }}>
            <App />
        </Context.Provider>
    )
}

export default ContextApp