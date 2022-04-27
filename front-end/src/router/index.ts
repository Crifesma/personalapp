import Vue from "vue";
import VueRouter, { RouteConfig } from "vue-router";
import Authenticate from "./Authenticate";

Vue.use(VueRouter);

const routes: Array<RouteConfig> = [
  {
    path: "/app",
    name: "app",
    component: () => import("../views/Main.vue"),
    redirect: () => {
      return "/app/home";
    },

    children: [
      {
        path: "/app/home",
        name: "home",
        component: () => import("../views/Home.vue")
      },
      {
        path: "/app/permissions",
        name: "permissions",
        component: () => import("../views/Permissions.vue")
      },
      {
        path: "/app/roles",
        name: "roles",
        component: () => import("../views/Roles.vue")
      },
      {
        path: "/app/users",
        name: "users",
        component: () => import("../views/Users.vue")
      }
    ],
    meta: { requiresAuth: true }
  },
  {
    path: "/index",
    name: "index",
    component: () => import("../views/Login.vue")
  },
  {
    path: "/",
    name: "main",
    component: () => import("../App.vue"),
    redirect: () => {
      return "/index";
    }
  },
  {
    path: "*",
    redirect: () => {
      return "/app";
    }
  }
];

const router = new VueRouter({
  mode: "history",
  base: process.env.VUE_APP_BASE_URL,
  routes
});

router.beforeEach(Authenticate);

export default router;
