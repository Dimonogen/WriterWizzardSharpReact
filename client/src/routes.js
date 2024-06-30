import {
    ACCOUNT_ROUTE,
    ADMIN_ROUTE, BASE_ROUTE, CONFIG_ROUTE,
    LOGIN_ROUTE, MENU_ROUTE,
    REGISTRATION_ROUTE,
    USER_ROUTE
} from "./utils/consts"
import Admin from "./pages/Admin"
import Auth from "./pages/Auth"
import ErrorPage from "./pages/ErrorPage";
import UserPage from "./pages/UserPage";
import AccountPage from "./pages/AccountPage";
import StartPage from "./pages/StartPage";
import MenuPage from "./pages/MenuPage";
import ConfigPage from "./pages/ConfigPage";

export const authRoutes = [
    {
        path: ADMIN_ROUTE,
        Component: Admin
    },
    {
        path: ACCOUNT_ROUTE,
        Component: AccountPage
    },
    {
        path: MENU_ROUTE,
        Component: MenuPage
    },
    {
        path: MENU_ROUTE + '/:id',
        Component: MenuPage
    },
    {
        path: MENU_ROUTE + '/:id/:objId',
        Component: MenuPage
    },
    {
        path: CONFIG_ROUTE,
        Component: ConfigPage
    },
    {
        path: CONFIG_ROUTE + '/:id',
        Component: ConfigPage
    },
    {
        path: CONFIG_ROUTE + '/:id/:typeId',
        Component: ConfigPage
    }
]

export const publicRoutes = [
    {
        path: LOGIN_ROUTE,
        Component: Auth
    },
    {
        path: REGISTRATION_ROUTE,
        Component: Auth
    },
    {
        path: USER_ROUTE + '/:id',
        Component: UserPage
    },
    {
        path: "/*",
        Component: ErrorPage
    }
]

export const noBarsRoutes = [
    {
        path: BASE_ROUTE,
        Component: StartPage
    }
]