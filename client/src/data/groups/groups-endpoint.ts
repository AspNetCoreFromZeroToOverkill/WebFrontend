import { UpdateGroupDetailsCommandModel } from './models/update-group-details-command.model';
import { CreateGroupCommandModel } from './models/create-group-command.model';
import { CreateGroupCommandResultModel } from './models/create-group-command-result.model';
import { UpdateGroupDetailsCommandResultModel } from './models/update-group-details-command-result.model';
import { GroupDetailsModel } from './models/group-details.model';
import { GroupSummaryModel } from './models/group-summary.model';

export interface GroupsEndpoint {
    getAll(): Promise<GroupSummaryModel[]>;
    getById(id: number): Promise<GroupDetailsModel>;
    add(createGroupCommand: CreateGroupCommandModel): Promise<CreateGroupCommandResultModel>;
    update(id: number, updateGroupCommand: UpdateGroupDetailsCommandModel)
    : Promise<UpdateGroupDetailsCommandResultModel>;
    remove(id: number): Promise<void>;
}
