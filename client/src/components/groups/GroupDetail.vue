<template>
  <span v-if="isInEditMode">
    <input v-model="editableGroup.name" placeholder="Enter a name for the group">
    <button v-on:click="save()">Save</button>
    <button v-on:click="discard()">Discard</button>
    <button v-on:click="remove()">Remove</button>
  </span>
  <span v-else>
    {{ group.name }}
    <button v-on:click="edit()">Edit</button>
  </span>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { GroupViewModel } from './models';

@Component({
  components: {}
})
export default class GroupDetail extends Vue {
  @Prop() private group!: GroupViewModel;
  private isInEditMode: boolean = false;
  private editableGroup: GroupViewModel | null = null;

  private edit(): void {
    this.isInEditMode = true;
    this.editableGroup = { ...this.group };
  }
  private save(): void {
      this.$emit('update', this.editableGroup);
      this.discard();
  }

  private discard(): void {
    this.isInEditMode = false;
    this.editableGroup = null;
  }
  private remove(): void {
      this.$emit('remove', this.editableGroup!.id);
  }

}
</script>