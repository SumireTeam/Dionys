export class ApiServiceError extends Error {
  public readonly response: Response;

  public constructor(
    response: Response,
    message: string = null,
  ) {
    super(message);

    this.response = response;
  }
}

export interface CrudService<T> {
  list(): Promise<T[]>;
  get(id: string): Promise<T>;
  create(data: object): Promise<T>;
  update(model: T): Promise<T>;
  delete(id: string): Promise<T>;
}
