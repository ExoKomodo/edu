<template>
  <div class="spinner">
    <div></div>
    <div></div>
    <div></div>
    <div></div>
  </div>
</template>

<style scoped>
.spinner {
  display: inline-block;
  position: relative;
  width: 3rem;
  height: 3rem;
}

.spinner div {
  box-sizing: border-box;
  display: block;
  position: absolute;
  width: 2rem;
  height: 2rem;
  margin: 0.25rem;
  border: 0.25rem solid #fff;
  border-radius: 50%;
  animation: spinner 1.5s cubic-bezier(0.5, 0, 0.5, 1) infinite;
  border-bottom-color: transparent;
  border-left-color: transparent;
  border-right-color: transparent;
}

.spinner div:nth-child(1) {
  animation-delay: -0.45s;
}

.spinner div:nth-child(2) {
  animation-delay: -0.3s;
}

.spinner div:nth-child(3) {
  animation-delay: -0.15s;
}

@keyframes spinner {
  0% {
    transform: rotate(0deg);
  }

  100% {
    transform: rotate(360deg);
  }
}
</style>
