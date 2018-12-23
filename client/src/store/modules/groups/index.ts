import { GroupsState } from './state';
import { getters } from './getters';
import { makeActions } from './actions';
import { mutations } from './mutations';
import { Module } from 'vuex';
import { RootState } from '../../state';
import { GroupsService } from '@/data/groups/groups-service';

const groupsState: GroupsState = {
    groups: []
};

export const groups: Module<GroupsState, RootState> = {
    namespaced: true,
    state: groupsState,
    getters,
    actions: makeActions(new GroupsService()),
    mutations
};
