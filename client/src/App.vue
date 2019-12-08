<template>
  <div id="app">
    <Loader />
    <template v-if="isAuthInfoLoaded">
      <div id="nav">
        <router-link to="/">Home</router-link>
        | <router-link to="/about">About</router-link>
        <template v-if="isUserLoggedIn">
        | <router-link to="/groups">Groups</router-link>
        </template>
        <template v-else>
        | <a v-bind:href="loginUrl">Login</a>
        </template>
      </div>
      <router-view/>
    </template>
  </div>
  
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import { State, namespace } from 'vuex-class';
import { AuthState } from './store/modules/auth/state';
import Loader from '@/components/shared/Loader.vue';

const authModule = namespace('auth');

@Component({
  components: {
    Loader
  }
})
export default class App extends Vue {
  @authModule.Getter('info') private authInfo!: AuthState;

  public get isUserLoggedIn(): boolean {
    return !!this.authInfo ? this.authInfo.loggedIn : false;
  }

  public get isAuthInfoLoaded(): boolean {
    return !!this.authInfo ? this.authInfo.loaded : false;
  }

  public get loginUrl(): string {
    return `/auth/login?returnUrl=${window.location.href}`;
  }
}
</script>

<style lang="scss">
$progress-border-radius: 0;
$progress-bar-background-color: rgba(0,0,0,0.05);

@import 'bulma';
html {
  background-color: #f2f2f2;
}

#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: $black;
  
}
#nav {
  padding: 30px;
  a {
    font-weight: bold;
    color: $black;
    &.router-link-exact-active {
      color: $primary;
    }
  }
}
</style>
