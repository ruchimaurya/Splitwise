import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FriendsService } from '../services/friends.service';

@Component({
  selector: 'app-friends',
  templateUrl: './friends.component.html',
  styleUrls: ['./friends.component.css']
})
export class FriendsComponent implements OnInit {

  constructor(private router: Router,
    private route: ActivatedRoute,
    private friendService: FriendsService) { }

  uid = this.route.parent.snapshot.params['uid'];
  fid = this.route.snapshot.params['fid'];
  friend: any;
  trans: any;
  fSettle: any;
  fAmount: 0;

  ngOnInit() {
    this.friendService.getFriend(this.fid)
      .subscribe(f => {
        this.friend = f;
        console.log(this.friend);
      });

    this.friendService.getFriendsTransaction(this.uid, this.fid)
      .subscribe(f => {
        this.trans = f;
        console.log('trans',this.trans);
      });

    this.friendService.getIndividualSettlement(this.uid, this.fid)
      .subscribe(f => {
        this.fSettle = f;
        console.log('FSEttle', this.fSettle);
        this.fAmount = this.fSettle.fS_GSettle.map(j => j.gsM_Amount).reduce((sum, current) => sum + current);
        this.fAmount = this.fAmount + this.fSettle.fS_iAmount;
        console.log('famount', this.fAmount);
      });
  }

}
