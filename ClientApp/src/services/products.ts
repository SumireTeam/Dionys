import fetch from 'isomorphic-unfetch';
import { config } from '../config';
import { ApiServiceError, CrudService } from './service';

export interface ProductData {
  readonly name: string;
  readonly proteins: number;
  readonly fats: number;
  readonly carbohydrates: number;
  readonly energy: number;
  readonly commentary: string;
}

export type Product = ProductData & {
  readonly id: string;
}

export class ProductServiceError extends ApiServiceError { }

const baseURL = config.api.baseURL;
const listURL = `${baseURL}/api/products`;

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

  public async create(data: ProductData): Promise<Product> {
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
