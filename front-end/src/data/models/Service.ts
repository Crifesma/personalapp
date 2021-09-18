import ServiceResponse from "./ServiceResponse";
import FilterData from "./FilterData";
import QueryParameters from "./QueryParameters";
import QueryResult from "./QueryResult";

export default interface IService {
  setEndPoint(_url: String): void;

  get<T>(id: number, parameters?: QueryParameters): Promise<ServiceResponse<T>>;

  getAll<T>(parameters: QueryParameters): Promise<ServiceResponse<QueryResult<T>>>;
  filter<T>(
    filterDatas: Array<FilterData>,
    parameters: QueryParameters
  ): Promise<ServiceResponse<QueryResult<T>>>;

  post<T>(data: T): Promise<ServiceResponse<T>>;

  put<T>(id: string, data: T): Promise<ServiceResponse>;

  delete(id: string): Promise<ServiceResponse>;
}
