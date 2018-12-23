import { MutationTree } from 'vuex';
import { GroupsState, Group } from './state';

export const mutationTypes = {
    SET_GROUPS: 'setGroups',
    ADD_GROUP: 'addGroup',
    UPDATE_GROUP: 'updateGroup',
    REMOVE_GROUP: 'removeGroup',
};

export const mutations: MutationTree<GroupsState> = {
    setGroups(state: GroupsState, groups: Group[]): void {
        state.groups = [...groups];
    },
    addGroup(state: GroupsState, group: Group): void {
        state.groups = [...state.groups, group];
    },
    updateGroup(state: GroupsState, group: Group): void {
        const index = state.groups.findIndex(g => g.id === group.id);
        state.groups = [...state.groups.slice(0, index), group, ...state.groups.slice(index + 1, state.groups.length)];
    },
    removeGroup(state: GroupsState, groupId: number): void {
        state.groups = state.groups.filter(g => g.id !== groupId);
    }
};
