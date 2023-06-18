import HomeView from '@/views/HomeView.vue';
import { createRouter, createWebHistory } from 'vue-router';

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
      component: () => import('@/views/BlogView.vue'),
    },
    {
      path: '/blog/:id',
      name: 'blog',
      props: true,
      component: () => import('@/views/BlogPostView.vue'),
    },
    {
      path: '/course',
      name: 'courses',
      component: () => import('@/views/CourseView.vue'),
    },
    {
      path: '/course/:id',
      name: 'course',
      props: true,
      component: () => import('@/views/CoursePostView.vue'),
    },
    {
      path: '/section',
      name: 'sections',
      component: () => import('@/views/SectionView.vue'),
    },
    {
      path: '/section/:id',
      name: 'section',
      props: true,
      component: () => import('@/views/SectionPostView.vue'),
    },
    {
      path: '/assignment',
      name: 'assignments',
      component: () => import('@/views/AssignmentView.vue'),
    },
    {
      path: '/assignment/:id',
      name: 'assignment',
      props: true,
      component: () => import('@/views/AssignmentPostView.vue'),
    },
    {
      path: '/about',
      name: 'about',
      component: () => import('@/views/AboutView.vue'),
    },
  ],
});

export default router;
