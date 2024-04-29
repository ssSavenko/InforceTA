import {
  OnInit,
  Component
} from '@angular/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html'
})
export class HeaderComponent {
  public isSetUser: Boolean = false;
  public usersName?: String

  ngOnInit() {
    this.usersName = localStorage.getItem('usersName')!;
    this.isSetUser = this.IsUserNameExists();
    if (this.isSetUser) {
      this.usersName = this.usersName.substring(1, this.usersName.length - 1)!;
    }
  }

  constructor() {
    window.addEventListener('storage', () => {
      this.usersName = String(localStorage.getItem('usersName'))!;
      this.isSetUser = this.IsUserNameExists();
      if (this.isSetUser) {
        this.usersName = this.usersName.substring(1, this.usersName.length - 1)!;
      }
    });
  }

  private IsUserNameExists(): Boolean {
    return this.usersName == undefined || this.usersName == null ? false : true;
  }

}
