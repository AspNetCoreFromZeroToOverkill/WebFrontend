import { Module } from 'vuex';
import { LoaderState } from './state';
import { RootState } from '@/store/state';
import { mutations } from './mutations';

export const loader: Module<LoaderState, RootState> = {
    namespaced: true,
    mutations,
    state: {
        isVisible: false
    }
};
