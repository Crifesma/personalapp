<template lang="pug">
	div(style="flex-basis: 20%")
		//- v-text-field(:style="`width:${widthText}px;`" outlined clearable disabled v-model="txtViewFilter" label="Filtro" type="text" )
		//- 	template(v-slot:prepend)
		//- 		v-tooltip(bottom)
		//- 			template(v-slot:activator="{ on }")
		//- 				v-icon(v-on="on") mdi-magnify
		v-dialog#filter-modal(transition="dialog-bottom-transition" v-model='dialogFilter'   max-width="600")
			template(v-slot:activator='{ on }')
				v-btn(color='primary' class="mb-2 text-capitalize" v-on='on' :disabled="disabled")
					v-icon mdi-magnify
					| Filter
			template( v-slot:default="dialog")
				v-card
					v-toolbar(color="primary") Filters
					v-card-text
						template(v-for='(column, index) in columnsTable')
							template(v-if='column != undefined')
								template(v-if='typeInput(column.value) == 1')
									v-text-field( :label="column.text + ':'" type='text' placeholder='...' :value="getFilter(column.value)" @input='setFilter(column.value, $event)')
								//- template(v-if='typeInput(column.value) == 2')
								//- 	v-menu(v-model='menu2' :close-on-content-click='false' :nudge-right='40' transition='scale-transition' offset-y='' min-width='auto')
								//- 		template(v-slot:activator='{ on, attrs }')
								//- 			v-text-field(:value='getFilter(column.value)' label='Picker without buttons' prepend-icon='mdi-calendar' readonly='' v-bind='attrs' v-on='on')
								//- 			v-date-picker(:value='getFilter(column.value)' @input='setFilter2(column.value, $event)')
								template(v-if='typeInput(column.value) == 3')
									v-select(:items="listCountry" item-text="name" item-value="alpha3Code" :value='getFilter(column.value)' @input='setFilter(column.value, $event)')
					v-card-actions( class="justify-end")
						v-btn(@click='setColumns') Filter
						v-btn(@click='reset') Reset filters
						v-btn(@click='dialogFilter = false') Close
		span(v-if="txtViewFilter.length>0").ml-4 Filters: {{txtViewFilter}}
</template>

<script lang="ts">
import { Component, Vue, Prop, Emit } from "vue-property-decorator";
import IRepository from "@/data/IRepository";
import FilterData from "@/data/models/FilterData";
import QueryParameters from "@/data/models/QueryParameters";

interface IColumn {
  text: string;
  value: string;
}

@Component({})
export default class inputFilter extends Vue {
  @Prop({
    type: String
  })
  readonly nameModuleStore!: string;

  @Prop({
    type: Boolean
  })
  readonly disabled!: boolean;

  @Prop({
    type: Array
  })
  readonly columnsTable!: Array<IColumn>;

  dialogFilter = false;
  menu2 = false;
  txtFind = "";
  filters: Array<FilterData> = [];
  filtersTemp: Array<FilterData> = [];
  widthText = 210;
  txtViewFilter = "";
  optionState = [
    { name: "Ninguno", field: "" },
    { name: "Activo", field: "True" },
    { name: "Inactivo", field: "False" }
  ];
  parameters: QueryParameters = { currentPage: 1, pageSize: 10 };

  // get serverParams() {
  //   return "cxosas"; //this.$store.getters[this.nameModuleStore + "/serverParams"];
  // }

  getFilter(name: string): string {
    if (this.filters == []) return "";
    const value = this.filters.find(f => f.nameColumn == name);
    if (value == undefined) return "";
    return value.dataForFind;
  }

  setFilter(name: string, value: string): void {
    let shearData = this.filters.find(f => f.nameColumn == name);
    if (shearData != undefined) {
      shearData.dataForFind = value;
      return;
    }
    this.filters.push({ nameColumn: name, dataForFind: value });
  }

  setFilter2(name: string, value: string): void {
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

  setColumns(): void {
    this.filters = this.filters.filter(f => f.dataForFind != "");
    this.txtViewFilter = this.filters
      .map(filter => {
        const value = this.columnsTable.find(f => f.value == filter.nameColumn);
        if (value == undefined) return "";
        let label = value.text;
        if (filter.dataForFind != "") return `${label}:${filter.dataForFind}`;
      })
      .join(",");
    this.widthText = 150 + this.txtViewFilter.length * 6.5;
    this.find();
    this.dialogFilter = false;
  }

  openFilter(): void {
    // this.$bvModal.show("filter-modal");
  }

  reset(): void {
    this.filters = [];
    this.setColumns();
  }
  typeInput(name: string): number {
    if (name == "alpha3Code") return 3;
    if (name.toUpperCase().indexOf("DATE") == -1) return 1;
    if (name.toUpperCase().indexOf("DATE") != -1) return 2;
    return 0;
  }
}
</script>
