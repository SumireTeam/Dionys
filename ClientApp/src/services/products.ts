import { CrudServiceBase } from './service';
import { Product } from '../models';

export interface ProductData {
  readonly id: string;
  readonly name: string;
  readonly proteins: number;
  readonly fats: number;
  readonly carbohydrates: number;
  readonly energy: number;
  readonly commentary: string;
}

export class ProductService extends CrudServiceBase<ProductData, Product> {
  public constructor() {
    super('products');
  }

  protected mapToModel(data: ProductData): Product {
    return {
      id: data.id,
      name: data.name,
      description: data.commentary,
      protein: +data.proteins,
      fat: +data.fats,
      carbs: +data.carbohydrates,
      calories: +data.energy,
    };
  }

  protected mapToData(model: Product): ProductData {
    return {
      id: model.id,
      name: model.name,
      commentary: model.description,
      proteins: +model.protein,
      fats: +model.fat,
      carbohydrates: +model.carbs,
      energy: +model.calories,
    };
  }
}
