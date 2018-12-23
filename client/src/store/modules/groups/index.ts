import { GroupsState } from './state';
import { getters } from './getters';
import { actions } from './actions';
import { mutations } from './mutations';
import { Module } from 'vuex';
import { RootState } from '../../state';

const groupsState: GroupsState = {
    groups: []
};

export const groups: Module<GroupsState, RootState> = {
    namespaced: true,
    state: groupsState,
    getters,
    actions,
    mutations
};
