import { AuthInfoViewModel } from './auth-info-view-model';

export interface AuthInfoViewModel {
        loggedIn: boolean;
        loaded: boolean;
        user: AuthInfoViewModel | null;
}
