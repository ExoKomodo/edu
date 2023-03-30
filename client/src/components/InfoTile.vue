<template>
    <div class="relative rounded-lg my-10 p-6 m-5 bg-opacity-50 backdrop-filter backdrop-blur-lg bg-white shadow-xl xs:flex xs:flex-row sm:flex-col flex-col">
        <p class="text-2xl font-bold">{{ props.header }}</p>
        <p class="xs:hidden sm:block">
            Learn more about coding with our highly qualified instructors. <a href="#">EK.edu</a> is oriented around
            bootcamp graduates, CS graduates, and others who are interested in becoming artisans of software. We aim to 
            take learners beyond the basics of how to write code, and focus on transforming into an artist of programming.

        </p>
    </div>
</template>

<script setup lang="ts">
const props = defineProps(['header'])
</script>