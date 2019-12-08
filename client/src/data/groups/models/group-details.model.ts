import { GroupCreatorModel } from './group-creator.model';

export interface GroupDetailsModel {
    id: number;
    name: string;
    rowVersion: string;
    creator: GroupCreatorModel;
}
