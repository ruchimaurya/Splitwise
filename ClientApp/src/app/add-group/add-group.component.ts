import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { GroupsService } from '../services/groups.service';

@Component({
  selector: 'app-add-group',
  templateUrl: './add-group.component.html',
  styleUrls: ['./add-group.component.css']
})
export class AddGroupComponent implements OnInit {

  constructor(private router: Router,
    private route: ActivatedRoute,
    private groupsService: GroupsService) { }

  uid = this.route.snapshot.params['uid'];
  friends: any;
  group={
    g_Name: '',
    g_Admin: this.uid
  }
  gmem: any;
  gid: any;

  ngOnInit() {
    console.log(this.uid);

    this.groupsService.getGroupMembers(this.uid)
      .subscribe(f => {
        this.friends = f;
        console.log(f);
      });
  }

  CreateGroup() {
    console.log(this.group, this.gmem);

    this.groupsService.CreateGroup(this.group)
      .subscribe(f => {
        this.gid = f;
        console.log("gid:", f);

        for (var i = 0; i < this.gmem.length ;i++) {
          var mem = {
            gM_GroupId: f,
            gm_Member: this.gmem[i]
          }
          console.log(mem);
          this.groupsService.AddMember(mem)
            .subscribe(f => {
              console.log("mem added",f);
            });
        }
        alert("Group created successfully!!");
        this.router.navigate(['/home/', this.uid]);
      });
  }

}
