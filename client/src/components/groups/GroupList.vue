<template>
  <ul>
    <li v-for="group in groups" v-bind:key="group.id">
      <GroupDetail v-bind:group="group" v-on:update="onUpdate" v-on:remove="onRemove"/>
    </li>
    <li><CreateGroup v-on:add="onAdd"/></li>
  </ul>
</template>
<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import GroupDetail from './GroupDetail.vue';
import CreateGroup from './CreateGroup.vue';
import { GroupViewModel } from '@/components/groups/models';

@Component({
  components: {
    GroupDetail,
    CreateGroup
  }
})
export default class GroupList extends Vue {
  @Prop() private groups!: GroupViewModel[];

  private onUpdate(group: GroupViewModel): void {
    this.$emit('update', group);
  }

  private onRemove(groupId: number): void {
    this.$emit('remove', groupId);
  }

  private onAdd(group: GroupViewModel): void {
    this.$emit('add', group);
  }
}
</script>
