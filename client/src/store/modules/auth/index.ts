import { Module } from 'vuex';
import { RootState } from '@/store/state';
import { makeActions } from './actions';
import { mutations } from './mutations';
import { AuthService } from '@/data/auth/auth-service';
import { AuthState } from './state';
import { getters } from './getters';

export const auth: Module<AuthState, RootState> = {
    namespaced: true,
    actions: makeActions(new AuthService()), // TODO: maybe not the best place to create it
    mutations,
    getters,
    state: {
        loggedIn: false,
        loaded: false,
        username: null
    }
};
