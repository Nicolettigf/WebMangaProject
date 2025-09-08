<template>
  <div class="app-layout">
    <!-- Sidebar -->
    <aside :class="['sidebar', { active: sidebarActive }]">
      <div class="toggle" @click="toggleSidebar"></div>
      <ul>

        <!-- Home -->
        <li>
          <router-link to="/">
            <i class="uil uil-estate"></i>
            <span class="title">Home</span>
          </router-link>
        </li>

        <!-- Manga Menu -->
        <li>
          <a href="#" class="manga-btn" @click.prevent="toggleMenu('manga')">
            <i class="uil uil-book-open"></i>
            <span class="title">Manga</span>
            <i class="uil uil-caret-right sub-menu" :class="{ rotate: showManga }"></i>
          </a>
          <ul class="item-show" :class="{ active: showManga }">
            <!-- <li><router-link to="/manga/db">Database</router-link></li> -->
            <li><router-link to="/manga/Home">Manga Home</router-link></li>
            <li><router-link to="/manga/favorites">Favorites</router-link></li>
            <li><router-link to="/manga/popularity">Popular</router-link></li>
            <li><router-link to="/manga/score">Score</router-link></li>
          </ul>
        </li>

        <!-- Anime Menu -->
        <li>
          <a href="#" class="anime-btn" @click.prevent="toggleMenu('anime')">
            <i class="uil uil-play-circle"></i>
            <span class="title">Anime</span>
            <i class="uil uil-caret-right  sub-menu" :class="{ rotate: showAnime }"></i>
          </a>
          <ul class="item-show" :class="{ active: showAnime }">
            <li><router-link to="/anime/Home">Anime Home</router-link></li>
            <li><router-link to="/anime/favorites">Favorites</router-link></li>
            <li><router-link to="/anime/popularity">Popular</router-link></li>
            <li><router-link to="/anime/score">Score</router-link></li>
          </ul>
        </li>

        <!-- About -->
        <li>
          <router-link to="/about">
            <i class="uil uil-users-alt"></i>
            <span class="title">About Us</span>
          </router-link>
        </li>
      </ul>
    </aside>

    <!-- Main Content -->
    <div class="home_content">
      <Navbar :username="'currentUser'" />

      <!-- Conteúdo da página -->
      <main>
        <router-view></router-view>
      </main>
    </div>
  </div>
</template>

<script>
import Navbar from "./Navbar.vue";

export default {
  name: "AppLayout",
  components: { Navbar },
  data() {
    return {
      sidebarActive: false,
      showManga: false,
      showAnime: false,
    };
  },
  methods: {
    toggleSidebar() {
      this.sidebarActive = !this.sidebarActive;

      // se fechar a sidebar, fecha submenus também
      if (!this.sidebarActive) {
        this.showManga = false;
        this.showAnime = false;
      }
    },
    toggleMenu(menu) {
      // Se a sidebar estiver fechada, abre primeiro
      if (!this.sidebarActive) {
        this.sidebarActive = true;
      }

      // Agora abre o submenu correspondente
      if (menu === "manga") {
        this.showManga = !this.showManga;
        // fecha o outro submenu
        this.showAnime = false;
      } else if (menu === "anime") {
        this.showAnime = !this.showAnime;
        this.showManga = false;
      }
    }
  },
};
</script>

<style scoped>
@import "/css/site.css";

/* #region SideBar */

.sidebar {
  position: fixed;
  top: 0px;
  left: 0px;
  background: #171522;
  width: 78px;
  height: 100%;
  /* inset: 20px; */
  /* border-radius: 50px; */
  padding: 6px 14px;
  overflow: hidden;
  transition: all 0.5s ease;
  /*background: linear-gradient(270deg, #4346A6,#3D4095);*/
}

.sidebar.active {
  width: 240px;
}


.toggle {
  position: absolute;
  top: 15px;
  right: 15px;
  width: 50px;
  height: 50px;
  background: #fff;
  border-radius: 50%;
  box-shadow: 5px 5px 10px rgba(0, 0, 0, 0.15);
  cursor: pointer;
  display: flex;
  justify-content: center;
  align-items: center;
}

.toggle::before {
  content: '';
  position: absolute;
  width: 26px;
  height: 3px;
  border-radius: 3px;
  background: #171522;
  transform: translateY(-5px);
  transition: 1s;
}

.toggle::after {
  content: '';
  position: absolute;
  width: 26px;
  height: 3px;
  border-radius: 3px;
  background: #171522;
  transform: translateY(5px);
  transition: 1s;
}

.sidebar.active .toggle::before {
  transform: translateY(0px) rotate(-405deg);
}

.sidebar.active .toggle::after {
  transform: translateY(0px) rotate(405deg);
}

.sidebar ul {
  margin-top: 75px;
  padding: 0px;
}

.sidebar ul li {
  position: relative;
  /*height: 50px;*/
  width: 100%;
  list-style: none;
  line-height: 50px;
}

.sidebar ul li a {
  color: #fff;
  position: relative;
  width: 100%;
  display: flex;
  text-decoration: none;
  align-items: center;
  transition: all 0.4s ease;
  border-radius: 12px;
  white-space: nowrap;
}

.sidebar ul li a:hover {
  color: #222;
  background: #fff;
}

.sidebar ul li a i {
  height: 50px;
  min-width: 50px;
  border-radius: 12px;
  line-height: 50px;
  text-align: center;
  font-size: 1.75em;
}

.sidebar ul li a .sub-menu {
  position: absolute;
  right: 0px;
  line-height: 50px;
  text-align: center;
  font-size: 1em;
  opacity: 0;
  transform: rotate(90deg);
  transition: all 0.4s ease;
}

.sidebar ul li a .sub-menu.rotate {
  transform: rotate(270deg);  
}

.sidebar.active ul li a .sub-menu {
    opacity: 1;
    transition: all 0.4s ease;
    pointer-events: auto;
}

.sidebar ul ul {
    margin-top: 0px;
    position: static;
    transition: all 0.4s ease;
}

.sidebar ul ul li {
  line-height: 42px;
  transition: all 0.4s ease;
  border-bottom: none;
}

.sidebar ul ul li a {
  font-size: 15px;
  width: 130px;
  margin-left: 60px;
  transition: all 0.4s ease;
}

.sidebar ul ul li a .sub-title {
    padding-left: 15px;
}

.sidebar ul .item-show {
    display: none;
    opacity: 0;
    transition: all 0.4s ease;
    pointer-events: none;
}

.sidebar ul .item-show.active {
    display: flex;
    flex-direction: column;
    opacity: 1;
    transition: all 0.4s ease;
    pointer-events: auto;
}


.sidebar .title {
    font-size: 20px;
    opacity: 0;
    transition: all 0.4s ease;
    pointer-events: none;
}

.sidebar.active .title {
    opacity: 1;
    transition: all 0.4s ease;
    pointer-events: auto;
}

/* #endregion */

.app-layout {
  display: flex;
  min-height: 120vh;
}

.home_content {
    position: absolute;
    height: 100%;
    width: calc(100% - 78px);
    left: 78px;
    transition: all 0.5s ease;
    flex: 1; /* ocupa o restante do espaço ao lado da sidebar */
}

.sidebar.active ~ .home_content {
    width: calc(100% - 240px);
    left: 240px;
}

</style>
