import { ApiCrudService } from './service';
import { Consumed } from '../../models';
import { ConsumedService } from '../consumed';

export interface ConsumedData {
  readonly id: string;
  readonly product: string;
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
