export class CreatedUser {
    constructor(
        public Id: number,
        public Name: string,
        public Email: string,
        public Password: string,
        public Age: number,
        public Roles: string[]
    ){ }
}