import { ActionTree } from 'vuex';
import { GroupsState } from './state';
import { RootState } from '@/store/state';
import { GroupsEndpoint } from '@/data/groups/groups-endpoint';
import { CreateGroupCommandModel } from '@/data/groups/models/create-group-command.model';
import { GroupDetailsModel } from '@/data/groups/models';

export const types = {
    LOAD_GROUP: 'groups/loadGroup',
    LOAD_GROUPS: 'groups/loadGroups',
    ADD_GROUP: 'groups/add',
    UPDATE_GROUP: 'groups/update',
    REMOVE_GROUP: 'groups/remove'
};

export const makeActions = (groupsEndpoint: GroupsEndpoint): ActionTree<GroupsState, RootState> => {
    return {
        async loadGroup({ commit }, groupId: number): Promise<void> {
            const group = await groupsEndpoint.getById(groupId);
            commit('setGroupDetails', group);
        },
        async loadGroups({ commit }): Promise<void> {
            const groups = await groupsEndpoint.getAll();
            commit('setGroups', groups);
        },
        async add({ commit }, group: CreateGroupCommandModel): Promise<void> {
            const result = await groupsEndpoint.add(group);
            const refreshedGroup = await groupsEndpoint.getById(result.id);
            commit('add', refreshedGroup);
        },
        async update({ commit }, group: GroupDetailsModel): Promise<void> {
            const updatedGroup = await groupsEndpoint.update(group.id, group);
            commit('update', updatedGroup);
            commit('updateGroupDetails', updatedGroup);
        },
        async remove({ commit }, groupId: number): Promise<void> {
            await groupsEndpoint.remove(groupId);
            commit('remove', groupId);
        }
    };
};
