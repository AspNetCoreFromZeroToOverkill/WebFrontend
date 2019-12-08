import { GroupsState } from './state';
import { MutationTree } from 'vuex';
import { GroupSummaryModel } from '@/data/groups/models/group-summary.model';
import { GroupDetailsModel, UpdateGroupDetailsCommandResultModel } from '@/data/groups/models';

export const mutations: MutationTree<GroupsState> = {
    setGroupDetails(state: GroupsState, group: GroupDetailsModel): void {
        state.groupDetails = { ...group };
    },
    updateGroupDetails(state: GroupsState, updateResult: UpdateGroupDetailsCommandResultModel): void {
        state.groupDetails = Object.assign({}, state.groupDetails, updateResult);
    },
    setGroups(state: GroupsState, groups: GroupSummaryModel[]): void {
        state.groups = [...groups];
    },
    add(state: GroupsState, group: GroupSummaryModel): void {
        state.groups = [...state.groups, group];
    },
    update(state: GroupsState, group: GroupSummaryModel): void {
        const index = state.groups.findIndex(g => g.id === group.id);
        state.groups = [...state.groups.slice(0, index), group, ...state.groups.slice(index + 1, state.groups.length)];
    },
    remove(state: GroupsState, groupId: number): void {
        state.groups = state.groups.filter(g => g.id !== groupId);
    },
};
