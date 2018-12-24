<template>
  <GroupList v-bind:groups="groups" v-on:update="onUpdate" v-on:remove="onRemove" v-on:add="onAdd"/>
</template>
<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import GroupList from '@/components/groups/GroupList.vue';
import { GroupViewModel } from '@/components/groups/models';
import { types } from '@/store/modules/groups/actions';
import { State, namespace } from 'vuex-class';

const groupsModule = namespace('groups');

@Component({
  components: {
    GroupList
  }
})
export default class Groups extends Vue {
  @groupsModule.State('groups') private groups!: GroupViewModel[];
  @groupsModule.Action('loadGroups') private loadGroups!: () => void;

  public mounted(): void {
    // this.$store.dispatch(types.LOAD_GROUPS); // another way of doing the same
    this.loadGroups();
  }

  private onUpdate(group: GroupViewModel): void {
    this.$store.dispatch(types.UPDATE_GROUP, group);
  }

  private onRemove(groupId: number): void {
    this.$store.dispatch(types.REMOVE_GROUP, groupId);
  }

  private onAdd(group: GroupViewModel): void {
    this.$store.dispatch(types.ADD_GROUP, group);
  }
}
</script>
