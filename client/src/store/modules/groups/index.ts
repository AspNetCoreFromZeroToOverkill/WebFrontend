import { Module } from 'vuex';
import { GroupsState } from './state';
import { RootState } from '@/store/state';
import { makeActions } from './actions';
import { mutations } from './mutations';
import { GroupsService } from '@/data/groups/groups-service';

export const groups: Module<GroupsState, RootState> = {
    namespaced: true,
    actions: makeActions(new GroupsService()), // TODO: maybe not the best place to create it
    mutations,
    state: {
        groups: []
    }
};
