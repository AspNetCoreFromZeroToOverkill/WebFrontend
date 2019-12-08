<template>
  <div v-if="isVisible" class="loader-backdrop">
    <progress class="progress is-small is-primary" max="100">15%</progress>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { State, namespace } from 'vuex-class';

const loaderModule = namespace('loader');

@Component
export default class Loader extends Vue {
  @loaderModule.State('isVisible') private isVisible!: boolean;
}
</script>
<style lang="scss" scoped>
.loader-backdrop {
  position: fixed;
  z-index: 1000;
  background-color: rgba(0,0,0,0.2);
  height: 100%;
  width: 100%;
}
</style>