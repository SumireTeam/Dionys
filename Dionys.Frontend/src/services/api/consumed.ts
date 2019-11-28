import { ApiCrudService } from "./service";
import { IConsumed } from "../../models";
import { ConsumedService } from "../consumed";
import { ProductData } from "./products";

export interface ConsumedData {
    readonly id: string;
    readonly productId: string;
    readonly product?: ProductData;
    readonly weight: number;
    readonly timestamp: string;
}

export class ApiConsumedService extends ApiCrudService<ConsumedData, IConsumed> implements ConsumedService {
    // eslint-disable-line @typescript-eslint/indent
    public constructor() {
        super("consumedproducts");
    }

    protected mapToModel(data: ConsumedData): IConsumed {
        return {
            id: data.id,
            productId: data.productId,
            product: {
                id: data.id,
                name: data.product ? data.product.name : "",
                description: data.product ? data.product.description : "",
                protein: data.product ? +data.product.protein : 0,
                fat: data.product ? +data.product.fat : 0,
                carbs: data.product ? +data.product.carbohydrates : 0,
                calories: data.product ? +data.product.calories : 0
            },
            weight: +data.weight,
            date: new Date(data.timestamp + "Z")
        };
    }

    protected mapToData(model: IConsumed): ConsumedData {
        return {
            id: model.id,
            productId: model.productId,
            weight: +model.weight,
            timestamp: model.date.toISOString()
        };
    }
}
