import { Product } from "./product";

export interface IConsumed {
    readonly id: string;
    readonly productId: string;
    readonly product: Product;
    readonly weight: number;
    readonly date: Date;
}
