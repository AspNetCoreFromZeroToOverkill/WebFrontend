import Vue from 'vue';
import Vuex, { StoreOptions } from 'vuex';
import { RootState } from './state';
import { groups } from './modules/groups';
import { auth } from './modules/auth';

Vue.use(Vuex);


const options: StoreOptions<RootState> = {
  state: {},
  modules: {
    auth,
    groups
  }
};

export default new Vuex.Store(options);
