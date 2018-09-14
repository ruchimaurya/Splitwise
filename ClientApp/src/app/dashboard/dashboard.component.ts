import { Component, OnInit } from '@angular/core';
import { UsersService } from '../services/users.service';
import { FriendsService } from '../services/friends.service';
import { GroupsService } from '../services/groups.service';
import { Router, ActivatedRoute } from '@angular/router';
import { forEach } from '@angular/router/src/utils/collection';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  constructor(private router: Router,
    private usersService: UsersService,
    private friendService: FriendsService,
    private groupService: GroupsService,
    private route: ActivatedRoute) { }

  uid = this.route.parent.snapshot.params['uid'];
  friends: any;
  frdSet: any;
  i = 0; j = 0;
  iFsList = [];
  frdList = [];
  aPay=0;
  aRec = 0;
  total = 0;
  ngOnInit() {    
    this.usersService.getUsersFriends(this.uid)
      .subscribe(f => {
        this.friends = f;
        console.log('friends', this.friends);
        for (var i in this.friends) {
          this.friendService.getIndividualSettlement(this.uid, this.friends[i].fm_Id)
            .subscribe(x => {
              this.frdSet = x;
              this.frdList.push(x);
              console.log('frdlist', this.frdList);
              var temp = 0;
              for (var j in x.fS_GSettle)
              {                
                temp = x.fS_GSettle[j].gsM_Amount + temp;
            //    console.log('temp',temp);
                this.total = this.total + x.fS_GSettle[j].gsM_Amount;
                if (x.fS_GSettle[j].gsM_Amount < 0) {
                  this.aPay = this.aPay + x.fS_GSettle[j].gsM_Amount * (-1);
             //     console.log('apvna', x.fS_GSettle[j].gsM_Amount);
                }
                 
                else if (x.fS_GSettle[j].gsM_Amount > 0){
                  this.aRec = this.aRec + x.fS_GSettle[j].gsM_Amount;
             //     console.log('levana', x.fS_GSettle[j].gsM_Amount);
                }               
              }
              if (x.fS_iAmount < 0) {
                this.aPay = this.aPay + x.fS_iAmount * (-1);               
              }
              else if (x.fS_iAmount > 0) {
                this.aRec = this.aRec + x.fS_iAmount;                
              }
              this.total = this.total + x.fS_iAmount;
           //   console.log('final temp', temp, x.fS_iAmount);
              temp = temp + x.fS_iAmount;
              this.iFsList.push(temp);
          //    console.log('ifsList', this.iFsList);
            });
        
        }
      });
  }

  gotoFwindow(fName: any) {
    console.log(this.friends[fName]);
    var c = this.friends.findIndex(s => s.fm_Name == fName);
    
    console.log(c);
    this.router.navigate(['home/' + this.uid+'/friends/', this.friends[c].fm_Id]);
  }

}
