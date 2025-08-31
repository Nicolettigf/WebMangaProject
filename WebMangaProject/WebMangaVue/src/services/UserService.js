import axios from 'axios';

const API_BASE_URL = 'https://localhost:7164/api/User';

export const UserService = {
  async getById(id, token) {
    const headers = { Authorization: `Bearer ${token}` };
    const response = await axios.get(`${API_BASE_URL}/${id}`, { headers });
    return response.data;
  },

  async get(token, skip = 0, take = 25) {
    const headers = { Authorization: `Bearer ${token}` };
    const response = await axios.get(`${API_BASE_URL}/skip/${skip}/take/${take}`, { headers });
    return response.data;
  },

  async login(userLogin) {
    const response = await axios.post(`${API_BASE_URL}/Authenticate`, userLogin);
    return response.data;
  },

  async insert(user, token) {
    const headers = { Authorization: `Bearer ${token}` };
    const response = await axios.post(`${API_BASE_URL}`, user, { headers });
    return response.data;
  },

  async update(userProfileUpdate, token) {
    const headers = { Authorization: `Bearer ${token}` };
    const response = await axios.put(`${API_BASE_URL}/${userProfileUpdate.id}`, userProfileUpdate, { headers });
    return response.data;
  },

  async delete(id, token) {
    const headers = { Authorization: `Bearer ${token}` };
    const response = await axios.delete(`${API_BASE_URL}/${id}`, { headers });
    return response.data;
  }
};
