import Repository from "../Repository";
import { AuthInput } from "../models/AuthInput";
import MyEncript from "@/service/myEncript";
import Service from "@/service/setup";
import VueJwtDecode from "vue-jwt-decode";

export default class SecurityRepository extends Repository<any> {
  constructor() {
    super("Security");
  }

  public async login(data: AuthInput): Promise<any> {
    const dataForsend: AuthInput = { userName: "", password: "" };
    Object.assign(dataForsend, data);
    this.service.setEndPoint(this.endPoint() + "/login");
    const myEncript = new MyEncript();
    dataForsend.password = myEncript.encrypt(dataForsend.password);
    try {
      const httpResponse = await this.service.post(dataForsend);
      if (httpResponse.status >= 400) return Promise.reject(httpResponse);
      const model: any = httpResponse.data;
      sessionStorage.setItem("userInfo", JSON.stringify(model.tokens));
      sessionStorage.setItem("fullName", VueJwtDecode.decode(model.tokens).fullName);
      Service.defaults.headers["Authorization"] = `Bearer ${model.tokens}`;

      return model;
    } catch (err) {
      return Promise.reject(err);
    }
  }
}
