<template>
  <div class="user-list-container">
    <h1>Users</h1>
    <router-link to="/user/create" class="custom-link mb-3 d-inline-block">Create</router-link>

    <table class="table custom-table">
      <thead>
        <tr>
          <th>ID</th>
          <th>Nickname</th>
          <th>Email</th>
          <th>Created At</th>
          <th>Favorites Count</th>
          <th>Reviews Count</th>
          <th>Last Login</th>
          <th></th>
          <th></th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="user in users" :key="user.id">
          <td>{{ user.id }}</td>
          <td>{{ user.nickname }}</td>
          <td>{{ user.email }}</td>
          <td>{{ formatDate(user.createdAt) }}</td>
          <td>{{ user.favoritesCount }}</td>
          <td>{{ user.reviewsCount }}</td>
          <td>{{ formatDate(user.lastLogin) }}</td>
          <td>
            <router-link :to="`/user/edit/${user.id}`" class="custom-link">Edit</router-link>
          </td>
          <td>
            <button @click="deleteUser(user.id)" class="custom-link btn btn-link p-0">Delete</button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import axios from 'axios'
import { useRouter } from 'vue-router'

const router = useRouter()
const users = ref([])

// Carrega usuários
const fetchUsers = async () => {
  try {
    const res = await axios.get('/api/user')
    users.value = res.data
  } catch (err) {
    console.error(err)
  }
}

onMounted(() => {
  fetchUsers()
})

// Deleta usuário
const deleteUser = async (id) => {
  if (!confirm('Are you sure you want to delete this user?')) return
  try {
    await axios.delete(`/api/user/${id}`)
    users.value = users.value.filter(u => u.id !== id)
  } catch (err) {
    console.error(err)
  }
}

// Formata datas
const formatDate = (dateStr) => {
  const d = new Date(dateStr)
  return d.toLocaleString()
}
</script>

<style scoped>
.user-list-container {
  max-width: 1000px;
  margin: auto;
}
.custom-link {
  cursor: pointer;
}
</style>
