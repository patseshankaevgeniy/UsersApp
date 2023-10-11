import { User } from "./user.model";

export class Page{
    public totalCount: number;
    public rows: User[];

    constructor(totalCount: number, rows: User[]){
        this.totalCount = totalCount;
        this.rows = rows;
    }
}