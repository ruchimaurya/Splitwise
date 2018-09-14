import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { UsersService } from '../services/users.service';
import { FriendsService } from '../services/friends.service';
import { ViewChild, ElementRef } from '@angular/core';
import { log } from 'util';

@Component({
  selector: 'app-menu-bar',
  templateUrl: './menu-bar.component.html',
  styleUrls: ['./menu-bar.component.css']
})
export class MenuBarComponent implements OnInit {

  @ViewChild('closeaf') closeaf: ElementRef;

  constructor(private router: Router,
    private route: ActivatedRoute,
    private usersService: UsersService,
    private friendsService: FriendsService) { }

  uid = this.route.snapshot.params['uid'];
  frd = {
    User_Id: this.uid,
    Friend_Id:''
  }
   
  friends: any;
  groups: any;
  fEmail: any;
  display = 'none';

  ngOnInit() {
    //console.log(this.uid);
    console.log("on init");
    this.usersService.getUsersFriends(this.uid)
      .subscribe(f => {
        this.friends = f;    
      });

    this.usersService.getUsersGroups(this.uid)
      .subscribe(f => {
        this.groups = f;      
      });
  }

  addFriend() {
    console.log('add friend', this.fEmail);
    this.usersService.checkMail(this.fEmail)
      .subscribe(u => {
        console.log(u)
        this.frd.Friend_Id = u;
        if (u == this.uid) {
          alert("You are already friend with urself.!!");
        }
        else if (u > 0) {
          this.friendsService.checkFriend(this.uid, u)
            .subscribe(f => {
              if (f > 0)
                alert("You are already Friends.!!");
              else {
                this.friendsService.addFriend(this.frd)
                  .subscribe(r => {
                    console.log("added friend", r);
                    alert(' added as friend.!!');                   
                  });
              }
              this.closeaf.nativeElement.click();
              location.reload();
            });                   
        }
        else {          
         var inv = {
            I_Sender:this.uid,
            I_Email:this.fEmail
          }
          console.log('inv,', inv);
          this.friendsService.sendInvitation(inv)
            .subscribe(i => {
              console.log(i);
              alert('user doesnot exist invitation sent.!!');
              this.closeaf.nativeElement.click();
            });
         
        }
        this.ngOnInit();
      });
    this.display = 'none'; 
  }

}
