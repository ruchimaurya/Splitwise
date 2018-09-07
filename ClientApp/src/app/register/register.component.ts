import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UsersService } from '../services/users.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(private route: ActivatedRoute,
    private router: Router,
    private usersService: UsersService) { }
 
  user = {
    U_Name:'',
    U_Email: '',
    U_password: ''
  };

  ngOnInit() {
  }

  goToLogin() {
    this.router.navigate(['login']);
  }

  Register() {
    console.log(this.user);
    this.usersService.checkMail(this.user.U_Email)
      .subscribe(x => {
        console.log(x);
        if (x > 0) {
          alert("Email id already exist. Try with another");         
        }
        else {
          this.usersService.userRegistration(this.user)
            .subscribe(y => {
              console.log(y);
              alert("Registration successful!! Login to continue..");
              this.router.navigate(['login']);
            });
        }
        
      });
  }

}
