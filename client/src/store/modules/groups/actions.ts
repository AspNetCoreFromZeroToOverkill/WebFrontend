import { ActionTree } from 'vuex';
import { GroupsState, Group } from './state';
import { RootState } from '@/store/state';
import { mutationTypes } from './mutations';

let currentId: number = 2;

export const actionTypes = {
    LOAD_GROUPS: 'loadGroups',
    ADD_GROUP: 'addGroup',
    UPDATE_GROUP: 'updateGroup',
    REMOVE_GROUP: 'removeGroup',
};

export const actions: ActionTree<GroupsState, RootState> = {
    loadGroups({ commit }): void {
        // TODO: invoke api to fetch the groups
        const groups: Group[] = [
            { id: 1, name: 'Sample Group', rowVersion: 'aaa' },
            { id: 2, name: 'Another Sample Group', rowVersion: 'bbb' }
        ];
        commit(mutationTypes.SET_GROUPS, groups);
    },
    addGroup({ commit }, group: Group): void {
        group.id = ++currentId;
        commit(mutationTypes.ADD_GROUP, group);
    },
    updateGroup({ commit }, group: Group): void {
        commit(mutationTypes.UPDATE_GROUP, group);
    },
    removeGroup({commit}, groupId: number): void {
        commit(mutationTypes.REMOVE_GROUP, groupId);
    }
};
