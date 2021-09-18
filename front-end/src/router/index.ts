import Vue from "vue";
import VueRouter, { RouteConfig } from "vue-router";
import Authenticate from "./Authenticate";

Vue.use(VueRouter);

const routes: Array<RouteConfig> = [
  {
    path: "/app",
    name: "app",
    component: () => import(/* webpackChunkName: 'about' */ "../views/Main.vue"),
    //beforeEnter: Authenticate,
    redirect: () => {
      return "/app/home";
    },

    children: [
      {
        path: "/app/home",
        name: "home",
        component: () => import(/* webpackChunkName: 'about' */ "../views/Home.vue")
        //beforeEnter: Authenticate
      },
      {
        path: "/app/permissions",
        name: "permissions",
        component: () => import(/* webpackChunkName: 'about' */ "../views/Permissions.vue")
        //beforeEnter: Authenticate
      },
      {
        path: "/app/roles",
        name: "roles",
        component: () => import(/* webpackChunkName: 'about' */ "../views/Roles.vue")
        //beforeEnter: Authenticate
      },
      {
        path: "/app/users",
        name: "users",
        component: () => import(/* webpackChunkName: 'about' */ "../views/Users.vue")
      }
    ],
    meta: { requiresAuth: true }
  },
  {
    path: "/index",
    name: "index",
    component: () => import(/* webpackChunkName: 'about' */ "../views/Login.vue")
  },
  {
    path: "/",
    name: "main",
    component: () => import(/* webpackChunkName: 'about' */ "../App.vue"),
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
