import { GroupsState } from './modules/groups/state';

export interface RootState { }

export interface ApplicationState extends RootState {
    groups: GroupsState;
}
