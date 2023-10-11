import { CreateSignalOptions, Injectable } from "@angular/core";
import { map } from "rxjs/operators"
import { CreatedUserDto, ICreatedUserDto, IUserDto, UserRoleDto, UsersAPIClient } from "../clients/users-api.client";
import { Observable } from "rxjs";
import { User } from "../models/user.model";
import { Page } from "../models/page";
import { CreatedUser } from "../models/createdUser";

@Injectable({
    providedIn: 'root',
  })
  export class UserService {
   
    constructor(
      private warehouseAPIClient: UsersAPIClient
    ) { }

    getUsers(page: number, usersPerPage: number): Observable<Page>{
       var users = this.warehouseAPIClient
        .getAllUsers()
        .pipe(map(({result}) => result.map(user => this.mapToModel(user)))); 

        return this.getPageUsers(users, page, usersPerPage);
    }

    getUser(userId: number): Observable<User>{
      return this.warehouseAPIClient
        .getUser(userId)
        .pipe(map(({result}) => this.mapToModel(result)));
    }

    getCurrentUser(): Observable<User>{
      return this.warehouseAPIClient
        .getCurrentUser()
        .pipe(map(({result}) => this.mapToModel(result)));
    }

    getCurrentUserWithRoles(): Promise<User>{
      return new Promise((resolve) => {
        {
          this.getCurrentUser().subscribe((user) => {
            resolve({
              Age: user.Age,
              Email: user.Email,
              Name: user.Name,
              Roles: user.Roles,
              Id: user.Id
            });
          })
        }
      })
    }

    addUserRole(role: string, userId: number): Observable<User>{
      return this.warehouseAPIClient
        .addUserRole(userId, new UserRoleDto({role}))
        .pipe(map(({result}) => this.mapToModel(result)));
    }

    createUser(newUser: CreatedUser): Observable<User>{
      return this.warehouseAPIClient
        .createUser(new CreatedUserDto(this.mapToCreatedUserDto(newUser)))
        .pipe(map(({result}) => this.mapToModel(result)));
    }

    deleteUser(userId: number): Observable<void>{
      return this.warehouseAPIClient.deleteClient(userId).pipe(map(({}) => {}));
    }

    private getPageUsers(users: Observable<User[]>, page: number, usersPerPage: number): Observable<Page>{
      
      return users.pipe(
        map(u => {
          var startIndex = usersPerPage * (page - 1);
          return new Page(u.length, u.slice(startIndex, startIndex + usersPerPage));
        })
      )
    }

    private mapToModel(dto: IUserDto): User {
        return new User(
          dto.id!,
          dto.name!,
          dto.email!,
          dto.age,
          dto.roles!
        );
    }

    private mapToDto(model: User): IUserDto {
        return {
          id: model.Id!,
          name: model.Name,
          email: model.Email,
          age: model.Age,
          roles: model.Roles
        }
      }

      private mapToCreatedUserDto(user: CreatedUser): ICreatedUserDto{
        return {
          name: user.Name,
          age: user.Age,
          email: user.Email,
          password: user.Password,
          roles: user.Roles
        }
      }
  }