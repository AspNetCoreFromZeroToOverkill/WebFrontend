<template>
  <div v-if="editableGroup != null" class="container">
    <h1 class="title">{{editableGroup.name}}</h1>
    <div class="columns">
      <div class="column">Group created by {{editableGroup.creator.name}}</div>
      <div class="column">
        <section class="section">
          <label for="name">Name:</label>
          <input
            name="name"
            v-model="editableGroup.name"
            v-bind:readonly="!isInEditMode"
            placeholder="Enter a name for the group"
          />
        </section>
      </div>
    </div>
    <section class="section">
      <div class="field is-grouped is-pulled-right">
        <template v-if="isInEditMode">
          <div class="control">
            <button class="button is-danger" v-on:click="remove()">Remove</button>
          </div>
          <div class="control">
            <button class="button is-warning" v-on:click="reset()">Discard</button>
          </div>
          <div class="control">
            <button class="button is-primary" v-on:click="save()">Save</button>
          </div>
        </template>
        <template v-else>
          <button class="button is-primary" v-on:click="edit()">Edit</button>
        </template>
      </div>
    </section>
  </div>
  <div v-else>Group not found!</div>
</template>
<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { types } from '@/store/modules/groups/actions';
import { State, namespace } from 'vuex-class';
import { GroupDetailsModel } from '@/data/groups/models';
import { NavigationGuard } from 'vue-router';
import { withLoader } from '../shared/loader.functions';

const groupsModule = namespace('groups');

@Component
export default class GroupDetails extends Vue {
  @Prop() private id!: number;
  @groupsModule.State('groupDetails') private group!: GroupDetailsModel | null;
  private isInEditMode: boolean = false;
  private editableGroup: GroupDetailsModel | null = null;

  public mounted(): void {
    this.reset();
  }

  private edit(): void {
    this.isInEditMode = true;
    this.editableGroup = { ...this.group! };
  }

  private async save(): Promise<void> {
    await withLoader(
      async () =>
        await this.$store.dispatch(types.UPDATE_GROUP, this.editableGroup)
    );
    this.reset();
  }

  private reset(): void {
    this.isInEditMode = false;
    this.editableGroup = { ...this.group! };
  }

  private async remove(): Promise<void> {
    await withLoader(
      async () =>
        await this.$store.dispatch(types.REMOVE_GROUP, this.editableGroup!.id)
    );
    this.$router.push({ path: '/groups' });
  }
}
</script>
