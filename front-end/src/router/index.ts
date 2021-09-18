import Vue from "vue";
import VueRouter, { RouteConfig } from "vue-router";
import Authenticate from "./Authenticate";

Vue.use(VueRouter);

const routes: Array<RouteConfig> = [
  {
    path: "/",
    name: "h",
    component: () => import(/* webpackChunkName: 'about' */ "../App.vue"),
    beforeEnter: Authenticate,
    redirect: to => {
      return "/index";
    },

    children: [
      {
        path: "/home",
        name: "home",
        component: () => import(/* webpackChunkName: 'about' */ "../views/Home.vue")
      }
    ]
  },
  {
    path: "/index",
    name: "index",
    component: () => import(/* webpackChunkName: 'about' */ "../views/Login.vue")
  }
];

const router = new VueRouter({
  mode: "history",
  base: process.env.VUE_APP_BASE_URL,
  routes
});

export default router;
