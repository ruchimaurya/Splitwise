import { Component, OnInit } from '@angular/core';
import { UsersService } from '../services/users.service';
import { FriendsService } from '../services/friends.service';
import { GroupsService } from '../services/groups.service';
import { Router, ActivatedRoute } from '@angular/router';
import { forEach } from '@angular/router/src/utils/collection';
import { ViewChild, ElementRef } from '@angular/core';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent implements OnInit {

  @ViewChild('closegb') closegb: ElementRef;
  @ViewChild('closeib') closeib: ElementRef;
  @ViewChild('closegs') closegs: ElementRef;
  @ViewChild('closeis') closeis: ElementRef;
  @ViewChild('closefl') closefl: ElementRef;
  @ViewChild('closeas') closeas: ElementRef;

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
  billPayer = "You";
  gmem: any;
  
  gSettle: any;
  iSettle = {
    amount:0,
    fS_iAmount:0,
    fS_Payer: 'User',
    fS_Receiver: 'Friend',
    fS_GSettle:[]
  }
  xamt = 0;
  payerName= [];
  receiverName = [];
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
            console.log('before forloop', this.translist);
            console.log(s[x].gs_Amount);
            if ((s[x].gs_Amount > 0 && s[y].gs_Amount < 0) || (s[x].gs_Amount < 0 && s[y].gs_Amount > 0))
              if (s[x].gs_Amount > s[y].gs_Amount) {
                temptrans.T_PaidBy = s[y].gs_PayerId;
                temptrans.T_ReceivedByFriend = s[x].gs_PayerId;
                temptrans.T_Amount = (s[x].gs_Amount > (s[y].gs_Amount * (-1))) ? (s[y].gs_Amount * (-1)) : s[x].gs_Amount;
                if (this.translist.indexOf(temptrans)<0)
                  this.translist.push(temptrans);
                console.log('in forloop', this.translist);
              }
              else if (s[x].gs_Amount < s[y].gs_Amount) {
                temptrans.T_ReceivedByFriend = s[y].gs_PayerId;
                temptrans.T_PaidBy = s[x].gs_PayerId;
                temptrans.T_Amount = ((s[x].gs_Amount * (-1)) > (s[y].gs_Amount)) ? (s[y].gs_Amount) : (s[x].gs_Amount * (-1));
                if (this.translist.indexOf(temptrans) < 0)
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
    var res = confirm("Want to Logout??");
    if (res) {
      this.router.navigate(['login']);
    }   
  }

  addGroupBill() {
    console.log("Group bill", this.gBill);
    this.groupService.AddGroupBill(this.gBill)
      .subscribe(b => {
        console.log(b);
        this.closegb.nativeElement.click();
        location.reload();
      });
  }

  addFriendBill() {
    console.log(this.billPayer);
    if (this.iBill.Ib_PaidBy != this.uid)
      this.temp.push(this.uid);
    this.iBill.billMembers = this.temp;  
    console.log("friend bill", this.iBill);      
    this.friendService.addFriendsBill(this.iBill)
      .subscribe(b => {
        console.log(b);
        this.closeib.nativeElement.click();
        location.reload();
      });
  }

  GroupSettlement() {
    console.log('on GSET', this.translist[this.i]);
    this.groupService.PostGroupSettlement(this.translist[this.i])
      .subscribe(f => {
        console.log('settle done', f);
        this.closegs.nativeElement.click();        
      });
  }

  GetSetInd() {
    this.friendService.getIndividualSettlementModal(this.uid)
      .subscribe(f => {
        this.iSettle = f;
        console.log('isettle', this.iSettle);
        this.iSettle.amount = (this.iSettle.fS_iAmount > 0) ? this.iSettle.fS_iAmount : this.iSettle.fS_iAmount;
        for (var x in f.fS_GSettle) {
          this.iSettle.amount = this.iSettle.amount + f.fS_GSettle[x].gsM_Amount;
        }
        this.xamt = this.iSettle.amount;
        if (this.iSettle.amount < 0)
          this.iSettle.amount = this.iSettle.amount*(-1)
      });
  }

  GetSetFrd(fname1, fid1) {
    this.friendService.fid = fid1;
    this.friendService.getIndividualSettlement(this.uid, fid1)
      .subscribe(f => {
        this.iSettle = f;
        console.log('isettle', this.iSettle);
        this.iSettle.amount = (this.iSettle.fS_iAmount > 0) ? this.iSettle.fS_iAmount : this.iSettle.fS_iAmount;
        for (var x in f.fS_GSettle) {
          this.iSettle.amount = this.iSettle.amount + f.fS_GSettle[x].gsM_Amount;
        }
        this.xamt = this.iSettle.amount;
        if (this.iSettle.amount < 0)
          this.iSettle.amount = this.iSettle.amount * (-1)
      });
  }

  IndividualSettlement() {
    console.log('IndSet', this.iSettle);
    var sList = this.iSettle.fS_GSettle;
    console.log('sList', sList);
    var amt = this.iSettle.amount;
    if (amt <= 0)
      alert("Payment amount can not be empty");
    else {
      var tList = [];
      var t2 = {
        T_PaidBy: 0,
        T_ReceivedByGroup: null,
        T_ReceivedByFriend: null,
        T_Amount: 0,
      }

      if (this.iSettle.fS_iAmount > 0) {
        amt = amt + this.iSettle.fS_iAmount;
        t2.T_ReceivedByFriend = this.uid;
        t2.T_Amount = (this.iSettle.fS_iAmount > amt) ? amt : this.iSettle.fS_iAmount;
        tList.push(t2);
      }
      else if (this.iSettle.fS_iAmount < 0) {
        amt = amt - this.iSettle.fS_iAmount;
        t2.T_PaidBy = this.uid
        t2.T_Amount = (this.iSettle.fS_iAmount * (-1) > amt) ? amt : this.iSettle.fS_iAmount * (-1);

        tList.push(t2);
      }


      for (var i in sList) {
        var t1 = {
          T_PaidBy: 0,
          T_ReceivedByGroup: null,
          T_ReceivedByFriend: null,
          T_Amount: 0,
        }
        if (amt > 0) {
          if (sList[i].gsM_Amount > 0) {
            t1.T_ReceivedByFriend = this.uid;
            t1.T_Amount = (sList[i].gsM_Amount > amt) ? amt : sList[i].gsM_Amount;
            t1.T_ReceivedByGroup = sList[i].gsM_Gid;
            amt = amt - t1.T_Amount;
            console.log('if t1', t1);
            tList.push(t1);
          }
          else if (sList[i].gsM_Amount < 0) {
            amt = amt - sList[i].gsM_Amount;
            t1.T_PaidBy = this.uid;
            t1.T_Amount = (sList[i].gsM_Amount * (-1) > amt) ? amt : sList[i].gsM_Amount * (-1);
            t1.T_ReceivedByGroup = sList[i].gsM_Gid;

            console.log('else ifif t1', t1);
            tList.push(t1);
          }
        }
      }
      console.log('tList', tList);
      for (var j in tList) {
        this.friendService.postIndividualSettle(tList[j])
          .subscribe(z => {
            alert("Settlement done");
            this.closefl.nativeElement.click();
            this.closeas.nativeElement.click();
            location.reload();
          });
      }
    }
  }

  changePayee(pName: any,pid:any) {
    this.billPayer = pName;
    if (this.billPayer!='You')
      this.iBill.Ib_PaidBy = pid;
    this.temp = [];
  }

  changegbPayee(pName: any, pid: any) {
    console.log(pName, pid);
    this.billPayer = pName;
    this.gBill.gb_PaidBy = pid;
  }

  changegbPayer() {
    this.groupService.getGroupMembersList()
      .subscribe(m => {
        console.log('groupMembers', m);
        this.gmem = m;
      });
  }
}
