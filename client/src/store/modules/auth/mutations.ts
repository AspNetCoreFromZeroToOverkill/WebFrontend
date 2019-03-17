import { AuthState } from './state';
import { MutationTree } from 'vuex';
import { AuthInfoModel } from '@/data/auth/models/auth-info-model';

export const mutations: MutationTree<AuthState> = {
    setUser(state: AuthState, authInfo: AuthInfoModel): void {
        state.loggedIn = true;
        state.loaded = true;
        state.username = authInfo.name;
    },
    setAnonymousUser(state: AuthState): void {
        state.loggedIn = false;
        state.loaded = true;
        state.username = null;
    }
};
