import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { UsersService } from '../services/users.service';
import { FriendsService } from '../services/friends.service';
import { forEach } from '@angular/router/src/utils/collection';

@Component({
  selector: 'app-expenses',
  templateUrl: './expenses.component.html',
  styleUrls: ['./expenses.component.css']
})
export class ExpensesComponent implements OnInit {

  constructor(private route: ActivatedRoute,
    private usersService: UsersService,
    private friendService: FriendsService ) { }

  uid = this.route.parent.snapshot.params['uid'];
  expense: any;
  friends: any;
  total= 0;
  ngOnInit() {
    this.usersService.getUsersExpenses(this.uid)
      .subscribe(e => {
        this.expense = e;
        console.log('exp', this.expense);
      });

    this.usersService.getUsersFriends(this.uid)
      .subscribe(f => {
        this.friends = f;
        console.log('friends', this.friends);
        for (var i in this.friends) {
          this.friendService.getIndividualSettlement(this.uid, this.friends[i].fm_Id)
            .subscribe(x => {
              for (var j in x.fS_GSettle) {
                this.total = this.total + x.fS_GSettle[j].gsM_Amount;
              }
              this.total = this.total + x.fS_iAmount;
              console.log('total:1', this.total);
            });
        }
      });
  }

}
