<script setup lang="ts">
import { computed, onMounted, reactive, ref } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import api from '@/services/api'
import type { Course, EnrollmentRequest, EnrollmentResponse, Student } from '@/types'

const route = useRoute()
const router = useRouter()
const enrollmentId = route.params.id ? Number(route.params.id) : null
const isEdit = computed(() => enrollmentId !== null)

const students = ref<Student[]>([])
const courses = ref<Course[]>([])
const loading = ref(false)
const error = ref('')

const form = reactive<EnrollmentRequest>({
  studentId: 0,
  courseId: 0,
  semester: 'Fall',
  year: new Date().getFullYear()
})

const loadLookups = async () => {
  const [studentResponse, courseResponse] = await Promise.all([
    api.get<Student[]>('/students'),
    api.get<Course[]>('/courses')
  ])

  students.value = studentResponse.data
  courses.value = courseResponse.data
}

const loadEnrollment = async () => {
  if (!enrollmentId) return

  const response = await api.get<EnrollmentResponse>(`/enrollment/${enrollmentId}`)
  const enrollment = response.data

  form.studentId = enrollment.studentId
  form.courseId = enrollment.courseId
  form.semester = enrollment.semester
  form.year = enrollment.year
}

const saveEnrollment = async () => {
  error.value = ''

  if (!form.studentId || !form.courseId || !form.semester || !form.year) {
    error.value = 'Please complete all fields.'
    return
  }

  try {
    if (isEdit.value && enrollmentId) {
      await api.put(`/enrollment/${enrollmentId}`, form)
    } else {
      await api.post('/enrollment', form)
    }

    router.push('/enrollments')
  } catch (err) {
    error.value = 'Unable to save enrollment.'
  }
}

onMounted(async () => {
  loading.value = true
  try {
    await loadLookups()
    if (isEdit.value) {
      await loadEnrollment()
    }
  } catch (err) {
    error.value = 'Failed to load form data.'
  } finally {
    loading.value = false
  }
})
</script>

<template>
  <section class="panel">
    <div class="panel-header">
      <div>
        <h2>{{ isEdit ? 'Edit Enrollment' : 'Add Enrollment' }}</h2>
        <p class="subtitle">Use student and course selections to manage registrations.</p>
      </div>
    </div>

    <div v-if="loading" class="status">Loading form...</div>
    <div v-if="error" class="status error">{{ error }}</div>

    <form v-if="!loading" @submit.prevent="saveEnrollment" class="form-grid">
      <label>
        Student
        <select v-model.number="form.studentId" required>
          <option value="0" disabled>Select student</option>
          <option v-for="student in students" :key="student.studentId" :value="student.studentId">
            {{ student.name }}
          </option>
        </select>
      </label>

      <label>
        Course
        <select v-model.number="form.courseId" required>
          <option value="0" disabled>Select course</option>
          <option v-for="course in courses" :key="course.courseId" :value="course.courseId">
            {{ course.title }}
          </option>
        </select>
      </label>

      <label>
        Semester
        <input type="text" v-model="form.semester" placeholder="Fall, Spring" required />
      </label>

      <label>
        Year
        <input type="number" v-model.number="form.year" min="2000" max="2100" required />
      </label>

      <div class="form-actions">
        <button type="button" class="secondary-button" @click="router.push('/enrollments')">Cancel</button>
        <button type="submit" class="primary-button">{{ isEdit ? 'Update' : 'Create' }}</button>
      </div>
    </form>
  </section>
</template>

<style scoped>
.panel {
  max-width: 640px;
  margin: 0 auto;
  background: #fff;
  border-radius: 16px;
  padding: 1.5rem;
  box-shadow: 0 16px 40px rgba(15, 23, 42, 0.08);
}

.panel-header {
  margin-bottom: 1.25rem;
}

.subtitle {
  margin: 0.25rem 0 0;
  color: #64748b;
}

.form-grid {
  display: grid;
  gap: 1rem;
}

label {
  display: grid;
  gap: 0.5rem;
  color: #334155;
  font-weight: 600;
}

input,
select {
  width: 100%;
  border: 1px solid #cbd5e1;
  border-radius: 12px;
  padding: 0.9rem 1rem;
  background: #f8fafc;
  color: #0f172a;
}

.form-actions {
  display: flex;
  justify-content: flex-end;
  gap: 0.75rem;
  margin-top: 1rem;
}

.primary-button,
.secondary-button {
  border: none;
  border-radius: 10px;
  padding: 0.85rem 1.2rem;
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
}

.status {
  margin-bottom: 1rem;
  color: #334155;
}

.status.error {
  color: #b91c1c;
}
</style>
