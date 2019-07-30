import { CrudService } from './service';
import { Product } from '../models';

export type ProductService = CrudService<Product> & {
  search(name: string): Promise<Product[]>;
};
