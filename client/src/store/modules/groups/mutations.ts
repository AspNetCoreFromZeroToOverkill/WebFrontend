import { GroupsState, Group } from './state';
import { MutationTree } from 'vuex';

export const mutations: MutationTree<GroupsState> = {
    setGroups(state: GroupsState, groups: Group[]): void {
        state.groups = [...groups];
    },
    add(state: GroupsState, group: Group): void {
        state.groups = [...state.groups, group];
    },
    update(state: GroupsState, group: Group): void {
        const index = state.groups.findIndex(g => g.id === group.id);
        state.groups = [...state.groups.slice(0, index), group, ...state.groups.slice(index + 1, state.groups.length)];
    },
    remove(state: GroupsState, groupId: number): void {
        state.groups = state.groups.filter(g => g.id !== groupId);
    },
};
