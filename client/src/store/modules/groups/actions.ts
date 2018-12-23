import { ActionTree } from 'vuex';
import { GroupsState, Group } from './state';
import { RootState } from '@/store/state';
import { mutationTypes } from './mutations';
import { GroupsEndpoint } from '@/data/groups/groups-endpoint';

export const actionTypes = {
    LOAD_GROUPS: 'loadGroups',
    ADD_GROUP: 'addGroup',
    UPDATE_GROUP: 'updateGroup',
    REMOVE_GROUP: 'removeGroup',
};

export const makeActions = (groupsService: GroupsEndpoint): ActionTree<GroupsState, RootState> => {
    return {
        async loadGroups({ commit }): Promise<void> {
            const groups: Group[] = await groupsService.GetAll();
            commit(mutationTypes.SET_GROUPS, groups);
        },
        async addGroup({ commit }, group: Group): Promise<void> {
            const addedGroup: Group = await groupsService.Add(group);
            commit(mutationTypes.ADD_GROUP, addedGroup);
        },
        async updateGroup({ commit }, group: Group): Promise<void> {
            const updatedGroup: Group = await groupsService.Update(group);
            commit(mutationTypes.UPDATE_GROUP, updatedGroup);
        },
        async removeGroup({commit}, groupId: number): Promise<void> {
            await groupsService.Remove(groupId);
            commit(mutationTypes.REMOVE_GROUP, groupId);
        }
    };
};
