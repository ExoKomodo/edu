<template>
  <nav class="bg-richBlack shadow-lg sticky">
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
        <div v-if="!isSmall">
          <div class="ml-4 flex items-center md:ml-6">
            <a
              @click="login"
              class="px-3 m-2 py-1 transition duration-250 rounded-md text-sm
              font-medium text-gray-300 hover:text-virgil hover:bg-midnightGreen bg-mysticStone xs:hidden sm:block"
              v-if="!user.email">
                <div>
                  login
                </div>
            </a>
            <a
              @click="logoutReal"
              class="px-3 m-2 py-1 transition duration-250 rounded-md text-sm
              font-medium text-gray-300 hover:text-virgil hover:bg-midnightGreen bg-mysticStone xs:hidden sm:block"
              v-if="user.email">
                <div>
                  logout
                </div>
            </a>
            <hr class="bg-virgil color-virgil w-3 border-1.5"/>
            <!-- TODO - fix hidden items, swap with hamburger menu & dropdown -->
            <RouterLink v-for="route in routes"
                :to=route.path
                class="px-3 m-2 py-1 transition duration-250 rounded-md text-sm 
                font-medium text-gray-300 hover:text-virgil hover:bg-midnightGreen bg-mysticStone xs:hidden sm:block"
                >
                <div>
                  {{ route.name }}
                </div>
            </RouterLink>
            <hr class="bg-virgil color-virgil w-3 border-1.5"/>
            <a
              :href=bugRoute.path
              class="px-3 m-2 py-1 transition duration-250 rounded-md text-sm 
                font-medium text-gray-300 hover:text-virgil hover:bg-rust bg-rufous xs:hidden sm:block"
              target="_blank">
                <div>
                  {{ bugRoute.name }}
                </div>
            </a>
          </div>
        </div>
        <div  v-else>
          <div>
            <div
              @click="handleClick"
              v-if="!isVisible"
              class="text-white border-2 border-slate-500 p-2 rounded">
              <i class="fas fa-bars"></i>
            </div>
            <div
              v-else
              @click="handleClick"
              class="text-white border-2 border-slate-500 p-2 rounded">
              <i class="fas fa-times"></i>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div v-show="isVisible" >
      <div class="flex items-center flex-row justify-evenly navBorder">
        <RouterLink v-for="route in routes"
            :to=route.path
            class="px-2 m-1 py-1.5 transition duration-250 rounded-md text-sm 
            font-medium text-gray-300 hover:text-virgil hover:bg-midnightGreen
             bg-mysticStone"
            >
            <div>
              {{ route.name }}
            </div>
        </RouterLink>
        <!-- Needs the wrapping span to obey flex rules -->
        <span>
          <hr class="bg-virgil color-virgil w-3  border-1.5"/>
        </span>
        <a
          :href=bugRoute.path
          class="px-2 m-1 py-1.5 transition duration-250 rounded-md text-sm 
            font-medium text-gray-300 hover:text-virgil hover:bg-rust
             bg-rufous"
          target= '_blank'
          >
            <div>
              {{ bugRoute.name }}
            </div>
        </a>
      </div>
    </div>
  </nav>
</template>
  
<script lang="ts" setup>
import { ref } from 'vue'
import { RouterLink } from 'vue-router';
import router from '../router/index';
import { useAuth0 } from '@auth0/auth0-vue';

const { loginWithRedirect, logout, user } = useAuth0();

const routes = router.options.routes.filter(
  route => !route.props
);
const bugRoute = {
  name: 'bug?',
  path: 'https://github.com/ExoKomodo/Edu/issues/new/choose',
};

const isVisible = ref(false);
const handleClick = () => {
  isVisible.value = !isVisible.value;
}

const isSmall = ref(false);
function updateWindowSize() {
  isSmall.value = window.innerWidth <= 640;
}
window.addEventListener("resize", updateWindowSize);
updateWindowSize();

const login = () => {
  loginWithRedirect();
}

const logoutReal = () => {
  logout(
    {
      logoutParams: {
        returnTo: window.location.origin,
      },
    },
  );
}

if (user.value.email) {
  alert('You are logged in with email: ' + user.value.email);
}
</script>
