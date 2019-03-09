import Vue from 'vue';
import Router from 'vue-router';
import Home from './views/Home.vue';
import store from './store';
import * as authActions from './store/modules/auth/actions';
import * as authGetters from './store/modules/auth/getters';

Vue.use(Router);

const router = new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home
    },
    {
      path: '/groups',
      name: 'groups',
      component: () => import('./views/Groups.vue'),
      meta: { requiresAuthentication: true }
    },
    {
      path: '/about',
      name: 'about',
      // route level code-splitting
      // this generates a separate chunk (about.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import(/* webpackChunkName: "about" */ './views/About.vue')
    },
  ],
});

router.beforeEach(async (to, from, next) => {
  if (!store.getters[authGetters.types.INFO].loaded) {
    await store.dispatch(authActions.types.LOAD_INFO);
  }
  if (to.matched.some(record => record.meta.requiresAuthentication)
    && !store.getters[authGetters.types.INFO].loggedIn) {
    window.location.href = `/api/auth/login?returnUrl=${window.location.href}`;
  } else {
    next();
  }
});

export default router;
