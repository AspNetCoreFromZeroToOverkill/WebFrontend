import { AxiosRequestConfig } from 'axios';

export class BaseService {
    protected getAxiosConfig(): AxiosRequestConfig {
        return { xsrfHeaderName: 'X-XSRF-TOKEN', xsrfCookieName: 'XSRF-TOKEN'};
    }
}
