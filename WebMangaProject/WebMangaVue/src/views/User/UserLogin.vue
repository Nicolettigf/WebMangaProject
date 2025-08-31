<template>
  <div class="login-body login-space">
    <div class="login-container">
      <div class="login-forms">

        <!-- Login Form -->
        <div class="login-form login" v-if="!showSignup">
          <span class="login-title">Login</span>
          <form @submit.prevent="loginUser">
            <div class="login-input-field">
              <input v-model="login.EmailOrNickname" type="text" placeholder="Enter your email" required />
              <i class="uil uil-envelope icon"></i>
              <span class="text-danger">{{ loginErrors.EmailOrNickname }}</span>
            </div>

            <div class="login-input-field">
              <input v-model="login.Password" :type="showPassword ? 'text' : 'password'" placeholder="Enter your password" required />
              <i class="uil uil-lock icon"></i>
              <i @click="togglePassword" class="uil" :class="showPassword ? 'uil-eye' : 'uil-eye-slash'"></i>
              <span class="text-danger">{{ loginErrors.Password }}</span>
            </div>

            <div class="login-input-field login-button">
              <input type="submit" value="Login" />
            </div>
          </form>

          <div class="login-signup">
            <span class="login-text">
              Not a member?
              <a href="#" class="signup-link" @click.prevent="showSignup = true">Signup Now</a>
            </span>
          </div>
        </div>

        <!-- Registration Form -->
        <div class="login-form signup" v-if="showSignup">
          <span class="login-title">Registration</span>
          <form @submit.prevent="registerUser">
            <div class="login-input-field">
              <input v-model="register.Nickname" type="text" placeholder="Enter your nickname" required />
              <i class="uil uil-user icon"></i>
              <span class="text-danger">{{ registerErrors.Nickname }}</span>
            </div>

            <div class="login-input-field">
              <input v-model="register.Email" type="email" placeholder="Enter your email" required />
              <i class="uil uil-envelope icon"></i>
              <span class="text-danger">{{ registerErrors.Email }}</span>
            </div>

            <div class="login-input-field">
              <input v-model="register.Password" :type="showPassword ? 'text' : 'password'" placeholder="Create a password" required />
              <i class="uil uil-lock icon"></i>
              <i @click="togglePassword" class="uil" :class="showPassword ? 'uil-eye' : 'uil-eye-slash'"></i>
              <span class="text-danger">{{ registerErrors.Password }}</span>
            </div>

            <div class="login-input-field">
              <input v-model="register.ConfirmPassword" :type="showPassword ? 'text' : 'password'" placeholder="Confirm a password" required />
              <i class="uil uil-lock icon"></i>
              <span class="text-danger">{{ registerErrors.ConfirmPassword }}</span>
            </div>

            <div v-if="formError" class="form-group">
              <p class="bg-warning">{{ formError }}</p>
            </div>

            <div class="login-input-field login-button">
              <input type="submit" value="Create" />
            </div>
          </form>

          <div class="login-signup">
            <span class="login-text">
              Already a member?
              <a href="#" class="login-link" @click.prevent="showSignup = false">Login Now</a>
            </span>
          </div>
        </div>

      </div>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'

// Toggle login/signup e password
const showSignup = ref(false)
const showPassword = ref(false)

// Login form
const login = ref({
  EmailOrNickname: '',
  Password: ''
})
const loginErrors = ref({ EmailOrNickname: '', Password: '' })

// Registration form
const register = ref({
  Nickname: '',
  Email: '',
  Password: '',
  ConfirmPassword: ''
})
const registerErrors = ref({ Nickname: '', Email: '', Password: '', ConfirmPassword: '' })

// Mensagem geral de erro (simulando ViewBag.Errors)
const formError = ref('')

// Função toggle password
function togglePassword() {
  showPassword.value = !showPassword.value
}

// Simulação de login
function loginUser() {
  // Limpa erros
  loginErrors.value = { EmailOrNickname: '', Password: '' }
  formError.value = ''

  // Aqui você chamaria a API
  if (!login.value.EmailOrNickname || !login.value.Password) {
    loginErrors.value.EmailOrNickname = !login.value.EmailOrNickname ? 'Required' : ''
    loginErrors.value.Password = !login.value.Password ? 'Required' : ''
    return
  }

  console.log('Login submitted', login.value)
}

// Simulação de registro
function registerUser() {
  // Limpa erros
  registerErrors.value = { Nickname: '', Email: '', Password: '', ConfirmPassword: '' }
  formError.value = ''

  // Validação simples
  if (!register.value.Nickname || !register.value.Email || !register.value.Password || !register.value.ConfirmPassword) {
    if (!register.value.Nickname) registerErrors.value.Nickname = 'Required'
    if (!register.value.Email) registerErrors.value.Email = 'Required'
    if (!register.value.Password) registerErrors.value.Password = 'Required'
    if (!register.value.ConfirmPassword) registerErrors.value.ConfirmPassword = 'Required'
    return
  }

  if (register.value.Password !== register.value.ConfirmPassword) {
    formError.value = 'Passwords do not match'
    return
  }

  console.log('Register submitted', register.value)
}
</script>

<style scoped>
/* Copie seu CSS do Razor aqui */
.login-body { display:flex; justify-content:center; align-items:center; height:100vh; }
.login-container { width:400px; background:#fff; padding:2rem; border-radius:10px; box-shadow:0 0 10px rgba(0,0,0,0.1); }
.login-title { font-size:1.5rem; margin-bottom:1rem; display:block; text-align:center; }
.login-input-field { position:relative; margin-bottom:1rem; }
.login-input-field input { width:100%; padding:0.75rem; border-radius:5px; border:1px solid #ccc; }
.login-input-field .icon { position:absolute; right:10px; top:50%; transform:translateY(-50%); }
.login-input-field.login-button input { width:100%; background:#42b883; color:#fff; border:none; cursor:pointer; }
.login-signup { text-align:center; margin-top:1rem; }
.text-danger { color:red; font-size:0.85rem; }
.bg-warning { background:#f9d342; padding:0.5rem; border-radius:5px; }
</style>
