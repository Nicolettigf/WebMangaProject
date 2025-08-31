// src/plugins/api.js
import axios from "axios";

const api = axios.create({
  baseURL: "https://localhost:7164/api",
  timeout: 10000,
});

api.interceptors.request.use((config) => {
  const token = localStorage.getItem("token");
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

export default api;
