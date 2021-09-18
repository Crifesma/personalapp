<template lang="pug">
  v-app(dark)
      v-main
        v-container
          v-row(justify="center" align="center")
            v-col(cols="12" sm="8" md="6")
              v-flex(class='login-form' class='text-center')
                .display-1.mb-3 #[v-icon.mr-2(large) mdi-login] Ingreso
                v-card
                  v-card-text
                    .subheading
                      | Ingresa con su cuenta de google.
                    v-form
                      v-text-field(v-model='authInput.userName' light prepend-icon='mdi-account' label='Usuario' type='text' dark)
                      v-text-field(v-model='authInput.password' light
                      :append-icon="show1 ? 'mdi-eye' : 'mdi-eye-off'" prepend-icon='mdi-lock' label='Password' :type="show1 ? 'text' : 'password'" @click:append="show1 = !show1" dark)
                    div
                      v-btn(@click="login" dark).mr-1 ingresar
          v-snackbar(v-model="openMessage" :color="errorMessage?'error':'success'")
            | {{ message }}
</template>
<script lang="ts">
import { Component, Vue, Watch } from "vue-property-decorator";
import SecurityRepository from "@/data/repositories/SecurityRepository";
import { AuthInput } from "@/data/models/AuthInput";

@Component({
  components: {
    inputFilter: () => import("@/components/inputFilter.vue")
  }
})
export default class Login extends Vue {
  private show1 = false;
  private authInput: AuthInput = { userName: "", password: "" };
  private message = "";
  private openMessage = false;
  private errorMessage = false;

  securityRepository = new SecurityRepository();

  public async login(): Promise<void> {
    this.errorMessage = false;
    this.message = "";
    try {
      let response = await this.securityRepository.login(this.authInput);
      this.message = "Usuario identificado";

      this.$router.push({ name: "home" });
    } catch (err) {
      this.errorMessage = true;
      console.log({ err }, err.response);
      if (err.data != undefined) this.message = err.data.type + ": " + err.data.message;
      else this.message = err.message == "Network Error" ? "Error de red" : " Error desconocido";
    }

    this.openMessage = true;
  }
}
</script>
