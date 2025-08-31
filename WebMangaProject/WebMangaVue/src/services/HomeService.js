// src/services/HomeService.js
import api from "../plugins/api"; // axios configurado

export class HomeService {
  constructor(token = "") {
    this.token = token;
    this.headers = {
      Authorization: token ? `Bearer ${token}` : "",
    };
  }

  // GET: api/Home/HomePage?skip={skip}&take={take}
  async getHomePageData(skip = 0, take = 7) {
    try {
      const { data } = await api.get(`/Home/HomePage?skip=${skip}&take=${take}`, {
        headers: this.headers,
      });
      return data;
    } catch (error) {
      console.error("Erro ao buscar dados da HomePage:", error);
      return null;
    }
  }
}
