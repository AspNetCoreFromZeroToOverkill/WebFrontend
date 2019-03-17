<template>
  <div id="app">
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
    <template v-else>
      Loading...
    </template>
  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import { State, namespace } from 'vuex-class';
import { AuthInfoViewModel } from './models/auth-info-view-model';

const authModule = namespace('auth');

@Component
export default class App extends Vue {
  @authModule.Getter('info') private authInfo!: AuthInfoViewModel;

  public get isUserLoggedIn(): boolean {
    return !!this.authInfo ? this.authInfo.loggedIn : false;
  }

  public get isAuthInfoLoaded(): boolean {
    return !!this.authInfo ? this.authInfo.loaded : false;
  }

  public get loginUrl(): string {
    return `/api/auth/login?returnUrl=${window.location.href}`;
  }
}
</script>

<style lang="scss">
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
#nav {
  padding: 30px;
  a {
    font-weight: bold;
    color: #2c3e50;
    &.router-link-exact-active {
      color: #42b983;
    }
  }
}
</style>
