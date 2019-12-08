<template>
  <div class="home">
    <h1 class="title">PlayBall</h1>
    <img class="logo" alt="PlayBall" src="../assets/football.svg">
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import HelloWorld from '@/components/HelloWorld.vue'; // @ is an alias to /src

@Component({})
export default class Home extends Vue {}
</script>
<style lang="scss" scoped>
img.logo {
  max-height: 30vh;
}
</style>
