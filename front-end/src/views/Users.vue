<template lang="pug">
  v-card
    v-card-text.pa-5
      v-data-table.contact-listing-app(:headers='headers' item-key="rowKey" :items='list' :search='search' :items-per-page.sync='parameters.pageSize' :page.sync='parameters.currentPage' :footer-props="{\
      'items-per-page-text': 'Cuentas por pagina ',\
      }" :server-items-length='length' loading-text='Cargando' :loading='loadingTable' @update:items-per-page='changePageSize($event)' @update:page='changePage($event)')
        template(v-slot:top)
          v-toolbar.mb-8
            inputFilter(:columnsTable="headers.filter(x=>x.text!='' && (x.value=='userName' || x.value=='role'))" nameModuleStore="" @filter="loadfilter" :disabled="disabledFilter")
            v-spacer
            v-dialog(v-model='dialog' max-width='1000' )
              template(v-slot:activator="{ on  }")
                v-btn(color="primary" dark class="mb-2 text-capitalize" v-on="on" :disabled="disabledNew")
                  v-icon(class="mr-2") mdi-plus-circle
                  | Nuevo
              v-card
                v-card-title.pa-4.info
                  span.title.white--text Usuario
                v-card-text
                  v-container
                    v-row
                      v-col(cols='12' sm='6')
                        v-text-field(outlined='' hide-details v-model='editedItem.id' label='Id')
                      v-col(cols='12' sm='6')
                        v-text-field(outlined hide-details v-model='editedItem.userName' label='Nombre Usuario')
                      v-col(cols='12' sm='6')
                        v-text-field(outlined hide-details v-model='editedItem.fullName' label='Nombre Completo')
                      v-col(cols='12' sm='6')
                        v-select(outlined hide-details :items="listRoles" item-text="name"
          item-value="id" v-model='editedItem.roleId' label='Rol')
                      v-col(cols='12' sm='6')
                        v-text-field(outlined hide-details v-model='editedItem.email' label='Correo')
                      v-col(cols='12' sm='6')
                        v-text-field(outlined hide-details v-model='editedItem.address' label='Dirección')
                    v-row
                      v-col(cols='12' sm='6')
                        v-text-field(outlined hide-details v-model='editedItem.phone' label='Teléfono')
                      v-col(cols='12' sm='6')
                        v-text-field(outlined hide-details v-model='editedItem.age' label='Edad')
                    v-row
                      v-col(cols='12' sm='6')
                        v-text-field(outlined hide-details v-model='editedItem.password' :append-icon="show1 ? 'mdi-eye' : 'mdi-eye-off'"  :type="show1 ? 'text' : 'password'" @click:append="show1 = !show1" label='Password')
                v-card-actions
                  v-spacer
                  v-btn(color='error' dark @click='close') Cerrar
                  v-btn(color='success' dark @click='save') Guardar
        template(v-slot:item.role='{ item }')
          | {{listRoles.find(x=>x.id=item.roleId).name}}
        template(v-slot:item.actions='{ item }')
          v-icon.mr-2.info--text(small @click='editItem(item)' :disabled="disabledEdit") mdi-pencil
          v-icon.error--text(small @click='deleteItem(item)' :disabled="disabledDelete") mdi-delete
        template(v-slot:no-data)
          v-btn(color='primary' @click='initialize') Recargar
        template(v-slot:footer.page-text='items')
          | {{ items.pageStart }} - {{ items.pageStop }} de
          | {{ items.itemsLength }}
</template>

<script lang="ts">
import { Role } from "@/data/entities/Role";
import { User } from "@/data/entities/User";
import FilterData from "@/data/models/FilterData";
import QueryParameters from "@/data/models/QueryParameters";
import RoleRepository from "@/data/repositories/RoleRepository";
import UserRepository from "@/data/repositories/UserRepository";
import { Component, Vue, Watch } from "vue-property-decorator";
import VueJwtDecode from "vue-jwt-decode";

@Component({
  components: {
    inputFilter: () => import("@/components/inputFilter.vue")
  }
})
export default class Users extends Vue {
  private show1 = false;
  private dialog = false;
  private loadingTable = false;
  private length = 0;
  private search = "";
  private menu2 = false;
  private editedIndex = -1;
  private list: Array<any> = [];
  private listRoles: Array<any> = [];
  private currentFilters: FilterData[] = [];
  private disabledFilter = false;
  private disabledNew = false;
  private disabledEdit = false;
  private disabledDelete = false;

  private parameters: QueryParameters = {
    currentPage: 1,
    pageSize: 10
  };

  private headers: Array<any> = [
    {
      text: "Id",
      align: "start",
      value: "id"
    },
    { text: "Usuario", value: "userName" },
    { text: "Nombre compoleto", value: "fullName", sortable: false },
    { text: "Dirección", value: "address", sortable: false },
    { text: "Teléfono", value: "phone" },
    { text: "Email", value: "email", sortable: false },
    { text: "Edad", value: "age", sortable: false },
    { text: "Role", value: "role", sortable: false },
    { text: "", value: "actions", sortable: false }
  ];

  editedItem: User = {
    id: 0,
    userName: "",
    password: "",
    fullName: "",
    address: "",
    phone: "",
    email: "",
    age: 0,
    roleId: 1
  };

  defaultItem: User = {
    id: 0,
    userName: "",
    password: "",
    fullName: "",
    address: "",
    phone: "",
    email: "",
    age: 0,
    roleId: 1
  };

  userRepository = new UserRepository();
  roleRepository = new RoleRepository();

  public async initialize(): Promise<void> {
    this.loadingTable = true;
    let res = await this.userRepository.getAll(this.parameters);
    let resRoles = await this.roleRepository.getAll({ currentPage: 0, pageSize: 1000 });
    this.loadingTable = false;
    this.length = res.totalRecords;
    this.list = res.data;
    this.listRoles = resRoles.data;
  }

  async loadfilter(filters: FilterData[]): Promise<void> {
    this.loadingTable = true;
    let res = await this.userRepository.filter(filters, this.parameters);
    this.currentFilters = filters;
    this.loadingTable = false;
    this.length = res.totalRecords;
    this.list = res.data;
  }

  public close(): void {
    this.dialog = false;
  }

  public editItem(item: User): void {
    this.editedIndex = this.list.indexOf(item);
    this.editedItem = Object.assign({}, item);
    this.dialog = true;
  }

  public save(): void {
    if (this.editedIndex > -1) {
      //this.editedItem.eTag = "";
      Object.assign(this.list[this.editedIndex], this.editedItem);
      this.userRepository.update(this.editedItem.id, this.editedItem);
    } else {
      this.list.push(this.editedItem);
      this.userRepository.post(this.editedItem);
    }
    this.editedIndex = -1;
    this.close();
  }

  public async deleteItem(item: User): Promise<void> {
    this.editedIndex = this.list.indexOf(item);
    if (this.editedIndex > -1) {
      this.userRepository.delete(item.id);
      this.list.splice(this.editedIndex, 1);
    }
    this.editedIndex = -1;
  }

  public changePage(event: any): void {
    this.parameters.currentPage = event;
  }

  public changePageSize(event: any): void {
    this.parameters.pageSize = event;
  }

  @Watch("parameters", { deep: true })
  wParameters() {
    this.loadfilter(this.currentFilters);
  }

  @Watch("dialog", { deep: true })
  wdialog(value: any) {
    if (value == false)
      setTimeout(() => {
        this.editedItem = Object.assign({}, this.defaultItem);
        this.editedIndex = -1;
      }, 300);
  }

  permissionsUsers(): void {
    let userInfo: string | null = sessionStorage.getItem("userInfo");
    if (userInfo != null) {
      const tocken = JSON.parse(userInfo);
      const decodeTockent = VueJwtDecode.decode(tocken);
      const stringPermissions = decodeTockent.permissions;
      console.log(stringPermissions, decodeTockent.permissions);
      this.disabledFilter = stringPermissions.split("")[4] !== "1";
      this.disabledNew = stringPermissions.split("")[0] !== "1";
      this.disabledDelete = stringPermissions.split("")[1] !== "1";
      this.disabledEdit = stringPermissions.split("")[2] !== "1";
    }
  }

  created(): void {
    this.initialize();
    this.permissionsUsers();
  }
}
</script>
