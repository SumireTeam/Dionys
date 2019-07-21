import fetch from 'isomorphic-unfetch';
import { config } from '../config';

export interface Product {
  readonly id: string;
  readonly name: string;
  readonly proteins: number;
  readonly fats: number;
  readonly carbohydrates: number;
  readonly energy: number;
  readonly commentary: string;
}

export interface CreateProduct {
  readonly name: string;
  readonly proteins: number;
  readonly fats: number;
  readonly carbohydrates: number;
  readonly energy: number;
  readonly commentary: string;
}

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

export class ProductServiceError extends ApiServiceError { }

const baseURL = config.api.baseURL;
const listURL = `${baseURL}/api/products`;

export interface CrudService<T> {
  list(): Promise<T[]>;
  get(id: string): Promise<T>;
  create(data: object): Promise<T>;
  update(model: T): Promise<T>;
  delete(id: string): Promise<T>;
}

export class ProductService implements CrudService<Product> {
  public async list(): Promise<Product[]> {
    const response = await fetch(listURL, {
      headers: {
        'Accept': 'application/json',
      },
      credentials: 'same-origin',
    });

    if (response.status !== 200) {
      throw new ProductServiceError(response);
    }

    return await response.json() as Product[];
  }

  public async get(id: string): Promise<Product> {
    const response = await fetch(`${listURL}/${id}`, {
      headers: {
        'Accept': 'application/json',
      },
      credentials: 'same-origin',
    });

    if (response.status !== 200) {
      throw new ProductServiceError(response);
    }

    return await response.json() as Product;
  }

  public async create(data: CreateProduct): Promise<Product> {
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
      throw new ProductServiceError(response);
    }

    return await response.json() as Product;
  }

  public async update(product: Product): Promise<Product> {
    const response = await fetch(`${listURL}/${product.id}`, {
      method: 'put',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(product),
      credentials: 'same-origin',
    });

    if (response.status !== 204) {
      throw new ProductServiceError(response);
    }

    return product;
  }

  public async delete(id: string): Promise<Product> {
    const response = await fetch(`${listURL}/${id}`, {
      method: 'delete',
      headers: {
        'Accept': 'application/json',
      },
      credentials: 'same-origin',
    });

    if (response.status !== 200) {
      throw new ProductServiceError(response);
    }

    return await response.json() as Product;
  }
}
