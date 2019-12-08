import Vue from 'vue';
import Vuex, { StoreOptions } from 'vuex';
import { RootState } from './state';
import { groups } from './modules/groups';
import { auth } from './modules/auth';
import { loader } from './modules/loader';

Vue.use(Vuex);


const options: StoreOptions<RootState> = {
  state: {},
  modules: {
    auth,
    groups,
    loader
  }
};

export default new Vuex.Store(options);
