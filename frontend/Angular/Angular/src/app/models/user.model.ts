export class User {
    constructor(
        public Id: number,
        public Name: string,
        public Email: string,
        public Age: number,
        public Roles: string[]
    ){ }
}