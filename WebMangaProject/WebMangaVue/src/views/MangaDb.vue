<template>
  <div class="manga-db">
    <h1>Manga</h1>

    <div class="actions">
      <button @click="goToCreate" class="custom-link">Create</button>
      <button @click="resetAndConsume" class="custom-link">Reset + Consume</button>
    </div>

    <table class="table custom-table">
      <thead>
        <tr>
          <th>ID</th>
          <th>Title</th>
          <th>Favorites Count</th>
          <th></th>
          <th></th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="item in mangas" :key="item.id">
          <td>{{ item.id }}</td>
          <td>{{ item.canonicalTitle }}</td>
          <td>{{ item.favoritesCount ?? 0 }}</td>
          <td>
            <button @click="editManga(item.id)" class="custom-link">Edit</button>
          </td>
          <td>
            <button @click="deleteManga(item.id)" class="custom-link">Delete</button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
export default {
  name: "MangaDb",
  data() {
    return {
      mangas: [], // lista carregada da API ou backend
    };
  },
  created() {
    this.loadMangas();
  },
  methods: {
    loadMangas() {
      // substituir por fetch/axios real
      // Exemplo: axios.get("/api/manga").then(res => this.mangas = res.data)
      console.log("Carregando mangas do backend...");
    },
    goToCreate() {
      this.$router.push({ name: "MangaDbCreate" });
    },
    resetAndConsume() {
      // ação de reset + consumir API
      console.log("Reset + Consume chamado");
    },
    editManga(id) {
      this.$router.push({ name: "MangaDbEdit", params: { id } });
    },
    deleteManga(id) {
      if (confirm("Deseja realmente deletar este manga?")) {
        // chamada DELETE real aqui
        console.log("Deletar manga", id);
      }
    },
  },
};
</script>

<style scoped>
.actions {
  margin-bottom: 1rem;
}
.custom-link {
  margin-right: 0.5rem;
  cursor: pointer;
  background: none;
  border: none;
  color: #007bff;
  text-decoration: underline;
}
.custom-link:hover {
  color: #0056b3;
}
.table {
  width: 100%;
  border-collapse: collapse;
}
.table th,
.table td {
  border: 1px solid #ddd;
  padding: 0.5rem;
}
</style>
