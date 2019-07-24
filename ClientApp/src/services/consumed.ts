import { CrudServiceBase } from './service';

export interface ConsumedData {
  readonly productId: string;
  readonly weight: number;
  readonly time: string;
}

export type Consumed = ConsumedData & {
  readonly id: string;
}

export class ConsumedService extends CrudServiceBase<ConsumedData, Consumed> {
  public constructor() {
    super('eatenproducts');
  }
}
