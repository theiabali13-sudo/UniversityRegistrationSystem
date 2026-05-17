import api from './api'
import type { Student } from '@/types'

type StudentCreateRequest = {
  name: string
  email: string
  majorId: number
}

const endpoint = '/Students'

const getStudents = async (): Promise<Student[]> => {
  const response = await api.get<Student[]>(endpoint)
  return response.data
}

const createStudent = async (payload: StudentCreateRequest): Promise<Student> => {
  const response = await api.post<Student>(endpoint, payload)
  return response.data
}

const deleteStudent = async (id: number): Promise<void> => {
  await api.delete(`${endpoint}/${id}`)
}

export default {
  getStudents,
  createStudent,
  deleteStudent
}
