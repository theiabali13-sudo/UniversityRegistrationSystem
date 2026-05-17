export interface EnrollmentResponse {
  enrolId: number
  studentId: number
  courseId: number
  studentName: string
  courseTitle: string
  semester: string
  year: number
}

export interface EnrollmentRequest {
  studentId: number
  courseId: number
  semester: string
  year: number
}

export interface Student {
  studentId: number
  name: string
  email: string
  majorId: number
  courseCount: number
}

export interface Course {
  courseId: number
  title: string
  credits: number
  deptId: number
}
