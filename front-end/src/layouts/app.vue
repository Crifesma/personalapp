<template lang="pug">
  v-app(dark)
    v-navigation-drawer(v-model="drawer" :mini-variant="miniVariant" :clipped="clipped" fixed app)
      v-list
        v-list-item(v-for="(item, i) in items" :key="i" :to="item.to" router exact)
          v-list-item-action
            v-icon {{ item.icon }}
          v-list-item-content(@click="changeTitle(item.title)")
            v-list-item-title(v-text="item.title")
    v-app-bar(:clipped-left="clipped" fixed app)
      v-app-bar-nav-icon(@click.stop="drawer = !drawer")
      v-btn(icon @click.stop="miniVariant = !miniVariant")
        v-icon mdi-{{ `chevron-${miniVariant ? 'right' : 'left'}` }}
      v-btn(icon @click.stop="clipped = !clipped")
        v-icon mdi-application
      v-btn(icon @click.stop="fixed = !fixed")
        v-icon mdi-minus
      v-toolbar-title(v-text="title")
      v-spacer
      | Cristian Escobar Marin
      v-menu(left bottom rounded offset-y)
        template(v-slot:activator="{on,attrs}")
          v-btn(icon v-bind="attrs" v-on="on")
            v-icon mdi-account-circle
        v-list
          v-list-item(link)
              v-list-item-action
                v-icon mdi-login
              v-list-item-content(@click="login")
                v-list-item-title Iniciar sesión
          v-list-item(link)
            v-list-item-action
              v-icon mdi-logout
            v-list-item-content(@click="logout")
              v-list-item-title Cerrar sesión
    v-main
      v-container
        router-view
    v-footer(:absolute="!fixed" app)
      span &copy; {{ new Date().getFullYear() }}
</template>
<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import IMenuLayoutItem from "@/data/models/MenuLayoutItem";

@Component
export default class LayoutDefault extends Vue {
  clipped = false;
  drawer = false;
  fixed = false;

  items: Array<IMenuLayoutItem> = [
    {
      icon: "mdi-apps",
      title: "Panel de Control",
      to: "/home"
    },
    {
      icon: "mdi-printer-pos",
      title: "Venta",
      to: "/sale"
    },
    {
      icon: "mdi-cash-multiple",
      title: "Compra / Gasto",
      to: "/buy"
    },
    {
      icon: "mdi-clipboard-list-outline",
      title: "Productos",
      to: "/product"
    }
  ];

  miniVariant = false;
  right = true;
  rightDrawer = false;
  title = "Vuetify.js";

  changeTitle(title: string) {
    this.title = title;
  }

  logout() {
    sessionStorage.removeItem("userInfo");
  }
}
</script>
