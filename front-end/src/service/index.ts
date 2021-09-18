import IService from "@/data/models/Service";
import conectApi from "./setup";
import ServiceResponse from "@/data/models/ServiceResponse";
import QueryParameters from "@/data/models/QueryParameters";
import FilterData from "@/data/models/FilterData";
import QueryResult from "@/data/models/QueryResult";

export default class Service implements IService {
  public url!: string;

  setEndPoint(_url: string): void {
    this.url = _url;
  }

  get<T>(id: number, parameters?: QueryParameters): Promise<ServiceResponse<T>> {
    let source = `${this.url}/`;
    if (id != undefined) source = source + `${id}`;
    if (parameters != undefined)
      source = source + `?currentPage=${parameters.currentPage}&pageSize=${parameters.pageSize}`;
    return conectApi.get(source);
  }
  getAll<T>(parameters: QueryParameters): Promise<ServiceResponse<QueryResult<T>>> {
    return conectApi.get(
      `${this.url}?currentPage=${parameters.currentPage}&pageSize=${parameters.pageSize}`
    );
  }

  filter<T>(
    filterDatas: FilterData[],
    parameters: QueryParameters
  ): Promise<ServiceResponse<QueryResult<T>>> {
    return conectApi.post(
      `${this.url}/Filter?currentPage=${parameters.currentPage}&pageSize=${parameters.pageSize}`,
      filterDatas
    );
  }

  post<T>(data: T) {
    return conectApi.post(`${this.url}`, data);
  }
  put<T>(id: number, data: T) {
    return conectApi.put(`${this.url}/${id}`, data);
  }
  delete<T>(id: number) {
    return conectApi.delete(`${this.url}/${id}`);
  }
}
