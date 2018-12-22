<template>
  <span v-if="creating">
    <input v-model="group.name" placeholder="Enter a name for the group">
    <button v-on:click="save()">Save</button>
    <button v-on:click="discard()">Discard</button>
  </span>
  <button v-else v-on:click="create()">Create new group</button>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { GroupViewModel } from './models';

@Component({})
export default class CreateGroup extends Vue {
  private group: GroupViewModel | null = null;
  private creating: boolean = false;

  private create(): void {
    this.group = { id: 0, name: '', rowVersion: '' };
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