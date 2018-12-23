import { GroupModel } from './group-model';

export interface GroupsEndpoint {
    GetAll(): Promise<GroupModel[]>;
    GetById(groupId: number): Promise<GroupModel>;
    Add(group: GroupModel): Promise<GroupModel>;
    Update(group: GroupModel): Promise<GroupModel>;
    Remove(groupId: number): Promise<void>;
}
