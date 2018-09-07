import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { UsersService } from '../services/users.service';

@Component({
  selector: 'app-activity',
  templateUrl: './activity.component.html',
  styleUrls: ['./activity.component.css']
})
export class ActivityComponent implements OnInit {

  constructor(private route: ActivatedRoute,
    private usersService: UsersService) { }

  uid = this.route.parent.snapshot.params['uid'];
  activity: any;

  ngOnInit() {
    console.log('uid',this.uid);
    this.usersService.getUsersActivity(this.uid)
      .subscribe(a => {
        this.activity = a;
      //  console.log('activity', this.activity);
      });
  }

}
