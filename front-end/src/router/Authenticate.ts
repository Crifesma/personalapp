import { NavigationGuardNext, Route } from "vue-router";
import Service from "@/service/setup";

export default (to: Route, from: Route, next: NavigationGuardNext) => {
  const tocken: string | null = sessionStorage.getItem("userInfo");
  if (tocken != null && tocken.length > 0) {
    const uid = JSON.parse(tocken).uid;
    Service.defaults.headers.common.Authorization = `Bearer ${uid}`;
    next();
  } else next("/index");
};
