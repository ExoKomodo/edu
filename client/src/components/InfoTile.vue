<template>
    <div class="relative rounded-lg my-10 p-6 m-5 bg-opacity-50 backdrop-filter backdrop-blur-lg bg-white shadow-xl xs:flex xs:flex-row sm:flex-col flex-col">
        <p class="text-2xl font-bold">{{ props.header }}</p>
        <p class="xs:hidden sm:block">
            Lorem ipsum dolor sit, amet consectetur 
            adipisicing elit. Numquam ex blanditiis at, qui deleniti, nostrum 
            nihil autem vel sequi voluptatum, fuga voluptate reiciendis! Ullam 
            ipsum corporis eveniet possimus quam ex.
        </p>
    </div>
</template>

<script setup lang="ts">
const props = defineProps(['header'])
</script>