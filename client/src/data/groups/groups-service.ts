import { GroupsEndpoint } from './groups-endpoint';
import { GroupModel } from './group-model';
import axios from 'axios';

// TODO: handle eventual errors

export class GroupsService implements GroupsEndpoint {
    private readonly baseUrl: string = '/api/groups';

    public async GetAll(): Promise<GroupModel[]> {
        const response = await axios.get(this.baseUrl);
        return response.data;
    }

    public async GetById(groupId: number): Promise<GroupModel> {
        const response = await axios.get(`${this.baseUrl}/${groupId}`);
        return response.data;
    }

    public async Add(group: GroupModel): Promise<GroupModel> {
        const response = await axios.post(this.baseUrl, group);
        return response.data;
    }

    public async Update(group: GroupModel): Promise<GroupModel> {
        const response = await axios.put(`${this.baseUrl}/${group.id}`, group);
        return response.data;
    }

    public async Remove(groupId: number): Promise<void> {
        const response = await axios.delete(`${this.baseUrl}/${groupId}`);
    }
}
