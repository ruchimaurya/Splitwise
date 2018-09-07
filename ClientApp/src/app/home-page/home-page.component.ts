import { Component, OnInit } from '@angular/core';
import { UsersService } from '../services/users.service';
import { FriendsService } from '../services/friends.service';
import { GroupsService } from '../services/groups.service';
import { Router, ActivatedRoute } from '@angular/router';
import { forEach } from '@angular/router/src/utils/collection';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent implements OnInit {
   
  constructor(private router: Router,
    private usersService: UsersService,
    private friendService: FriendsService,
    private groupService: GroupsService,
    private route: ActivatedRoute) { }
  i = 0;
  uid = this.route.snapshot.params['uid'];
  fid: any;
  friends: any;
  temp: any;
  gSettle: any;
  payerName= [];
  receiverName= [];
  btnType:'none'
  gBill = {
    gb_Name: '',
    gb_Amount: '',
    gb_PaidBy: this.uid,
    gb_ForGroup: '',
  }
  trans={
    T_PaidBy: 0,
    T_ReceivedByGroup: null,
    T_ReceivedByFriend: null,
    T_Amount: 0,
  }
  translist = [{ T_PaidBy: 0, T_ReceivedByGroup: null, T_ReceivedByFriend: 0, T_Amount: 0 }];


  iBill = {
    Ib_Name: '',
    Ib_Amount: '', 
    Ib_PaidBy: this.uid,
    billMembers:[]
  }
  uName: any;

  ngOnInit() {

    this.usersService.getUser(this.uid)
      .subscribe(u => {
        this.uName = u.u_Name;      
      });

    this.usersService.getUsersFriends(this.uid)
      .subscribe(f => {
        this.friends = f;
        console.log(this.friends);
      });
    
  }
  GetSetGroup() {
    this.translist = [];
    this.groupService.GetGroupSettlement(this.uid)
      .subscribe(s => {
        this.gSettle = s;
        console.log('settle in home', this.gSettle);
        for (var x in s) {
          for (var y in s) {
            var temptrans = {
              T_PaidBy: 0,
              T_ReceivedByGroup: null,
              T_ReceivedByFriend: null,
              T_Amount: 0,
            }
            console.log(s[x].gs_Amount);
            if ((s[x].gs_Amount > 0 && s[y].gs_Amount < 0) || (s[x].gs_Amount < 0 && s[y].gs_Amount > 0))
              if (s[x].gs_Amount > s[y].gs_Amount) {
                temptrans.T_PaidBy = s[y].gs_PayerId;
                temptrans.T_ReceivedByFriend = s[x].gs_PayerId;
                temptrans.T_Amount = (s[x].gs_Amount > (s[y].gs_Amount * (-1))) ? (s[y].gs_Amount * (-1)) : s[x].gs_Amount;
                this.translist.push(temptrans);
                console.log('in forloop', this.translist);
              }
              else if (s[x].gs_Amount < s[y].gs_Amount) {
                temptrans.T_ReceivedByFriend = s[y].gs_PayerId;
                temptrans.T_PaidBy = s[x].gs_PayerId;
                temptrans.T_Amount = ((s[x].gs_Amount * (-1)) > (s[y].gs_Amount)) ? (s[y].gs_Amount) : (s[x].gs_Amount * (-1));
                this.translist.push(temptrans);
                console.log('in forloop', this.translist);
              }           
          }          
        //  console.log('set trans home', this.trans, this.payerName, this.receiverName);
        }
        //for (var t in this.translist) {
        //  var t1: any;
        //  t1 = this.gSettle.map(z => z.gs_PayerId == this.translist[t].)
        //}
        for (var t in this.translist) {
          for (var r in this.gSettle) {
            var t1 = '';
            if (this.translist[t].T_PaidBy == this.gSettle[r].gs_PayerId) {
              t1 = this.gSettle[r].gs_PayerName;
              this.payerName.push(t1);
            }

            else if (this.translist[t].T_ReceivedByFriend == this.gSettle[r].gs_PayerId) {
              t1 = this.gSettle[r].gs_PayerName;
              this.receiverName.push(t1);
            }
              
          }
            
        }
        console.log('set trans home', this.trans, this.payerName, this.receiverName, this.translist);
      });
  }

  Logout() {
    if (confirm("Want to Logout??")) {
      this.router.navigate(['login']);
    }   
  }

  addGroupBill() {
    console.log("Group bill", this.gBill);
    this.groupService.AddGroupBill(this.gBill)
      .subscribe(b => {
        console.log(b);
        alert('bill added');
        //this.router.navigate(['home/', this.uid]);
      //("#AddBillGroup").modal("hide");
      });
  }

  addFriendBill() {
    this.iBill.billMembers = this.temp;
    console.log("friend bill", this.iBill);
    console.log("temp",this.temp);
    this.friendService.addFriendsBill(this.iBill)
      .subscribe(b => {
        console.log(b);
        alert('bill added');
        this.router.navigate(['home/', this.uid]);
      })
  }

  GroupSettlement() {
    console.log('on GSET', this.translist[this.i]);
    this.groupService.PostGroupSettlement(this.translist[this.i])
      .subscribe(f => {
        console.log('settle done', f);
        this.ngOnInit();
      });
  }
}
