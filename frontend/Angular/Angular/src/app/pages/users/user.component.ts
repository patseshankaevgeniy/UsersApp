import { Component, OnInit} from '@angular/core';
import { User } from 'src/app/models/user.model';
import { UserService } from 'src/app/services/user.service';


@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss'],
})
export class UserComponent implements OnInit{
  
  public usersPerPage: number = 3;
  public page: number = 1;
  public collectionSize: number = 0;
  public users: User[] = [];
  public canAdd: boolean = false

  constructor(private readonly userService: UserService){}

  ngOnInit(): void {
    this.loadUsers();
  }
  
  addNewUser( ){

  }

  onChangeClick(){

  }

  onAddUserRole(role: string, userId: number){
    this.userService.addUserRole(role, userId).subscribe(()=> {});
  }

  onDeleteClick(userId: number){
    this.userService.deleteUser(userId).subscribe(() =>
    (this.users = this.users.filter(
      (p) => p.Id != userId
    ))
  );
  }

  onPageChanged(pageNumber: number){
    this.loadUsers();
  }

  private loadUsers(){
    this.userService
      .getUsers(this.page, this.usersPerPage)
      .subscribe((page) => (this.users = page.rows, this.collectionSize = page.totalCount))
  }
}
