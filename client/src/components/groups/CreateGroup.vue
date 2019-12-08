<template>
  <div class="tile is-child"> 
    <div v-if="creating">
      <div class="field">
        <div class="control"><input type="text" class="input is-primary" v-model="group.name" placeholder="Enter a name for the group"></div>
      </div>
      <div class="field is-grouped">
        <div class="control"><button class="button is-primary" v-on:click="save()">Save</button></div>
        <div class="control"><button class="button is-warning" v-on:click="discard()">Discard</button></div>
      </div>
    </div>
    <div v-else class="control">
      <button class="button" v-on:click="create()">Create new group</button>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { GroupSummaryModel } from '@/data/groups/models';

@Component({})
export default class CreateGroup extends Vue {
  private group: GroupSummaryModel | null = null;
  private creating: boolean = false;

  private create(): void {
    this.group = { id: 0, name: '' };
    this.creating = true;
  }
  private save(): void {
    this.$emit('add', this.group);
    this.discard();
  }

  private discard(): void {
    this.creating = false;
    this.group = null;
  }
}
</script>