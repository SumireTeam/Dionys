import { CrudServiceBase } from './service';
import { Consumed } from '../models';

export interface ConsumedData {
  readonly id: string;
  readonly productId: string;
  readonly weight: number;
  readonly time: string;
}

export class ConsumedService extends CrudServiceBase<ConsumedData, Consumed> {
  public constructor() {
    super('eatenproducts');
  }

  protected mapToModel(data: ConsumedData): Consumed {
    return {
      id: data.id,
      productId: data.productId,
      weight: +data.weight,
      date: new Date(data.time),
    };
  }

  protected mapToData(model: Consumed): ConsumedData {
    return {
      id: model.id,
      productId: model.productId,
      weight: +model.weight,
      time: model.date.toISOString(),
    };
  }
}
