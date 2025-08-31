<template>
    <header>
        <nav class="custom-navbar">
          <router-link class="logo" to="/">
            <img src="/images/avatars/Logo.png" class="custom-logo" />
            <span class="title-logo">AniMa List</span>
          </router-link>
          
          <ul class="navbar-ul">
            <li><router-link to="/profile">Hi User!</router-link></li>
            <li><router-link to="/logout">Logout</router-link></li>
          </ul>
        </nav>
      </header>
</template>

<script>
export default {
  name: "Navbar",
  props: {
    username: {
      type: String,
      default: "User"
    }
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

</style>
