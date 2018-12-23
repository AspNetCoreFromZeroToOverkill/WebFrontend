import { GetterTree } from 'vuex';
import { GroupsState, Group } from './state';
import { RootState } from '@/store/state';

export const getterTypes = {
    GROUPS: 'groups'
};

export const getters: GetterTree<GroupsState, RootState> = {
    groups(state: GroupsState): Group[] {
        return [...state.groups];
    }
};
