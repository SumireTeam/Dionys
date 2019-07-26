import fetch from 'isomorphic-unfetch';
import { config } from '../../config';
import { CrudService, Identity } from '../service';

export class ApiServiceError extends Error {
  public readonly response: Response;

  public constructor(
    response: Response,
    message: string = null,
  ) {
    super(message);
    this.response = response;
  }
}

function request(method: string, endpoint: string, data: object = null) {
  const options: RequestInit = {
    method,
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json',
    },
    body: data ? JSON.stringify(data) : null,
    credentials: 'same-origin',
  };

  const url = `${config.api.baseURL}/api/${endpoint}`;
  return fetch(url, options);
}

export abstract class ApiCrudService<TData extends Identity, TModel extends Identity>
  implements CrudService<TModel> { // eslint-disable-line @typescript-eslint/indent

  protected readonly endpoint: string;

  protected constructor(endpoint: string) {
    this.endpoint = endpoint;
    this.mapToModel = this.mapToModel.bind(this);
    this.mapToData = this.mapToData.bind(this);
  }

  protected abstract mapToModel(data: TData): TModel;
  protected abstract mapToData(model: TModel): TData;

  public async list(): Promise<TModel[]> {
    const response = await request('get', this.endpoint);
    if (response.status !== 200) {
      throw new ApiServiceError(response);
    }

    const items = await response.json() as TData[];
    return items.map(this.mapToModel);
  }

  public async get(id: string): Promise<TModel> {
    const response = await request('get', `${this.endpoint}/${id}`);
    if (response.status !== 200) {
      throw new ApiServiceError(response);
    }

    const item = await response.json() as TData;
    return this.mapToModel(item);
  }

  public async create(model: TModel): Promise<TModel> {
    const data = this.mapToData(model);
    const response = await request('post', this.endpoint, data);
    if (response.status !== 201) {
      throw new ApiServiceError(response);
    }

    const item = await response.json() as TData;
    return this.mapToModel(item);
  }

  public async update(model: TModel): Promise<TModel> {
    const data = this.mapToData(model);
    const response = await request('put', `${this.endpoint}/${model.id}`, data);
    if (response.status !== 204) {
      throw new ApiServiceError(response);
    }

    return model;
  }

  public async delete(id: string): Promise<TModel> {
    const response = await request('delete', `${this.endpoint}/${id}`);
    if (response.status !== 200) {
      throw new ApiServiceError(response);
    }

    const item = await response.json() as TData;
    return this.mapToModel(item);
  }
}
