import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UsersService } from '../services/users.service';

@Component({
  selector: 'app-account-setting',
  templateUrl: './account-setting.component.html',
  styleUrls: ['./account-setting.component.css']
})
export class AccountSettingComponent implements OnInit {

  constructor(private route: ActivatedRoute,
    private router: Router,
    private usersService: UsersService) { }
  
  user = {
    u_Password: '',
    u_Contact:''
  };
  oldpwd: any;
  newpwd: any;
  uid = this.route.snapshot.params['uid'];
  cno: any;

  ngOnInit() {
    this.usersService.getUser(this.uid)
      .subscribe(u => {
        this.user = u;
        console.log(this.user);
        this.cno = this.user.u_Contact;
      });
  }

  settings() {
    console.log(this.user.u_Password);
    if (this.oldpwd != null) {
      if (this.user.u_Password === this.oldpwd) {
        console.log('pwd match', this.user.u_Password, this.oldpwd);
        this.user.u_Password = this.newpwd;
        console.log('final', this.user);
        this.usersService.updateUser(this.uid, this.user)
          .subscribe(u => {
            console.log('updated succesfully', u);
            alert('User Profile updated.!!');
            this.router.navigate(['../../home/', this.uid]);
          });
      }
      else {
        alert("password does not match!! Try Again...");
      }
    }
    else {
      console.log('final', this.user);
      this.usersService.updateUser(this.uid, this.user)
        .subscribe(u => {
          console.log('updated succesfully', u);
          alert('User Profile updated.!!');
          this.router.navigate(['../../home/', this.uid]);
        });
    }  
  }
}
