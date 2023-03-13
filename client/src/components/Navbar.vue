<template>
  <nav class="bg-richBlack shadow-lg">
    <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
      <div class="flex items-center justify-between h-[8vh]">
        <div class="flex-shrink-0">
          <a href="/"
             class="flex items-center">
            <span class="ml-2 p-2 rounded text-virgil text-xl transition duration-250
             hover:text-virgil hover:bg-midnightGreen font-bold">
              <span class="text-tiffanyBlue">EK</span>.<strong>Edu</strong>
            </span>
          </a>
        </div>
        <div class="hidden md:block">
          <div class="ml-4 flex items-center md:ml-6">
            <!-- TODO - fix hidden items, swap with hamburger menu & dropdown -->
            <!-- TODO - fix v-if !route.props type error -->
            <RouterLink v-for="route in routes"
                :to=route.path
                class="px-3 m-2 py-2 transition duration-250 rounded-md text-sm 
                font-medium text-gray-300 hover:text-virgil hover:bg-midnightGreen bg-gray-800 xs:hidden sm:block"
                >
                <div v-if="!route.props">
                  {{ route.name }}
                </div>
            </RouterLink>
          </div>
        </div>
      </div>
    </div>
  </nav>
</template>
  
<script lang="ts" setup>
import { RouterLink } from 'vue-router';
import router from '../router/index';

const routes = router.options.routes;


</script>
