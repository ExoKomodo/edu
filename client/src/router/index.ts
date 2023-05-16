import { createRouter, createWebHistory } from 'vue-router';
import HomeView from '../views/HomeView.vue';

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
    },
    {
      path: '/blog',
      name: 'blogs',
      component: () => import('../views/BlogView.vue'),
    },
    {
      path: '/blog/:id',
      name: 'blog',
      props: true,
      component: () => import('../views/BlogPostView.vue'),
    },
    {
      path: '/course',
      name: 'courses',
      component: () => import('../views/CourseView.vue'),
    },
    {
      path: '/course/:id',
      name: 'course',
      props: true,
      component: () => import('../views/CoursePostView.vue'),
    },
    {
      path: '/about',
      name: 'about',
      component: () => import('../views/AboutView.vue'),
    },
    {
      path: '/contact',
      name: 'contact',
      component: () => import('../views/ContactView.vue'),
    },
  ],
});

export default router;
