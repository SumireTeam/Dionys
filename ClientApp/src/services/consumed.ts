import { CrudServiceBase } from './service';
import { Consumed } from '../models';

export interface ConsumedData {
  readonly id: string;
  readonly product: string;
  readonly weight: number;
  readonly timestamp: string;
}

export class ConsumedService extends CrudServiceBase<ConsumedData, Consumed> {
  public constructor() {
    super('consumedproducts');
  }

  protected mapToModel(data: ConsumedData): Consumed {
    return {
      id: data.id,
      productId: data.product,
      weight: +data.weight,
      date: new Date(data.timestamp),
    };
  }

  protected mapToData(model: Consumed): ConsumedData {
    return {
      id: model.id,
      product: model.productId,
      weight: +model.weight,
      timestamp: model.date.toISOString(),
    };
  }
}
