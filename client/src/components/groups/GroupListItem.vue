<template>
  <div class="tile is-child notification is-primary">
    <h3 class="title">{{ group.name }}</h3>
    <router-link class="button is-link is-pulled-right" :to="{name: 'group-details', params: {id: group.id}}">Details</router-link>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { GroupSummaryModel } from '@/data/groups/models/';

@Component({
  components: {}
})
export default class GroupListItem extends Vue {
  @Prop() private group!: GroupSummaryModel;
}
</script>