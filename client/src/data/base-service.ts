import { AxiosRequestConfig } from 'axios';

export class BaseService {
    // TODO: there's probably a better way to do this configuration
    protected getAxiosConfig(): AxiosRequestConfig {
        return { xsrfHeaderName: 'X-XSRF-TOKEN', xsrfCookieName: 'XSRF-TOKEN'};
    }
}
