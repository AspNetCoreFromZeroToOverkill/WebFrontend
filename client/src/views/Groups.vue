<template>
  <GroupList v-bind:groups="groups" v-on:add="onAdd"/>
</template>
<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import GroupList from '@/components/groups/GroupList.vue';
import { types } from '@/store/modules/groups/actions';
import { State, namespace } from 'vuex-class';
import { GroupSummaryModel } from '@/data/groups/models/group-summary.model';
import { withLoader } from '../shared/loader.functions';

const groupsModule = namespace('groups');

@Component({
  components: {
    GroupList
  }
})
export default class Groups extends Vue {
  @groupsModule.State('groups') private groups!: GroupSummaryModel[];

  public async mounted(): Promise<void> {
    await withLoader(async () =>  await this.$store.dispatch(types.LOAD_GROUPS));
  }

  private async onAdd(group: GroupSummaryModel): Promise<void> {
    await withLoader(async () => await this.$store.dispatch(types.ADD_GROUP, group));
  }
}
</script>
