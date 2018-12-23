export interface Group {
    id: number;
    name: string;
    rowVersion: string;
}

export interface GroupsState {
    groups: Group[];
}
