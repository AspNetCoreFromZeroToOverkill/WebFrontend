import { ActionTree } from 'vuex';
import { GroupsState, Group } from './state';
import { RootState } from '@/store/state';

export const types = {
    LOAD_GROUPS: 'groups/loadGroups',
    ADD_GROUP: 'groups/add',
    UPDATE_GROUP: 'groups/update',
    REMOVE_GROUP: 'groups/remove'
};

export const actions: ActionTree<GroupsState, RootState> = {
    loadGroups({ commit }): void {
        // TODO: fetch groups from the api
        const groups = [
            { id: 1, name: 'Sample Group', rowVersion: 'aaa' },
            { id: 2, name: 'Another Sample Group', rowVersion: 'bbb' }
        ];
        commit('setGroups', groups);
    },
    add({ commit }, group: Group): void {
        // TODO: make the api request before committing to the store
        commit('add', group);
    },
    update({ commit }, group: Group): void {
        // TODO: make the api request before committing to the store
        commit('update', group);
    },
    remove({ commit }, groupId: number): void {
        // TODO: make the api request before committing to the store
        commit('remove', groupId);
    }
};
