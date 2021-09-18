import Repository from "../Repository";
import { AuthInput } from "../models/AuthInput";
import MyEncript from "@/service/myEncript";

export default class SecurityRepository extends Repository<any> {
  constructor() {
    super("Security");
  }

  public async login(data: AuthInput) {
    this.service.setEndPoint(this.endPoint() + "/login");
    const myEncript = new MyEncript();
    data.password = myEncript.encrypt(data.password);
    try {
      const httpResponse = await this.service.post(data);
      if (httpResponse.status >= 400) return Promise.reject(httpResponse);
      const model = httpResponse.data;
      return model;
    } catch (err) {
      return Promise.reject(err);
    }
  }
}
