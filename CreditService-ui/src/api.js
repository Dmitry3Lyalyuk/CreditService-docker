import axios from 'axios';

const apiClient = axios.create({
  // Если мы настроили прокси, то базовый URL будет начинаться с /api
  baseURL: '/api',
  headers: {
    'Content-Type': 'application/json'
  }
});

export default apiClient;
