<template>
  <div>
    <div class="page-title pt-config">
      <h3>{{ title }}</h3>
      <p class="va-config">Meet our team</p>
    </div>

    <hr />

    <div>
      <div class="center">
        <div class="columns" v-for="(member, index) in team" :key="index">
          <img class="icon-ed" :src="member.avatar" />
          <h5>{{ member.name }}</h5>
          <a :href="member.linkedin" target="_blank" class="custom-link">
            <i class="uil uil-linkedin"></i>
            <span class="text-ed">LinkedIn</span>
          </a>
          <a :href="member.github" target="_blank" class="custom-link">
            <i class="uil uil-github"></i>
            <span class="text-ed">GitHub</span>
          </a>
        </div>
      </div>

      <div>
        <hr />
        <h3 id="OurCodeId">Our Code</h3>
      </div>

      <div class="columns">
        <p id="centerId">
          <a :href="codeLink" target="_blank" id="linkId">Click here</a> to access our code
        </p>
        <a :href="codeLink" target="_blank">
          <img class="d-flex" id="githubId" src="/images/avatars/GitHub_Logo_White.png" />

        </a>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "AboutUs",
  data() {
    return {
      title: "About Us",
      codeLink: "https://github.com/eduardoPauloPanstein/MangaProject",
      team: [
        {
          name: "Paulo Eduardo",
          avatar: "../../public/images/avatars/thorfinn.jpg",
          linkedin: "https://www.linkedin.com/in/paulo-eduardo-franzen-panstein-8a9094249/",
          github: "https://github.com/eduardoPauloPanstein"
        },
        {
          name: "Gabriel Felipe",
          avatar: "../../public/images/avatars/asta.jpg",
          linkedin: "https://www.linkedin.com/in/gabriel-felipe-nicoletti-102706191/",
          github: "https://github.com/Nicolettigf"
        },
        {
          name: "Lucas William",
          avatar: "../../public/images/avatars/joyboy.jpg",
          linkedin: "https://www.linkedin.com/in/lucas-william-peschke-b326ba248/",
          github: "https://github.com/Lucas-Peschke"
        },
        {
          name: "Júlia",
          avatar: "../../public/images/avatars/yato.jpg",
          linkedin: "https://www.linkedin.com/in/j%C3%BAlia-nogueira-de-souza-315926247/",
          github: "https://github.com/Julia-Nogueira"
        }
      ]
    };
  }
};
</script>

<style scoped>
@import "/css/site.css";

.pt-config {
    margin-top: 2%;
}

.center{
    display: flex;
    justify-content: center;
}

.columns {
    display: flex;
    justify-content: center;
    padding: 18px;
    column-count: 3;
    column-gap: 40px;
    align-items: center;
    justify-content: center;
    display: flex;
    flex-direction: column;
}

</style>
