<template>
  <div class="edit-user-container">
    <h3 class="page-title pt-config">Settings</h3>
    <hr />
    <form @submit.prevent="submitForm" enctype="multipart/form-data">
      <div class="form-group e-padding">
        <label for="nickname" class="control-label e-title">Nickname</label>
        <input
          id="nickname"
          v-model="form.nickname"
          type="text"
          class="form-control etb-nick"
          required
        />
      </div>

      <div class="form-group py-3">
        <label for="about" class="control-label e-title">About</label>
        <textarea
          id="about"
          v-model="form.about"
          class="form-control edit-textbox"
          rows="3"
        ></textarea>
      </div>

      <div class="row">
        <div class="edit-img-title">
          <h5>Icon image</h5>
          <input
            type="file"
            @change="onAvatarChange"
            accept="image/png"
            class="custom-file-input"
          />
          <img
            v-if="avatarPreview"
            :src="avatarPreview"
            class="img-thumbnail"
            width="230"
          />
        </div>

        <div class="edit-img-title">
          <h5>Cover Image</h5>
          <input
            type="file"
            @change="onCoverChange"
            accept="image/png"
            class="custom-file-input"
          />
          <img
            v-if="coverPreview"
            :src="coverPreview"
            class="img-thumbnail"
            width="230"
          />
        </div>
      </div>

      <div class="form-group mt-3">
        <button type="submit" class="custom-btn">Save</button>
        <router-link to="/user" class="custom-link ml-2">Back to List</router-link>
      </div>
    </form>
  </div>
</template>

<script setup>
import { reactive, ref, onMounted } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import axios from 'axios'

const route = useRoute()
const router = useRouter()

// Form data
const form = reactive({
  id: route.params.id,
  nickname: '',
  about: '',
  avatarFile: null,
  coverFile: null
})

// Image previews
const avatarPreview = ref(null)
const coverPreview = ref(null)

// Load existing user data
onMounted(async () => {
  try {
    const res = await axios.get(`/api/user/${form.id}`)
    form.nickname = res.data.nickname
    form.about = res.data.about
    avatarPreview.value = res.data.avatarUrl
    coverPreview.value = res.data.coverUrl
  } catch (err) {
    console.error(err)
  }
})

// Handle file changes
const onAvatarChange = (event) => {
  form.avatarFile = event.target.files[0]
  avatarPreview.value = URL.createObjectURL(form.avatarFile)
}

const onCoverChange = (event) => {
  form.coverFile = event.target.files[0]
  coverPreview.value = URL.createObjectURL(form.coverFile)
}

// Submit form
const submitForm = async () => {
  try {
    const formData = new FormData()
    formData.append('Nickname', form.nickname)
    formData.append('About', form.about)
    if (form.avatarFile) formData.append('AvatarImageFile', form.avatarFile)
    if (form.coverFile) formData.append('CoverImageFile', form.coverFile)

    await axios.put(`/api/user/${form.id}`, formData, {
      headers: { 'Content-Type': 'multipart/form-data' }
    })

    router.push('/user')
  } catch (err) {
    console.error(err)
  }
}
</script>

<style scoped>
.edit-user-container {
  max-width: 800px;
  margin: auto;
}

.edit-img-title {
  margin-right: 20px;
}

.img-thumbnail {
  margin-top: 10px;
}
</style>
