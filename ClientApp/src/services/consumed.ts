import fetch from 'isomorphic-unfetch';
import { config } from '../config';
import { ApiServiceError, CrudService } from './service';

export interface ConsumedData {
  readonly productId: string;
  readonly weight: number;
  readonly time: string;
}

export type Consumed = ConsumedData & {
  readonly id: string;
}

export class ConsumedServiceError extends ApiServiceError { }

const baseURL = config.api.baseURL;
const listURL = `${baseURL}/api/eatenproducts`;

export class ConsumedService implements CrudService<Consumed> {
  public async list(): Promise<Consumed[]> {
    const response = await fetch(listURL, {
      headers: {
        'Accept': 'application/json',
      },
      credentials: 'same-origin',
    });

    if (response.status !== 200) {
      throw new ConsumedServiceError(response);
    }

    return await response.json() as Consumed[];
  }

  public async get(id: string): Promise<Consumed> {
    const response = await fetch(`${listURL}/${id}`, {
      headers: {
        'Accept': 'application/json',
      },
      credentials: 'same-origin',
    });

    if (response.status !== 200) {
      throw new ConsumedServiceError(response);
    }

    return await response.json() as Consumed;
  }

  public async create(data: ConsumedData): Promise<Consumed> {
    const response = await fetch(listURL, {
      method: 'post',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(data),
      credentials: 'same-origin',
    });

    if (response.status !== 201) {
      throw new ConsumedServiceError(response);
    }

    return await response.json() as Consumed;
  }

  public async update(consumed: Consumed): Promise<Consumed> {
    const response = await fetch(`${listURL}/${consumed.id}`, {
      method: 'put',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(consumed),
      credentials: 'same-origin',
    });

    if (response.status !== 204) {
      throw new ConsumedServiceError(response);
    }

    return consumed;
  }

  public async delete(id: string): Promise<Consumed> {
    const response = await fetch(`${listURL}/${id}`, {
      method: 'delete',
      headers: {
        'Accept': 'application/json',
      },
      credentials: 'same-origin',
    });

    if (response.status !== 200) {
      throw new ConsumedServiceError(response);
    }

    return await response.json() as Consumed;
  }
}
