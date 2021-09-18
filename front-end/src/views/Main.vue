<template lang="pug">
  v-app(dark)
    v-navigation-drawer(v-model="drawer" :mini-variant="miniVariant" :clipped="clipped" fixed app)
      v-list
        v-list-item(v-for="(item, i) in items" :key="i" :to="item.to" router exact :disabled="!item.enabled")
          v-list-item-action
            v-icon {{ item.icon }}
          v-list-item-content(@click="changeTitle(item.title)" )
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
      | {{fullName}}
      v-menu(left bottom rounded offset-y)
        template(v-slot:activator="{on,attrs}")
          v-btn(icon v-bind="attrs" v-on="on")
            v-icon mdi-account-circle
        v-list
          //- v-list-item(link)
          //-     v-list-item-action
          //-       v-icon mdi-login
          //-     v-list-item-content(@click="login")
          //-       v-list-item-title Iniciar sesión
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
import MenuLayoutItem from "@/data/models/MenuLayoutItem";
import VueJwtDecode from "vue-jwt-decode";

@Component({})
export default class App extends Vue {
  clipped = false;
  drawer = false;
  fixed = false;
  fullName = "";
  miniVariant = false;
  right = true;
  rightDrawer = false;
  title = "Home";

  items: Array<MenuLayoutItem> = [
    {
      icon: "mdi-apps",
      title: "Inicio",
      to: "/app/home",
      enabled: true
    },
    // {
    //   icon: "",
    //   title: "Permisos",
    //   to: "/app/permissions",
    //enabled: true
    // },
    // {
    //   icon: "",
    //   title: "Roles",
    //   to: "/app/roles",
    //enabled: true
    // },
    {
      icon: "",
      title: "Ususarios",
      to: "/app/users",
      enabled: true
    }
  ];

  changeTitle(title: string): void {
    this.title = title;
  }

  logout(): void {
    sessionStorage.removeItem("userInfo");
    this.$router.push({ name: "index" });
  }

  created(): void {
    let userInfo: string | null = sessionStorage.getItem("userInfo");
    if (userInfo != null) {
      const tocken = JSON.parse(userInfo);
      const decodeTockent = VueJwtDecode.decode(tocken);
      const stringPermissions = decodeTockent.permissions;
      console.log(stringPermissions);
      this.items[1].enabled = stringPermissions.split("")[3] === "1";
      this.fullName = decodeTockent.fullName;
    }
  }
}
</script>
