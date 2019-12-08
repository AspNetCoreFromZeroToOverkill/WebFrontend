import { GroupsEndpoint } from './groups-endpoint';
import axios from 'axios';
import { UpdateGroupDetailsCommandModel } from './models/update-group-details-command.model';
import { BaseService } from '../base-service';
import { CreateGroupCommandModel } from './models/create-group-command.model';
import { UpdateGroupDetailsCommandResultModel } from './models/update-group-details-command-result.model';
import { CreateGroupCommandResultModel } from './models/create-group-command-result.model';
import { GroupSummaryModel } from './models/group-summary.model';
import { GroupDetailsModel } from './models/group-details.model';

// TODO: handle eventual errors

export class GroupsService extends BaseService implements GroupsEndpoint {
    private readonly baseUrl: string = '/api/groups';

    public async getAll(): Promise<GroupSummaryModel[]> {
        const response = await axios.get(this.baseUrl);
        return response.data;
    }

    public async getById(id: number): Promise<GroupDetailsModel> {
        const response = await axios.get(`${this.baseUrl}/${id}`);
        return response.data;
    }

    public async add(createGroupCommand: CreateGroupCommandModel): Promise<CreateGroupCommandResultModel> {
        const response = await axios.post(this.baseUrl, createGroupCommand, this.getAxiosConfig());
        return response.data;
    }

    public async update(id: number, updateGroupCommand: UpdateGroupDetailsCommandModel)
    : Promise<UpdateGroupDetailsCommandResultModel> {
        const response = await axios.put(`${this.baseUrl}/${id}`, updateGroupCommand, this.getAxiosConfig());
        return response.data;
    }

    public async remove(id: number): Promise<void> {
        await axios.delete(`${this.baseUrl}/${id}`, this.getAxiosConfig());
    }
}
