import { GroupsEndpoint } from './groups-endpoint';
import axios, { AxiosRequestConfig } from 'axios';
import { GroupModel } from './models/group-model';
import { BaseService } from '../base-service';

// TODO: handle eventual errors

export class GroupsService extends BaseService implements GroupsEndpoint {
    private readonly baseUrl: string = '/api/groups';

    public async getAll(): Promise<GroupModel[]> {
        const response = await axios.get(this.baseUrl);
        return response.data;
    }

    public async getById(id: number): Promise<GroupModel> {
        const response = await axios.get(`${this.baseUrl}/${id}`);
        return response.data;
    }

    public async add(group: GroupModel): Promise<GroupModel> {
        const response = await axios.post(this.baseUrl, group, this.getAxiosConfig());
        return response.data;
    }

    public async update(group: GroupModel): Promise<GroupModel> {
        const response = await axios.put(`${this.baseUrl}/${group.id}`, group);
        return response.data;
    }

    public async remove(id: number): Promise<void> {
        const response = await axios.delete(`${this.baseUrl}/${id}`);
    }
}
