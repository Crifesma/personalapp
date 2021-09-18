<template lang="pug">
  div
    router-view
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import MenuLayoutItem from "@/data/models/MenuLayoutItem";

@Component({})
export default class App extends Vue {
  clipped = false;
  drawer = false;
  fixed = false;

  miniVariant = false;
  right = true;
  rightDrawer = false;
  title = "Vuetify.js";

  items: Array<MenuLayoutItem> = [
    {
      icon: "mdi-apps",
      title: "Inicio",
      to: "/home"
    },
    {
      icon: "mdi-clipboard-list-outline",
      title: "Productos",
      to: "/producto"
    }
  ];

  changeTitle(title: string): void {
    this.title = title;
  }

  logout(): void {
    //
  }

  login(): void {
    //
  }
}
</script>
