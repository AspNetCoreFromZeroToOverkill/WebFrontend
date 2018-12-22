<template>
  <GroupList v-bind:groups="groups" v-on:update="onUpdate" v-on:remove="onRemove" v-on:add="onAdd" />
</template>
<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import GroupList from '@/components/groups/GroupList.vue';
import { GroupViewModel } from '@/components/groups/models';

@Component({
  components: {
    GroupList
  }
})
export default class Groups extends Vue {
  private currentId: number = 0;
  private groups: GroupViewModel[] = [
    { id: ++this.currentId, name: 'Sample Group', rowVersion: 'aaa' },
    { id: ++this.currentId, name: 'Another Sample Group', rowVersion: 'bbb' }
  ];

  private onUpdate(group: GroupViewModel): void {
    const index = this.groups.findIndex(g => g.id === group.id);
    this.groups = [...this.groups.slice(0, index), group, ...this.groups.slice(index + 1, this.groups.length)];
  }

  private onRemove(groupId: number): void {
    this.groups = this.groups.filter(g => g.id !== groupId);
  }

  private onAdd(group: GroupViewModel): void {
    group.id = ++this.currentId;
    this.groups = [...this.groups, group];
  }
}
</script>
