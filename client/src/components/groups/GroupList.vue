<template>
  <div class="container">
    <div class="tile is-ancestor is-vertical">
      <div class="tile is-parent">
        <div class="tile is-parent is-4" >
          <CreateGroup v-on:add="onAdd"/>
        </div>
      </div>
      <div class="tile is-parent" v-for="row in getGroupsMatrix(3)" v-bind:key="getRowKey(row)">
        <div class="tile is-parent is-4" v-for="group in row" v-bind:key="group.id">
          <GroupListItem v-bind:group="group" />
        </div>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import GroupListItem from './GroupListItem.vue';
import CreateGroup from './CreateGroup.vue';
import { GroupSummaryModel } from '../../data/groups/models/group-summary.model';

@Component({
  components: {
    GroupListItem,
    CreateGroup
  }
})
export default class GroupList extends Vue {
  @Prop() private groups!: GroupSummaryModel[];

  private getGroupsMatrix(groupsPerRow: number): GroupSummaryModel[][] {
    if (this.groups == null) {
      return [];
    }

    const matrix: GroupSummaryModel[][] = [];
    for (let i = 0; i < this.groups.length; i++) {
      const row = Math.floor(i / groupsPerRow);
      if (i % groupsPerRow === 0) {
        matrix.push([]);
      }
      matrix[row].push(this.groups[i]);
    }

    return matrix;
  }

  private getRowKey(row: GroupSummaryModel[]): string {
    return row.map(g => g.id).join('|');
  }

  private onAdd(group: GroupSummaryModel): void {
    this.$emit('add', group);
  }
}
</script>
