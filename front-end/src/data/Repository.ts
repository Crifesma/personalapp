import IRepository from "./IRepository";
import IService from "./models/Service";
import Service from "@/service";
import ServiceResponse from "./models/ServiceResponse";
import QueryResult from "./models/QueryResult";
import FilterData from "./models/FilterData";
import QueryParameters from "./models/QueryParameters";

export default abstract class Repository<TIEntity> implements IRepository<TIEntity> {
  constructor(name: string, url = "") {
    this.name = name;
    this.url = url;
  }

  protected readonly service: IService = new Service();

  public url!: string;
  public name!: string;

  endPoint(): string {
    if (this.url != "") return this.url;
    return this.name;
  }

  public async get(id: number): Promise<TIEntity> {
    this.service.setEndPoint(this.endPoint());
    const httpResponse: ServiceResponse<TIEntity> = await this.service.get(id);
    const model = httpResponse.data;
    return model;
  }

  public async getAll(parameters: QueryParameters): Promise<QueryResult<TIEntity[]>> {
    this.service.setEndPoint(this.endPoint());
    const httpResponse: ServiceResponse<QueryResult<TIEntity[]>> = await this.service.getAll(
      parameters
    );
    const model: QueryResult<TIEntity[]> = httpResponse.data;
    return model;
  }

  public async filter(
    filterDatas: Array<FilterData>,
    parameters: QueryParameters
  ): Promise<QueryResult<TIEntity[]>> {
    this.service.setEndPoint(this.endPoint());
    const httpResponse: ServiceResponse<QueryResult<TIEntity[]>> = await this.service.filter(
      filterDatas,
      parameters
    );
    const model: QueryResult<TIEntity[]> = httpResponse.data;
    return model;
  }

  public async post(data: TIEntity): Promise<TIEntity> {
    this.service.setEndPoint(this.endPoint());
    const httpResponse = await this.service.post(data);
    const model = httpResponse.data;
    return model;
  }

  public async update(id: number, data: TIEntity): Promise<boolean> {
    this.service.setEndPoint(this.endPoint());
    const httpResponse = await this.service.put(id, data);
    const model = httpResponse.data;
    return true;
  }

  public async delete(id: number): Promise<boolean> {
    this.service.setEndPoint(this.endPoint());
    console.log(id);
    const httpResponse = await this.service.delete(id);
    const model = httpResponse.data;
    return true;
  }
}
