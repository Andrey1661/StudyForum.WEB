import axios from 'axios'

const instance = axios.create({
    baseURL: 'http://localhost:8000/api/',
    headers: {
        'Content-type': 'application/json'
    }
});

instance.defaults.headers.common["Authorization"] = localStorage.getItem('token') || '';

export default instance;