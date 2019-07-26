import { CrudServiceBase } from './service';
import { Product } from '../models';

export interface ProductData {
  readonly id: string;
  readonly name: string;
  readonly description: string;
  readonly protein: number;
  readonly fat: number;
  readonly carbohydrates: number;
  readonly calories: number;
}

export class ProductService extends CrudServiceBase<ProductData, Product> {
  public constructor() {
    super('products');
  }

  protected mapToModel(data: ProductData): Product {
    return {
      id: data.id,
      name: data.name,
      description: data.description,
      protein: +data.protein,
      fat: +data.fat,
      carbs: +data.carbohydrates,
      calories: +data.calories,
    };
  }

  protected mapToData(model: Product): ProductData {
    return {
      id: model.id,
      name: model.name,
      description: model.description,
      protein: +model.protein,
      fat: +model.fat,
      carbohydrates: +model.carbs,
      calories: +model.calories,
    };
  }
}
