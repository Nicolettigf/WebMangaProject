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

  async GetHomeMedia(skip = 0, take = 7, mediaType = "anime") {
    try {
      const { data } = await api.get(
        `/Home/GetHomeMedia?skip=${skip}&take=${take}&mediaType=${mediaType}`,
        {
          headers: this.headers,
        }
      );
      return data;
    } catch (error) {
      console.error(`Erro ao buscar dados da HomePage para ${mediaType}:`, error);
      return null;
    }
  }

  // NOVO MÉTODO: Buscar por nome
  async GetByName(name) {
    if (!name || name.length < 3) return { Animes: [], Mangas: [] }; // evita chamadas desnecessárias
    try {
      const { data } = await api.get(`/Home/GetByName?name=${encodeURIComponent(name)}`, {
        headers: this.headers,
      });
      return data;
    } catch (error) {
      console.error(`Erro ao buscar dados por nome "${name}":`, error);
      return { Animes: [], Mangas: [] };
    }
  }
}
