<template>
  <div class="user-create">
    <h1>Welcome</h1>
    <h4>Create an account here</h4>
    <hr />

    <form @submit.prevent="submitForm">
      <div v-if="errors.length" class="alert alert-warning">
        <p v-for="(error, index) in errors" :key="index">{{ error }}</p>
      </div>

      <div class="form-group">
        <label for="nickname">Nickname</label>
        <input id="nickname" v-model="form.nickname" class="form-control" />
      </div>

      <div class="form-group">
        <label for="email">Email</label>
        <input id="email" v-model="form.email" type="email" class="form-control" />
      </div>

      <div class="form-group">
        <label for="password">Password</label>
        <input id="password" v-model="form.password" type="password" class="form-control" />
      </div>

      <div class="form-group">
        <label for="confirmPassword">Confirm Password</label>
        <input id="confirmPassword" v-model="form.confirmPassword" type="password" class="form-control" />
      </div>

      <button type="submit" class="btn btn-primary">Create</button>
    </form>

    <router-link to="/user" class="mt-3 d-block">Back to List</router-link>
  </div>
</template>

<script setup>
import { reactive, ref } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()
const errors = ref([])

const form = reactive({
  nickname: '',
  email: '',
  password: '',
  confirmPassword: ''
})

const submitForm = () => {
  errors.value = []

  // Validação simples
  if (!form.nickname) errors.value.push('Enter nickname please')
  if (!form.email) errors.value.push('Enter email please')
  if (!form.password) errors.value.push('Enter password please')
  if (!form.confirmPassword) errors.value.push('Confirm your password please')
  if (form.password && form.confirmPassword && form.password !== form.confirmPassword)
    errors.value.push('Passwords do not match')

  if (errors.value.length === 0) {
    // Aqui você faria a chamada à API para criar o usuário
    console.log('Form submitted', form)
    // Exemplo de redirecionamento
    router.push('/user')
  }
}
</script>

<style scoped>
.user-create {
  max-width: 400px;
  margin: 2rem auto;
}
.alert {
  margin-bottom: 1rem;
}
</style>
