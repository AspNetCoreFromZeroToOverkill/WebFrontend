import axios from 'axios';
import { AuthInfoModel } from './models/auth-info-model';
import { AuthEndpoint } from './auth-endpoint';
import { BaseService } from '../base-service';

// TODO: handle eventual errors

export class AuthService extends BaseService implements AuthEndpoint {
    private readonly baseUrl: string = '/api/auth';

    public async getAuthInfo(): Promise<AuthInfoModel | null> {
        try {
            const response = await axios.get(`${this.baseUrl}/info`);
            return response.data;
        } catch (error) {
            // if we get a 401, the user isn't logged in
            if (error.response.status === 401) {
                return null;
            }
            throw error;
        }

    }
}
