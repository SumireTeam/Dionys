import { ApiCrudService } from './service';
import { Consumed } from '../../models';
import { ConsumedService } from '../consumed';
import { ProductData } from './products';

export interface ConsumedData {
  readonly id: string;
  readonly productId: string;
  readonly product?: ProductData;
  readonly weight: number;
  readonly timestamp: string;
}

export class ApiConsumedService extends ApiCrudService<ConsumedData, Consumed>
  implements ConsumedService { // eslint-disable-line @typescript-eslint/indent
  public constructor() {
    super('consumedproducts');
  }

  protected mapToModel(data: ConsumedData): Consumed {
    return {
      id: data.id,
      productId: data.productId,
      product: {
        id: data.id,
        name: data.product.name,
        description: data.product.description,
        protein: +data.product.protein,
        fat: +data.product.fat,
        carbs: +data.product.carbohydrates,
        calories: +data.product.calories,
      },
      weight: +data.weight,
      date: new Date(data.timestamp + 'Z'),
    };
  }

  protected mapToData(model: Consumed): ConsumedData {
    return {
      id: model.id,
      productId: model.productId,
      weight: +model.weight,
      timestamp: model.date.toISOString(),
    };
  }
}
