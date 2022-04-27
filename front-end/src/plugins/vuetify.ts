import Vue from "vue";
import Vuetify from "vuetify";
import "vuetify/dist/vuetify.min.css";
import "@mdi/font/css/materialdesignicons.css"; // Ensure you are using css-loader
import colors from "vuetify/lib/util/colors";
Vue.use(Vuetify);

const ic: "mdi" | "mdiSvg" | "md" | "fa" | "faSvg" | "fa4" | undefined = "mdi";

const opts = {
  icons: {
    iconfont: ic // 'mdi' || 'mdiSvg' || 'md' || 'fa' || 'fa4' || 'faSvg'
  },

  theme: {
    dark: false,
    themes: {
      dark: {
        primary: colors.blue.darken2,
        accent: colors.grey.darken3,
        secondary: colors.amber.darken3,
        info: colors.teal.lighten1,
        warning: colors.amber.base,
        error: colors.deepOrange.accent4,
        success: colors.green.accent3
      }
    }
  }
};

export default new Vuetify(opts);
