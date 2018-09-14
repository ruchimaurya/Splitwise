import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { GroupsService } from '../services/groups.service';

@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.css']
})
export class GroupsComponent implements OnInit {

  constructor(private router: Router,
    private route: ActivatedRoute,
    private groupsService: GroupsService) { }

  uid = this.route.parent.snapshot.params['uid'];
  gid = this.route.snapshot.params['gid'];  
  group: any;
  gBills: any;
  bInfo: any;
  gTrans: any;
  settle: any;
  btList=[];
  ngOnInit() {
   // console.log(this.uid, this.gid);
    this.groupsService.gid = this.gid;
  //  console.log("gid", this.groupsService.gid);

    this.groupsService.getGroupInfo(this.gid)
      .subscribe(g => {
        this.group = g;       
         //    console.log(g);  
      });

    this.groupsService.GetGroupsAllTransactions(this.gid)
      .subscribe(b => {
        this.gTrans = b;
        console.log('Group trans', this.gTrans);
      });


    this.groupsService.GetGroupSettlement(this.gid)
      .subscribe(s => {
        this.settle = s;
     //   console.log("settle",this.settle);
      });

  }

  AddBill() {
    this.groupsService.gid = this.route.snapshot.params['gid'];
    console.log("gid", this.groupsService.gid);
  }
  GetSettle() {
    this.groupsService.gid = this.route.snapshot.params['gid'];
  }

  BillInfo(bId:any) {
    console.log('bill info',bId.value);
   
  }

}
