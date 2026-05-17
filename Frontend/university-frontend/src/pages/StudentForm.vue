<script setup lang="ts">
import { ref } from 'vue'
import axios from 'axios'
import { useRouter } from 'vue-router'
import studentService from '@/services/studentService'

const router = useRouter()
const name = ref('')
const email = ref('')
const majorId = ref<number | null>(null)
const loading = ref(false)
const error = ref('')
const success = ref('')

const getErrorMessage = (errorValue: unknown) => {
  if (axios.isAxiosError(errorValue)) {
    return errorValue.response?.data?.message || errorValue.message || 'Server request failed.'
  }
  if (errorValue instanceof Error) {
    return errorValue.message
  }
  return 'An unexpected error occurred.'
}

const submitStudent = async () => {
  error.value = ''
  success.value = ''

  if (!name.value.trim() || !email.value.trim() || majorId.value === null) {
    error.value = 'Please provide name, email, and major ID.'
    return
  }

  loading.value = true

  try {
    await studentService.createStudent({
      name: name.value.trim(),
      email: email.value.trim(),
      majorId: majorId.value
    })
    success.value = 'Student created successfully.'
    setTimeout(() => router.push('/students'), 700)
  } catch (err) {
    error.value = getErrorMessage(err)
  } finally {
    loading.value = false
  }
}

const cancel = () => {
  router.push('/students')
}
</script>

<template>
  <section class="form-shell">
    <div class="form-card">
      <div class="form-heading">
        <div>
          <h1>Add New Student</h1>
          <p class="subtitle">Create a student and save it to the registration system.</p>
        </div>
      </div>

      <div v-if="error" class="status-panel error">
        <p>{{ error }}</p>
      </div>
      <div v-if="success" class="status-panel success">
        <p>{{ success }}</p>
      </div>

      <form @submit.prevent="submitStudent">
        <label>
          Name
          <input v-model="name" type="text" placeholder="Student name" />
        </label>

        <label>
          Email
          <input v-model="email" type="email" placeholder="student@example.com" />
        </label>

        <label>
          Major ID
          <input v-model.number="majorId" type="number" min="1" placeholder="Enter department ID" />
        </label>

        <p class="note">Use a valid department ID for the student major. The API accepts major numeric IDs.</p>

        <div class="actions">
          <button class="button primary" type="submit" :disabled="loading">
            {{ loading ? 'Saving…' : 'Save Student' }}
          </button>
          <button class="button secondary" type="button" @click="cancel" :disabled="loading">
            Cancel
          </button>
        </div>
      </form>
    </div>
  </section>
</template>

<style scoped>
.form-shell {
  max-width: 640px;
  margin: 0 auto;
  padding-top: 0.5rem;
}

.form-card {
  background: #ffffff;
  border: 1px solid #e2e8f0;
  border-radius: 1.25rem;
  padding: 2rem;
  box-shadow: 0 18px 48px rgba(15, 23, 42, 0.08);
}

.form-heading h1 {
  margin: 0;
  font-size: clamp(2rem, 2.1vw, 2.4rem);
}

.subtitle {
  margin: 0.6rem 0 0;
  color: #475569;
}

label {
  display: block;
  margin-top: 1.4rem;
  color: #0f172a;
  font-weight: 600;
}

input {
  width: 100%;
  margin-top: 0.75rem;
  padding: 0.95rem 1rem;
  border-radius: 0.9rem;
  border: 1px solid #cbd5e1;
  background: #f8fafc;
  color: #0f172a;
  font-size: 1rem;
}

input:focus {
  outline: none;
  border-color: #0f172a;
  box-shadow: 0 0 0 4px rgba(15, 23, 42, 0.08);
}

.note {
  margin: 1rem 0 0;
  color: #475569;
  font-size: 0.96rem;
}

.status-panel {
  margin-bottom: 1rem;
  padding: 1rem 1.15rem;
  border-radius: 0.85rem;
}

.status-panel.error {
  background: #fff1f2;
  border: 1px solid #fecdd3;
  color: #9f1239;
}

.status-panel.success {
  background: #ecfdf5;
  border: 1px solid #6ee7b7;
  color: #166534;
}

.actions {
  display: flex;
  gap: 1rem;
  margin-top: 1.7rem;
}

.button {
  border: none;
  border-radius: 0.85rem;
  padding: 0.95rem 1.25rem;
  cursor: pointer;
  transition: transform 0.18s ease, opacity 0.18s ease;
}

.button:hover:not(:disabled) {
  transform: translateY(-1px);
}

.button.primary {
  background: #0f172a;
  color: #fff;
}

.button.secondary {
  background: #f8fafc;
  color: #0f172a;
  border: 1px solid #cbd5e1;
}

.button:disabled {
  opacity: 0.65;
  cursor: not-allowed;
}
</style>
