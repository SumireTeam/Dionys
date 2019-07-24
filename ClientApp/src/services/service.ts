import fetch from 'isomorphic-unfetch';
import { config } from '../config';

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

export interface Model {
  readonly id: string;
}

export interface CrudService<TData extends object, TModel extends Model & TData> {
  list(): Promise<TModel[]>;
  get(id: string): Promise<TModel>;
  create(data: TData): Promise<TModel>;
  update(model: TModel): Promise<TModel>;
  delete(id: string): Promise<TModel>;
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

export abstract class CrudServiceBase<TData extends object, TModel extends Model & TData>
  implements CrudService<TData, TModel> { // eslint-disable-line @typescript-eslint/indent

  protected readonly endpoint: string;

  protected constructor(endpoint: string) {
    this.endpoint = endpoint;
  }

  public async list(): Promise<TModel[]> {
    const response = await request('get', this.endpoint);
    if (response.status !== 200) {
      throw new ApiServiceError(response);
    }

    return await response.json() as TModel[];
  }

  public async get(id: string): Promise<TModel> {
    const response = await request('get', `${this.endpoint}/${id}`);
    if (response.status !== 200) {
      throw new ApiServiceError(response);
    }

    return await response.json() as TModel;
  }

  public async create(data: TData): Promise<TModel> {
    const response = await request('post', this.endpoint, data);
    if (response.status !== 201) {
      throw new ApiServiceError(response);
    }

    return await response.json() as TModel;
  }

  public async update(model: TModel): Promise<TModel> {
    const response = await request('put', `${this.endpoint}/${model.id}`, model);
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

    return await response.json() as TModel;
  }
}
