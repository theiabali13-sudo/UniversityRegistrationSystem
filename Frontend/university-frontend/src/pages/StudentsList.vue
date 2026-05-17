<script setup lang="ts">
import { ref, onMounted } from 'vue'
import axios from 'axios'
import studentService from '@/services/studentService'
import type { Student } from '@/types'

const students = ref<Student[]>([])
const loading = ref(false)
const error = ref('')
const deletingId = ref<number | null>(null)

const getErrorMessage = (errorValue: unknown) => {
  if (axios.isAxiosError(errorValue)) {
    return errorValue.response?.data?.message || errorValue.message || 'Server request failed.'
  }
  if (errorValue instanceof Error) {
    return errorValue.message
  }
  return 'An unexpected error occurred.'
}

const fetchStudents = async () => {
  error.value = ''
  loading.value = true
  try {
    students.value = await studentService.getStudents()
  } catch (err) {
    error.value = getErrorMessage(err)
  } finally {
    loading.value = false
  }
}

const deleteStudent = async (id: number) => {
  const student = students.value.find((item) => item.studentId === id)
  if (!student) {
    return
  }

  if (!confirm(`Delete ${student.name}? This cannot be undone.`)) {
    return
  }

  deletingId.value = id
  error.value = ''

  try {
    await studentService.deleteStudent(id)
    students.value = students.value.filter((item) => item.studentId !== id)
  } catch (err) {
    error.value = getErrorMessage(err)
  } finally {
    deletingId.value = null
  }
}

onMounted(fetchStudents)
</script>

<template>
  <section class="page-shell">
    <div class="page-header">
      <div>
        <h1>Students</h1>
        <p class="subtitle">Browse and manage student records from the API.</p>
      </div>
      <router-link class="button primary" to="/students/new">Add Student</router-link>
    </div>

    <div class="status-panel" v-if="error">
      <p>{{ error }}</p>
    </div>

    <div class="data-card">
      <div class="table-wrapper">
        <table>
          <thead>
            <tr>
              <th>ID</th>
              <th>Name</th>
              <th>Email</th>
              <th>Major</th>
              <th>Courses</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            <tr v-if="loading">
              <td colspan="6" class="loading-cell">Loading students...</td>
            </tr>
            <tr v-else-if="students.length === 0">
              <td colspan="6" class="empty-cell">No students available yet.</td>
            </tr>
            <tr v-else v-for="student in students" :key="student.studentId">
              <td>{{ student.studentId }}</td>
              <td>{{ student.name }}</td>
              <td>{{ student.email }}</td>
              <td>{{ student.majorId }}</td>
              <td>{{ student.courseCount }}</td>
              <td>
                <button
                  class="button danger"
                  :disabled="deletingId === student.studentId"
                  @click="deleteStudent(student.studentId)"
                >
                  {{ deletingId === student.studentId ? 'Deleting…' : 'Delete' }}
                </button>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </section>
</template>

<style scoped>
.page-shell {
  max-width: 1140px;
  margin: 0 auto;
  display: flex;
  flex-direction: column;
  gap: 1.5rem;
}

.page-header {
  display: flex;
  justify-content: space-between;
  align-items: flex-end;
  gap: 1rem;
}

.page-header h1 {
  margin: 0;
  font-size: clamp(2rem, 2.5vw, 2.5rem);
}

.subtitle {
  margin: 0.35rem 0 0;
  color: #475569;
}

.data-card {
  background: #ffffff;
  border: 1px solid #e2e8f0;
  border-radius: 1rem;
  box-shadow: 0 16px 40px rgba(15, 23, 42, 0.08);
  overflow: hidden;
}

.table-wrapper {
  overflow-x: auto;
}

table {
  width: 100%;
  border-collapse: collapse;
}

th,
td {
  text-align: left;
  padding: 1rem 1.2rem;
}

th {
  background: #f8fafc;
  color: #334155;
  font-weight: 700;
  border-bottom: 1px solid #e2e8f0;
}

td {
  border-bottom: 1px solid #f1f5f9;
  color: #0f172a;
}

tr:hover td {
  background: #f8fafc;
}

.loading-cell,
.empty-cell {
  padding: 2rem;
  text-align: center;
  color: #64748b;
}

.status-panel {
  padding: 1rem 1.25rem;
  border-radius: 0.85rem;
  border: 1px solid #f8d7da;
  background: #fff1f2;
  color: #9f1239;
}

.button {
  border: none;
  border-radius: 0.75rem;
  padding: 0.8rem 1.15rem;
  cursor: pointer;
  transition: transform 0.15s ease, box-shadow 0.15s ease;
}

.button:hover:not(:disabled) {
  transform: translateY(-1px);
}

.button.primary {
  background: #0f172a;
  color: #fff;
}

.button.danger {
  background: #dc2626;
  color: #fff;
}

.button:disabled {
  opacity: 0.65;
  cursor: not-allowed;
}
</style>
