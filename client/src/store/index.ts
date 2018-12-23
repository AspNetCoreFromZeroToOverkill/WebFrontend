import Vue from 'vue';
import Vuex, { StoreOptions } from 'vuex';
import { groups } from './modules/groups';
import { RootState } from './state';

Vue.use(Vuex);

const storeOptions: StoreOptions<RootState> = {
  state: {},
  modules: {
    groups
  }
};

export default new Vuex.Store(storeOptions);
