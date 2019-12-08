import { AuthInfoModel } from '@/data/auth/models/auth-info-model';

export interface AuthState {
    loggedIn: boolean;
    loaded: boolean;
    info: AuthInfoModel | null;
}
