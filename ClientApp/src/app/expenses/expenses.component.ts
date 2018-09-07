import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { UsersService } from '../services/users.service';
import { forEach } from '@angular/router/src/utils/collection';

@Component({
  selector: 'app-expenses',
  templateUrl: './expenses.component.html',
  styleUrls: ['./expenses.component.css']
})
export class ExpensesComponent implements OnInit {

  constructor(private route: ActivatedRoute,
    private usersService: UsersService) { }

  uid = this.route.parent.snapshot.params['uid'];
  expense: any;

  ngOnInit() {
    this.usersService.getUsersExpenses(this.uid)
      .subscribe(e => {
        this.expense = e;
        console.log('exp', this.expense);
      });
  }

}
