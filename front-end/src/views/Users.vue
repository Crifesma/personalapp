<template lang="pug">
  v-card
    v-card-text.pa-5
      v-data-table.contact-listing-app(:headers='headers' item-key="rowKey" :items='list' :search='search' :items-per-page.sync='parameters.pageSize' :page.sync='parameters.currentPage' :footer-props="{\
      'items-per-page-text': 'Accounts for page ',\
      }" :server-items-length='length' loading-text='Cargando' :loading='loadingTable' @update:items-per-page='changePageSize($event)' @update:page='changePage($event)')
        template(v-slot:top)
          v-toolbar.mb-8
            inputFilter(:columnsTable="headers.filter(x=>x.text!='' && (x.value=='userName' || x.value=='role'))" nameModuleStore="" @filter="loadfilter" :disabled="disabledFilter")
            v-spacer
            v-dialog(v-model='dialog' max-width='1000' )
              template(v-slot:activator="{ on  }")
                v-btn(color="primary" class="mb-2 text-capitalize" v-on="on" :disabled="disabledNew")
                  v-icon(class="mr-2") mdi-plus-circle
                  | New
              v-card
                v-card-title.pa-4.info
                  span.title.white--text User
                v-card-text
                  v-container
                    v-row
                      v-col(cols='12' sm='6')
                        v-text-field(outlined='' hide-details v-model='editedItem.id' label='Id' disabled)
                      v-col(cols='12' sm='6')
                        v-text-field(outlined hide-details v-model='editedItem.userName' label='Name user')
                      v-col(cols='12' sm='6')
                        v-text-field(outlined hide-details v-model='editedItem.fullName' label='Full name')
                      v-col(cols='12' sm='6')
                        v-select(outlined hide-details :items="listRoles" item-text="name"
          item-value="id" v-model='editedItem.roleId' label='Rol')
                      v-col(cols='12' sm='6')
                        v-text-field(outlined hide-details v-model='editedItem.email' label='Email')
                      v-col(cols='12' sm='6')
                        v-text-field(outlined hide-details v-model='editedItem.address' label='Address')
                    v-row
                      v-col(cols='12' sm='6')
                        v-text-field(outlined hide-details v-model='editedItem.phone' label='Phone')
                      v-col(cols='12' sm='6')
                        v-text-field(outlined hide-details v-model='editedItem.age' label='Age')
                    v-row
                      v-col(cols='12' sm='6')
                        v-text-field(outlined hide-details v-model='editedItem.password' :append-icon="show1 ? 'mdi-eye' : 'mdi-eye-off'"  :type="show1 ? 'text' : 'password'" @click:append="show1 = !show1" label='Password' :rules="[rules.required, rules.min]")
                v-card-actions
                  v-spacer
                  v-btn(color='error' @click='close') Close
                  v-btn(color='success' @click='save') Save
        template(v-slot:item.role='{ item }')
          | {{listRoles.find(x=>x.id==item.roleId).name}}
        template(v-slot:item.actions='{ item }')
          v-icon.mr-2.info--text(small @click='editItem(item)' :disabled="disabledEdit") mdi-pencil
          v-icon.error--text(small @click='deleteItem(item)' :disabled="disabledDelete") mdi-delete
        template(v-slot:no-data)
          v-btn(color='primary' @click='initialize' v-if="currentFilters.length==0") Refresh
          div(v-if="currentFilters.length>0")
          | No data available with filters
        template(v-slot:footer.page-text='items')
          | {{ items.pageStart }} - {{ items.pageStop }} of
          | {{ items.itemsLength }}
</template>

<script lang="ts">
//import { Role } from "@/data/entities/Role";
import { User } from "@/data/entities/User";
import FilterData from "@/data/models/FilterData";
import QueryParameters from "@/data/models/QueryParameters";
import RoleRepository from "@/data/repositories/RoleRepository";
import UserRepository from "@/data/repositories/UserRepository";
import { Component, Vue, Watch } from "vue-property-decorator";
import VueJwtDecode from "vue-jwt-decode";
import MyEncript from "@/service/myEncript";

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

  private rules = {
    required: (value: string) => !!value || "Required.",
    min: (value: string) => value.length >= 8 || "Min 8 characters"
  };

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
    { text: "User", value: "userName" },
    { text: "FullName", value: "fullName", sortable: false },
    { text: "Addres", value: "address", sortable: false },
    { text: "Phone", value: "phone" },
    { text: "Email", value: "email", sortable: false },
    { text: "Age", value: "age", sortable: false },
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

  async initialize(): Promise<void> {
    this.loadingTable = true;
    let res = await this.userRepository.getAll(this.parameters);
    let resRoles = await this.roleRepository.getAll({ currentPage: 0, pageSize: 1000 });
    this.loadingTable = false;
    this.length = res.totalRecords;
    this.list = res.data;
    console.log(this.list);
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

  close(): void {
    this.dialog = false;
  }

  editItem(item: User): void {
    this.editedIndex = this.list.indexOf(item);
    this.editedItem = Object.assign({}, item);
    this.dialog = true;
  }

  async save(): Promise<void> {
    try {
      const myEncript = new MyEncript();
      this.editedItem.password = myEncript.encrypt(this.editedItem.password);
      if (this.editedIndex > -1) {
        //this.editedItem.eTag = "";
        await this.userRepository.update(this.editedItem.id, this.editedItem);
        Object.assign(this.list[this.editedIndex], this.editedItem);
      } else {
        let userSave = await this.userRepository.post(this.editedItem);
        console.log(userSave);
        this.editedItem.id = userSave.id;
        this.list.push(this.editedItem);
      }
    } catch (error) {
      console.log(error);
    }

    this.editedIndex = -1;

    this.close();
  }

  async deleteItem(item: User): Promise<void> {
    this.editedIndex = this.list.indexOf(item);
    if (this.editedIndex > -1) {
      await this.userRepository.delete(item.id);
      this.list.splice(this.editedIndex, 1);
    }
    this.editedIndex = -1;
  }

  changePage(event: number): void {
    this.parameters.currentPage = event;
  }

  changePageSize(event: number): void {
    this.parameters.pageSize = event;
  }

  @Watch("parameters", { deep: true })
  wParameters(): void {
    this.loadfilter(this.currentFilters);
  }

  @Watch("dialog", { deep: true })
  wdialog(value: boolean): void {
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
