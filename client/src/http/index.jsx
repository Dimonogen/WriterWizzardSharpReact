import axios from 'axios'

const baseUrlApi = import.meta.env.VITE_APP_API_URL

const $host = axios.create({
    baseURL: baseUrlApi
})

const $authHost = axios.create({
    baseURL: baseUrlApi
})

const authInterceptor = config => {
    config.headers.authorization = `Bearer ${localStorage.getItem('token')}`
    return config
}

$authHost.interceptors.request.use(authInterceptor)

export {
    $authHost,
    $host
}