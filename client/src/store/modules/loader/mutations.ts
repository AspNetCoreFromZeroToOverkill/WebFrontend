import { LoaderState } from './state';
import { MutationTree } from 'vuex';

export const types = {
    SHOW_LOADER: 'loader/show',
    HIDE_LOADER: 'loader/hide'
};

export const mutations: MutationTree<LoaderState> = {
    show(state: LoaderState): void {
        state.isVisible = true;
    },
    hide(state: LoaderState): void {
        state.isVisible = false;
    }
};
