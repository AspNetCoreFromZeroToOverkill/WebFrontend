import Vue from 'vue';
import Vuex, { StoreOptions, ActionContext, MutationTree, ActionTree } from 'vuex';
import { RootState } from './state';
import { groups } from './modules/groups';

Vue.use(Vuex);


const options: StoreOptions<RootState> = {
  state: {},
  modules: {
    groups
  }
};

export default new Vuex.Store(options);
