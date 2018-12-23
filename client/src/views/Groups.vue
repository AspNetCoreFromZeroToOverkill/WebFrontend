<template>
  <GroupList v-bind:groups="groups" v-on:update="onUpdate" v-on:remove="onRemove" v-on:add="onAdd"/>
</template>
<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import { Getter, Action } from 'vuex-class';
import GroupList from '@/components/groups/GroupList.vue';
import { GroupViewModel } from '@/components/groups/models';
import { actionTypes } from '@/store/modules/groups/actions';
import { getterTypes } from '@/store/modules/groups/getters';

const namespace: string = 'groups';

@Component({
  components: {
    GroupList
  }
})
export default class Groups extends Vue {
  @Getter(getterTypes.GROUPS, { namespace }) private groups!: GroupViewModel[];
  @Action(actionTypes.LOAD_GROUPS, { namespace })
  private loadGroups!: () => void;
  @Action(actionTypes.ADD_GROUP, { namespace }) private onAdd!: (
    group: GroupViewModel
  ) => void; // duck typing FTW! - GroupViewModel has the same format as Group
  @Action(actionTypes.UPDATE_GROUP, { namespace }) private onUpdate!: (
    group: GroupViewModel
  ) => void;
  @Action(actionTypes.REMOVE_GROUP, { namespace }) private onRemove!: (
    groupdId: number
  ) => void;

  public mounted() {
    // fetch data as soon as component is mounted
    this.loadGroups();
  }
}
</script>
