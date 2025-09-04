// src/services/AnimeService.js
import api from "../plugins/api"; // axios configurado

export class AnimeService {
  constructor(token = "") {
    this.token = token;
    this.headers = token 
    ? { Authorization: `Bearer ${token}` } 
    : {};
  }
  
  // dentro do AnimeService
  async getByCatalog(catalogName, skip = 0, take = 25) {
    const { data } = await api.get(
      `/Anime/ByCatalog/skip/${skip}/take/${take}?catalogName=${catalogName}`,
      { headers: this.headers }
    );
    return data;
  }

  async getAnimeOnPage(id) {
    const { data } = await api.get(`/Anime/AnimeOnPage/${id}`, { headers: this.headers });
    return data;
  }

  async getById(id) {
    const { data } = await api.get(`/Anime/ById/${id}`, { headers: this.headers });
    return data;
  }

  async getByName(title) {
    const { data } = await api.get(`/Anime/ByName/${encodeURIComponent(title)}`, { headers: this.headers });
    return data;
  }

  async getAll(skip = 0, take = 25) {
    const { data } = await api.get(`/Anime/skip/${skip}/take/${take}`, { headers: this.headers });
    return data;
  }

  async create(anime) {
    const { data } = await api.post(`/Anime`, anime, { 
      headers: { 
        ...this.headers,
        "Content-Type": "application/json" 
      }
    });
    return data;
  }

  async update(anime) {
    const { data } = await api.put(`/Anime/${anime.id}`, anime, { 
      headers: { 
        ...this.headers,
        "Content-Type": "application/json" 
      }
    });
    return data;
  }

  async delete(id) {
    const { data } = await api.delete(`/Anime/${id}`, { headers: this.headers });
    return data;
  }

  
  async getByCategory(categoryId) {
    const { data } = await api.get(`/Anime/ByCategory/${categoryId}`, { headers: this.headers });
    return data;
  }
  
  // #region deletar
    async getByFavorites(skip = 0, take = 25) {
      const { data } = await api.get(`/Anime/ByFavorites/skip/${skip}/take/${take}`, { headers: this.headers });
      return data;
    }

    async getByUserCount(skip = 0, take = 25) {
      const { data } = await api.get(`/Anime/ByUserCount/skip/${skip}/take/${take}`, { headers: this.headers });
      return data;
    }

    async getByRating(skip = 0, take = 25) {
      const { data } = await api.get(`/Anime/ByRating/skip/${skip}/take/${take}`, { headers: this.headers });
      return data;
    }

    async getByPopularity(skip = 0, take = 25) {
      const { data } = await api.get(`/Anime/ByPopularity/skip/${skip}/take/${take}`, { headers: this.headers });
      return data;
    }

  //#endregion
  

}
