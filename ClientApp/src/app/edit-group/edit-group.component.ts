import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { GroupsService } from '../services/groups.service';

@Component({
  selector: 'app-edit-group',
  templateUrl: './edit-group.component.html',
  styleUrls: ['./edit-group.component.css']
})
export class EditGroupComponent implements OnInit {

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private groupsService: GroupsService) { }

  uid = this.route.snapshot.params['uid'];
  gid = this.route.snapshot.params['gid'];
  group: any;
  gmem: any;
  settlement: any;
  friend: any;
  selfrd: any;
  tmem: any;
  ngOnInit() {
    this.groupsService.GetGroup(this.gid)
      .subscribe(g => {
        this.group = g;
     //   console.log(this.group);
      });
    this.groupsService.getGroupMembers(this.uid)
      .subscribe(f => {
        this.friend = f;
       // console.log(this.friend);
      });

    this.groupsService.getIndividualGroupMembers(this.gid)
      .subscribe(g => {
        this.gmem = g;
      //  console.log('gmem',this.gmem);
        this.tmem = g.map(x => x.m_Name);
        //console.log('fmem',this.tmem);
      });
  }

  DeleteGroup() {
    var temp = confirm("Are you sure!!Are you ABSOLUTELY sure you want to delete this group? This will remove this group for ALL users involved, not just yourself.");
    if(temp)
    this.router.navigate(['../../../../home/', this.uid]);
  }

  RemoveMember(mid:any) {
    this.groupsService.GetGroupSettlement(this.gid)
      .subscribe(s => {
        this.settlement = s;
       // console.log(this.settlement, mid);
        var amount = s.find(x => x.gs_PayerId == mid).gs_Amount;
       // console.log('amounr', amount);
        if (amount != 0)
          alert('A person must zero out their account (i.e. have a balance of $0.00) before he or she can be removed.');
        else
          var cnf=confirm('Are you sure you want to remove this person from this group?');
      });
  }

  EditGroup() {
    console.log('Edit');
  }

}
