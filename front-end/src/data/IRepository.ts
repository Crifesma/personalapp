import FilterData from "./models/FilterData";
import QueryParameters from "./models/QueryParameters";
import QueryResult from "./models/QueryResult";

export default interface IRepository<T> {
  get(id: number): Promise<T>;

  getAll(parameters: QueryParameters): Promise<QueryResult<T[]>>;
  filter(filterDatas: Array<FilterData>, parameters: QueryParameters): Promise<QueryResult<T[]>>;
  post(data: T): Promise<T>;

  update(id: string, data: T): Promise<boolean>;

  delete(id: string): Promise<boolean>;
}
