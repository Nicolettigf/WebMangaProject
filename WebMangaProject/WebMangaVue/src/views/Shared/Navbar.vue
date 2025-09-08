<template>
  <header>
    <nav class="custom-navbar">
      <router-link class="logo" to="/">
        <img src="/images/avatars/Logo.png" class="custom-logo" />
        <span class="title-logo">AniMa List</span>
      </router-link>

      <div class="search-input wrapper">
        <input 
          type="text" 
          placeholder="Search Manga..."
          v-model="query"
          @input="updateRoute"
        >
        <div class="icon" @click="updateRoute">
          <i class="uil uil-search search-icon"></i>
        </div>
      </div>
    </nav>
  </header>
</template>

<script>
import { ref, watch } from 'vue';
import { useRouter, useRoute } from 'vue-router';

export default {
  name: "Navbar",
  setup() {
    const router = useRouter();
    const route = useRoute();
    const query = ref(route.query.q || "");

    const updateRoute = () => {
      router.replace({ name: "SearchPage", query: { q: query.value } });
    };

    // 🔹 Atualiza input quando URL mudar
    watch(() => route.query.q, (newQ) => {
      query.value = newQ || "";
    });

    return { query, updateRoute };
  }
};
</script>

<style scoped>
@import "/css/site.css";

.custom-navbar {
    height: 80px;
    display: flex;
    align-items: center;
    background-color: #171522;
    box-shadow: 0 10px 20px -10px rgb(0 0 0 / 75%);
}

.custom-navbar ul {
    display: flex;
    list-style: none;
}

.navbar-ul {
    margin-bottom: 0px;
    padding-left: 0px;
}

.custom-logo {
    vertical-align: bottom;
    height: 40px;
    width: 35px;
}

.title-logo {
    padding: 8px;
    font-size: 25px;
    font-family: 'Oswald', sans-serif;
}


/*#region Search Bar */
.wrapper {
    z-index: 2;
    right: 1rem;
    position: absolute;
    max-width: 400px;
}

.wrapper .search-input {
    position: relative;
    background-color: #fff;
    width: 100%;
    color: #333;
    border-radius: 8px;
    box-shadow: 0px 1px 5px 3px rgba(0, 0, 0, 0.12);
}

.search-input input {
    height: 50px;
    outline: none;
    border: none;
    border-radius: 8px;
    padding: 0 60px 0 20px;
    font-size: 18px;
    box-shadow: 0px 1px 5px rgba(0, 0, 0, 0.1);
}

.search-input .icon {
    width: 55px;
    line-height: 45px;
    position: absolute;
    top: 0;
    right: 0;
    text-align: center;
    font-size: 20px;
    cursor: pointer;
}
/*#endregion */
</style>
