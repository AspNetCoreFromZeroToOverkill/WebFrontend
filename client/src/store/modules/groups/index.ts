import { Module } from 'vuex';
import { GroupsState } from './state';
import { RootState } from '@/store/state';
import { actions } from './actions';
import { mutations } from './mutations';

export const groups: Module<GroupsState, RootState> = {
    namespaced: true,
    actions,
    mutations,
    state: {
        groups: []
    }
};
