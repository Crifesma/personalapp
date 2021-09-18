<template lang="pug">
	div(style="flex-basis: 20%")
		//- v-text-field(:style="`width:${widthText}px;`" outlined clearable disabled v-model="txtViewFilter" label="Filtro" type="text" )
		//- 	template(v-slot:prepend)
		//- 		v-tooltip(bottom)
		//- 			template(v-slot:activator="{ on }")
		//- 				v-icon(v-on="on") mdi-magnify
		v-dialog#filter-modal(transition="dialog-bottom-transition" v-model='dialogFilter'   max-width="600")
			template(v-slot:activator='{ on }')
				v-btn(color='primary' dark class="mb-2 text-capitalize" v-on='on')
					v-icon mdi-magnify
					| Filtrar
			template( v-slot:default="dialog")
				v-card
					v-toolbar(color="primary" dark) Filtros
					v-card-text
						template(v-for='(column, index) in columnsTable')
							template(v-if='column != undefined')
								template(v-if='typeInput(column.value) == 1')
									v-text-field( :label="column.text + ':'" type='text' placeholder='...' :value='getFilter(column.field)' @input='setFilter(column.value, $event)')
								//- template(v-if='typeInput(column.value) == 2')
								//- 	v-menu(v-model='menu2' :close-on-content-click='false' :nudge-right='40' transition='scale-transition' offset-y='' min-width='auto')
								//- 		template(v-slot:activator='{ on, attrs }')
								//- 			v-text-field(:value='getFilter(column.value)' label='Picker without buttons' prepend-icon='mdi-calendar' readonly='' v-bind='attrs' v-on='on')
								//- 			v-date-picker(:value='getFilter(column.value)' @input='setFilter2(column.value, $event)')
								template(v-if='typeInput(column.value) == 3')
									v-select(:items="listCountry" item-text="name" item-value="alpha3Code" :value='getFilter(column.field)' @input='setFilter(column.value, $event)')
					v-card-actions( class="justify-end")
						v-btn(@click='setColumns') Filtrar
						v-btn(@click='reset') Reiniciar filtro
						v-btn(@click='dialogFilter = false') Cerrar
		span(v-if="txtViewFilter.length>0").ml-4 Filtros: {{txtViewFilter}}
</template>

<script lang="ts">
import { Component, Vue, Prop, Emit } from "vue-property-decorator";
import IRepository from "@/data/IRepository";
import FilterData from "@/data/models/FilterData";
import QueryParameters from "@/data/models/QueryParameters";

@Component({})
export default class inputFilter extends Vue {
  @Prop({
    type: String
  })
  readonly nameModuleStore!: string;

  @Prop({
    type: Array
  })
  readonly columnsTable!: Array<any>;

  dialogFilter = false;
  menu2 = false;
  txtFind = "";
  filters: Array<FilterData> = [];
  filtersTemp: Array<any> = [];
  widthText = 210;
  txtViewFilter = "";
  optionState = [
    { name: "Ninguno", field: "" },
    { name: "Activo", field: "True" },
    { name: "Inactivo", field: "False" }
  ];
  parameters: QueryParameters = { currentPage: 1, pageSize: 10 };

  get serverParams() {
    return "cxosas"; //this.$store.getters[this.nameModuleStore + "/serverParams"];
  }

  getFilter(name: string) {
    if (this.filters.find(f => f.nameColumn == name) != undefined)
      return this.filters.find(f => f.nameColumn == name)?.dataForFind;
    return "";
  }

  setFilter(name: string, value: string) {
    let shearData = this.filters.find(f => f.nameColumn == name);
    if (shearData != undefined) {
      shearData.dataForFind = value;
      return;
    }
    this.filters.push({ nameColumn: name, dataForFind: value });
  }
  setFilter2(name: string, value: string) {
    let shearData = this.filters.find(f => f.nameColumn == name);
    if (shearData != undefined) {
      shearData.dataForFind = value;
      return;
    }
    this.filters.push({ nameColumn: name, dataForFind: value });
    this.menu2 = false;
  }

  @Emit("filter")
  find(): FilterData[] {
    return this.filters;
  }

  setColumns() {
    this.filters = this.filters.filter(f => f.dataForFind != "");
    this.txtViewFilter = this.filters
      .map(filter => {
        let label = this.columnsTable.find(f => f.value == filter.nameColumn).text;
        if (filter.dataForFind != "") return `${label}:${filter.dataForFind}`;
      })
      .join(",");
    this.widthText = 150 + this.txtViewFilter.length * 6.5;
    this.find();
    this.dialogFilter = false;
  }

  openFilter() {
    // this.$bvModal.show("filter-modal");
  }

  reset() {
    this.filters = [];
  }
  typeInput(name: string) {
    if (name == "alpha3Code") return 3;
    if (name.toUpperCase().indexOf("DATE") == -1) return 1;
    if (name.toUpperCase().indexOf("DATE") != -1) return 2;
  }
}
</script>
