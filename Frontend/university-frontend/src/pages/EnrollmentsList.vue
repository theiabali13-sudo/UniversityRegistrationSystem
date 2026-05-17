<script setup lang="ts">
import { onMounted, ref } from 'vue'
import { useRouter } from 'vue-router'
import api from '@/services/api'
import type { EnrollmentResponse } from '@/types'

const router = useRouter()
const enrollments = ref<EnrollmentResponse[]>([])
const loading = ref(false)
const error = ref('')

const fetchEnrollments = async () => {
  loading.value = true
  error.value = ''

  try {
    const response = await api.get<EnrollmentResponse[]>('/enrollment')
    enrollments.value = response.data
  } catch (err) {
    error.value = 'Unable to load enrollments.'
  } finally {
    loading.value = false
  }
}

const deleteEnrollment = async (id: number) => {
  const confirmed = window.confirm('Delete this enrollment?')
  if (!confirmed) return

  try {
    await api.delete(`/enrollment/${id}`)
    await fetchEnrollments()
  } catch (err) {
    error.value = 'Unable to delete enrollment.'
  }
}

const editEnrollment = (id: number) => {
  router.push({ name: 'EnrollmentEdit', params: { id } })
}

onMounted(fetchEnrollments)
</script>

<template>
  <section class="panel">
    <div class="panel-header">
      <div>
        <h2>Enrollments</h2>
        <p class="subtitle">Manage courses and student registrations.</p>
      </div>
      <router-link class="primary-button" to="/enrollments/new">New Enrollment</router-link>
    </div>

    <div v-if="loading" class="status">Loading enrollments...</div>
    <div v-if="error" class="status error">{{ error }}</div>

    <table v-if="!loading && enrollments.length" class="data-table">
      <thead>
        <tr>
          <th>Student</th>
          <th>Course</th>
          <th>Semester</th>
          <th>Year</th>
          <th class="actions">Actions</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="enrollment in enrollments" :key="enrollment.enrolId">
          <td>{{ enrollment.studentName }}</td>
          <td>{{ enrollment.courseTitle }}</td>
          <td>{{ enrollment.semester }}</td>
          <td>{{ enrollment.year }}</td>
          <td class="actions">
            <button type="button" class="secondary-button" @click="editEnrollment(enrollment.enrolId)">Edit</button>
            <button type="button" class="danger-button" @click="deleteEnrollment(enrollment.enrolId)">Delete</button>
          </td>
        </tr>
      </tbody>
    </table>

    <div v-if="!loading && !enrollments.length" class="empty-state">
      No enrollments found. Create one to get started.
    </div>
  </section>
</template>

<style scoped>
.panel {
  max-width: 980px;
  margin: 0 auto;
  background: #fff;
  border-radius: 16px;
  padding: 1.5rem;
  box-shadow: 0 16px 40px rgba(15, 23, 42, 0.08);
}

.panel-header {
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: 1rem;
  margin-bottom: 1rem;
}

.subtitle {
  margin: 0.25rem 0 0;
  color: #64748b;
}

.data-table {
  width: 100%;
  border-collapse: collapse;
}

.data-table th,
.data-table td {
  padding: 0.85rem 1rem;
  text-align: left;
  border-bottom: 1px solid #e2e8f0;
}

.data-table th {
  color: #334155;
  font-weight: 700;
}

.actions {
  text-align: right;
}

.primary-button,
.secondary-button,
.danger-button {
  border: none;
  border-radius: 8px;
  padding: 0.6rem 1rem;
  cursor: pointer;
  font-weight: 600;
}

.primary-button {
  background: #2563eb;
  color: #fff;
}

.secondary-button {
  background: #e2e8f0;
  color: #0f172a;
  margin-right: 0.5rem;
}

.danger-button {
  background: #ef4444;
  color: #fff;
}

.status {
  margin: 1rem 0;
  color: #334155;
}

.status.error {
  color: #b91c1c;
}

.empty-state {
  padding: 1.5rem;
  border: 1px dashed #cbd5e1;
  border-radius: 12px;
  color: #64748b;
}
</style>
