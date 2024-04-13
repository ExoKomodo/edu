<template>
  <button @click="handler" class="px-3 m-2 py-1 transition duration-250 rounded-md text-sm
    font-medium text-gray-300 hover:text-virgil hover:bg-midnightGreen bg-mysticStone sm:block">
    <div>
      {{ text }}
    </div>
  </button>
</template>

<script setup lang="ts">
const props = defineProps<{
  text: string,
  handler: () => void
}>();
</script>
