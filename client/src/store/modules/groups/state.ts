import { GroupSummaryModel } from '@/data/groups/models/group-summary.model';
import { GroupDetailsModel } from '@/data/groups/models/group-details.model';

export interface GroupsState {
    groups: GroupSummaryModel[];
    groupDetails: GroupDetailsModel | null;
}
