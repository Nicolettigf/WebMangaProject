// src/services/MangaService.js
import api from "../plugins/api"; // usa a instância do axios já configurada

export class MangaService {
  constructor(token = "") {
    this.token = token;
    this.headers = {
      Authorization: token ? `Bearer ${token}` : "",
    };
  }

  // dentro do MangaService
  async getByCatalog(catalogName, skip = 0, take = 25) {
    const { data } = await api.get(
      `/Manga/ByCatalog/skip/${skip}/take/${take}?catalogName=${catalogName}`,
      { headers: this.headers }
    );
    return data;
  }

  // GET: api/Manga/MangaOnPage/{id}
  async getComplete(id) {
    const { data } = await api.get(`/Manga/MangaOnPage/${id}`, {
      headers: this.headers,
    });
    return data;
  }

  // GET: api/Manga/skip/{skip}/take/{take}
  async getAll(skip = 0, take = 25) {
    const { data } = await api.get(`/Manga/skip/${skip}/take/${take}`, {
      headers: this.headers,
    });
    return data;
  }
  
  // GET: api/Manga/ByName/{title}
  async getByTitle(title) {
    const { data } = await api.get(`/Manga/ByName/${encodeURIComponent(title)}`, {
      headers: this.headers,
    });
    return data;
  }
  
  // GET: api/Manga/ById/{id}
  async getById(id) {
    const { data } = await api.get(`/Manga/ById/${id}`, {
      headers: this.headers,
    });
    return data;
  }
  
  // POST: api/Manga
  async create(manga) {
    const { data } = await api.post(`/Manga`, JSON.stringify(manga), {
      headers: { 
        ...this.headers,
        "Content-Type": "application/json",
      },
    });
    return data;
  }
  
  // PUT: api/Manga/{id}
  async update(id, manga) {
    const { data } = await api.put(`/Manga/${id}`, JSON.stringify(manga), {
      headers: { 
        ...this.headers,
        "Content-Type": "application/json",
      },
    });
    return data;
  }

  // DELETE: api/Manga/{id}
  async delete(id) {
    const { data } = await api.delete(`/Manga/${id}`, {
      headers: this.headers,
    });
    return data;
  }
  
  // GET: api/Manga/ByCategory/{categoryId}
  async getByCategory(categoryId) {
    const { data } = await api.get(`/Manga/ByCategory/${categoryId}`, {
      headers: this.headers,
    });
    return data;
  }
  
  
  // #region deletar

    // GET: api/Manga/ByFavorites/skip/{skip}/take/{take}
    async getByFavorites(skip = 0, take = 25) {
      const { data } = await api.get(`/Manga/ByFavorites/skip/${skip}/take/${take}`, {
        headers: this.headers,
      });
      return data;
    }
  
    // GET: api/Manga/ByRating/skip/{skip}/take/{take}
    async getByRating(skip = 0, take = 25) {
      const { data } = await api.get(`/Manga/ByRating/skip/${skip}/take/${take}`, {
        headers: this.headers,
      });
      return data;
    }
    
    // GET: api/Manga/ByUserCount/skip/{skip}/take/{take}
    async getByUserCount(skip = 0, take = 25) {
      const { data } = await api.get(`/Manga/ByUserCount/skip/${skip}/take/${take}`, {
        headers: this.headers,
      });
      return data;
    }
    
    // GET: api/Manga/ByPopularity/skip/{skip}/take/{take}
    async getByPopularity(skip = 0, take = 25) {
      const { data } = await api.get(`/Manga/ByPopularity/skip/${skip}/take/${take}`, {
        headers: this.headers,
      });
      return data;
    }
  // #endregion

}
