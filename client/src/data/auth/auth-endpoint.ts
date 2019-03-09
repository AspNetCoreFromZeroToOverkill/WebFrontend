import { AuthInfoModel } from './models/auth-info-model';

export interface AuthEndpoint {
    getAuthInfo(): Promise<AuthInfoModel | null>;
}
