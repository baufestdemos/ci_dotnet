import axios, { AxiosInstance, AxiosRequestConfig, AxiosResponse } from "axios";

abstract class HttpClient {
    protected readonly _client: AxiosInstance;
    private static _clientsPool: Map<object, HttpClient> = new Map<object, HttpClient>();

    protected constructor({ url, headersConfig }:
        {
            url: string, headersConfig?: any
        }) {

        this._client = axios.create({
            baseURL: url,
            headers: headersConfig
        });
    }

    protected async get<T = any, R = AxiosResponse<T>, D = any>(url: string, config: AxiosRequestConfig<D> = {}): Promise<R> {
        return this._client.get(url, config);
    }

    protected async post<T = any, R = AxiosResponse<T>, D = any>(url: string, data?: D, config: AxiosRequestConfig<D> = {}): Promise<R> {
        return this._client.post(url, data, config);
    }

    protected async put<T = any, R = AxiosResponse<T>, D = any>(url: string, data?: D, config: AxiosRequestConfig<D> = {}): Promise<R> {
        return this._client.put(url, data, config);
    }

    protected async delete<T = any, R = AxiosResponse<T>, D = any>(url: string, config: AxiosRequestConfig<D> = {}): Promise<R> {
        return this._client.delete(url, config);
    }

    public static instance<T extends HttpClient>(subType: new () => T): T {
        let client = this._clientsPool.get(subType)
        if (!client) {
            client = new subType();
            this._clientsPool.set(subType, client);
        }
        return client as T;
    }
}

export default HttpClient;