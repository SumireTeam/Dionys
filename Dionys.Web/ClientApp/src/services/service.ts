export interface Identity {
  id: string;
}

export interface CrudService<TModel extends Identity> {
  list(): Promise<TModel[]>;
  get(id: string): Promise<TModel>;
  create(model: TModel): Promise<TModel>;
  update(model: TModel): Promise<TModel>;
  delete(id: string): Promise<TModel>;
}
