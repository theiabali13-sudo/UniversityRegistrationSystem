<script setup lang="ts"></script>

<template>
  <div class="app-shell">
    <header>
      <div class="brand">University Registration</div>
      <nav>
        <router-link to="/students">Students</router-link>
        <router-link to="/students/new">Add Student</router-link>
      </nav>
    </header>

    <main>
      <router-view />
    </main>
  </div>
</template>

<style scoped>
.app-shell {
  min-height: 100vh;
  display: flex;
  flex-direction: column;
  font-family: Inter, system-ui, sans-serif;
  color: #0f172a;
  background: #f8fafc;
}

header {
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 1.25rem 2rem;
  background: linear-gradient(90deg, #0f172a 0%, #111827 100%);
  color: #fff;
  box-shadow: 0 4px 16px rgba(15, 23, 42, 0.16);
}

.brand {
  font-size: 1.15rem;
  font-weight: 700;
  letter-spacing: 0.1em;
  text-transform: uppercase;
}

nav {
  display: flex;
  gap: 1rem;
}

nav a {
  color: #cbd5e1;
  padding: 0.5rem 0.75rem;
  border-radius: 0.5rem;
  text-decoration: none;
  transition: background 0.2s ease, color 0.2s ease;
}

nav a:hover {
  background: rgba(255, 255, 255, 0.08);
  color: #fff;
}

nav a.router-link-active {
  color: #0f172a;
  background: #f8fafc;
}

main {
  flex: 1;
  padding: 2rem;
}
</style>
