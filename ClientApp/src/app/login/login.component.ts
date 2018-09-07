import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UsersService } from '../services/users.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private router: Router,
    private usersService: UsersService) { }

  user = {
    email: '',
    password: ''
  };


  ngOnInit() {
  }

  openRegistration() {
    this.router.navigate(['registration']);
  }

  login() {  
    console.log("inside login",this.user);
    this.usersService.login(this.user)
      .subscribe(x => {
        console.log(x);
        if (x > 0)
          this.router.navigate(['/home/', x])
        else
          alert('Invalid email or password');               
      });

  }
}
