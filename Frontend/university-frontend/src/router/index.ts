import { createRouter, createWebHistory } from 'vue-router'
import StudentsList from '@/pages/StudentsList.vue'
import StudentForm from '@/pages/StudentForm.vue'

const routes = [
  { path: '/', redirect: '/students' },
  { path: '/students', name: 'Students', component: StudentsList },
  { path: '/students/new', name: 'StudentCreate', component: StudentForm },
  { path: '/:pathMatch(.*)*', redirect: '/students' }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router
