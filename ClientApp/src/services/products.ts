import { CrudServiceBase } from './service';

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

export class ProductService extends CrudServiceBase<ProductData, Product> {
  public constructor() {
    super('products');
  }
}
