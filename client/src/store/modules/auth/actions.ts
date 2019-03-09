import { ActionTree } from 'vuex';
import { RootState } from '@/store/state';
import { AuthEndpoint } from '@/data/auth/auth-endpoint';
import { AuthState } from './state';

export const types = {
    LOAD_INFO: 'auth/loadInfo'
};

export const makeActions = (authEndpoint: AuthEndpoint): ActionTree<AuthState, RootState> => {
    return {
        async loadInfo({ commit }): Promise<void> {
            const authInfo = await authEndpoint.getAuthInfo();
            if (!!authInfo) {
                commit('setUser', authInfo);
            } else {
                commit('setAnonymousUser');
            }
        }
    };
};
