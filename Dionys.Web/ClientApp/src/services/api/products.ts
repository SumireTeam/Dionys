import { ApiCrudService, ApiServiceError, request } from './service';
import { Product } from '../../models';
import { ProductService } from '../products';

export interface ProductData {
  readonly id: string;
  readonly name: string;
  readonly description: string;
  readonly protein: number;
  readonly fat: number;
  readonly carbohydrates: number;
  readonly calories: number;
}

export class ApiProductService extends ApiCrudService<ProductData, Product>
  implements ProductService { // eslint-disable-line @typescript-eslint/indent
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

  public async search(name: string): Promise<Product[]> {
    name = name.replace(/[%_]/g, '');
    const response = await request('get', `products/search/?q=%${name}%`);
    if (response.status !== 200) {
      throw new ApiServiceError(response);
    }

    const items = await response.json() as ProductData[];
    return items.map(this.mapToModel);
  }
}
