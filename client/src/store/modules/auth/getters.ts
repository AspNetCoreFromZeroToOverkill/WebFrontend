import { AuthState } from './state';
import { GetterTree } from 'vuex';
import { RootState } from '@/store/state';

export const types = {
    INFO: 'auth/info'
};

export const getters: GetterTree<AuthState, RootState> = {
    info(state: AuthState): AuthState {
        return { ...state };
    }
};

