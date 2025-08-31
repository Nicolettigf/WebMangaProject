import axios from 'axios';

const API_BASE_URL = 'https://localhost:7164/api/AnimeComentary';

export const AnimeComentaryService = {
  async getById(id, token) {
    const headers = token ? { Authorization: `Bearer ${token}` } : {};
    const response = await axios.get(`${API_BASE_URL}/ById/${id}`, { headers });
    return response.data;
  },

  async get(skip = 0, take = 25, token) {
    const headers = token ? { Authorization: `Bearer ${token}` } : {};
    const response = await axios.get(`${API_BASE_URL}/skip/${skip}/take/${take}`, { headers });
    return response.data;
  },

  async getByAnime(animeId) {
    const response = await axios.get(`${API_BASE_URL}/ByAnime/${animeId}`);
    return response.data;
  },

  async getByUser(userId) {
    const response = await axios.get(`${API_BASE_URL}/ByUser/${userId}`);
    return response.data;
  },

  async insert(item, token) {
    const headers = { Authorization: `Bearer ${token}` };
    const response = await axios.post(`${API_BASE_URL}`, item, { headers });
    return response.data;
  },

  async update(item, token) {
    const headers = { Authorization: `Bearer ${token}` };
    const response = await axios.put(`${API_BASE_URL}/${item.id}`, item, { headers });
    return response.data;
  },

  async delete(id, token) {
    const headers = { Authorization: `Bearer ${token}` };
    const response = await axios.delete(`${API_BASE_URL}/${id}`, { headers });
    return response.data;
  }
};
