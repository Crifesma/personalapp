import { NavigationGuardNext, Route } from "vue-router";
import Service from "@/service/setup";
import VueJwtDecode from "vue-jwt-decode";

//Todo: pendiente validacion por permisos al acceder a una ruta.

export default (to: Route, from: Route, next: NavigationGuardNext): void => {
  if (to.matched.some(record => record.meta.requiresAuth)) {
    const tocken: string | null = sessionStorage.getItem("userInfo");
    if (tocken != null && tocken.length > 0) {
      const uid = JSON.parse(tocken);
      const decodeTockent = VueJwtDecode.decode(uid);
      const current_time = new Date().getTime() / 1000;
      if (current_time > decodeTockent.exp) {
        console.log("cosas");
        sessionStorage.removeItem("userInfo");
        next("/index");
      }
      Service.defaults.headers.common.Authorization = `Bearer ${uid}`;
      next();
    } else {
      to.matched;
      console.log("cosas", to.matched);
      next("/index");
    }
  } else next();
};
