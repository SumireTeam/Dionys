import { ApiProductService, ApiConsumedService } from './api';
import { ProductService } from './products';
import { ConsumedService } from './consumed';

export class ServiceProvider {
  public static get productService(): ProductService {
    return new ApiProductService();
  }

  public static get consumedService(): ConsumedService {
    return new ApiConsumedService();
  }
}
