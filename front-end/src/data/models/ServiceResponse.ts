export default interface IServiceResponse<T = any> {
  data: T;
  status: number;
  statusText: string;
  headers: any;
  request?: any;
  config: any;
}
