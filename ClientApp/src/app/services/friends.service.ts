import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';


@Injectable()
export class FriendsService {

  constructor(private http: Http) { }
  fid: any;
  getFriend(fid: any) {
    return this.http.get("/api/friend/" + fid)
      .map(res => res.json());
  }

  getFriendsTransaction(uid: any, fid: any) {
    return this.http.get("api/transactions/friends/" + uid+"/"+fid)
      .map(res => res.json());
  }

  addFriendsBill(bModel: any) {
    console.log("aFBILL", bModel);
    return this.http.post("/api/individualbills/", bModel)
      .map(res => res.json());
  }

  addFriend(frd: any) {
    console.log('add frd', frd);
    return this.http.post("api/friends", frd)
      .map(res => res.json());
  }

  checkFriend(uid: any, fid: any) {
    return this.http.get("api/friends/" + uid + '/' + fid)
      .map(res => res.json());
  }

  sendInvitation(inv: any) {
    console.log('inv service',inv);
    return this.http.post("api/friends/invite", inv)
      .map(res => res.json());
  }

  getIndividualSettlement(uid: any, fid: any) {
    return this.http.get('api/transactions/friends/settlement/' + uid + '/' + fid)
      .map(res => res.json());
  }
  getIndividualSettlementModal(uid: any) {
    var fid = this.fid;
    return this.http.get('api/transactions/friends/settlement/' + uid + '/' + fid)
      .map(res => res.json());
  }

  postIndividualSettle(trans: any) {
    if (trans.T_ReceivedByFriend == null)
      trans.T_ReceivedByFriend = this.fid;
    else if (trans.T_PaidBy == 0)
      trans.T_PaidBy = this.fid;
    console.log('fs model', trans);
    return this.http.post('/api/transactions/', trans)
      .map(res => res.json());
  }

  getFriendsBills(uid: any, fid: any) {
    return this.http.get('api/friendsbills/' + uid + '/' + fid)
      .map(res => res.json());
  }

  RemoveFriend(uid: any, fid: any) {
    return this.http.delete('api/friends/' + uid + '/' + fid)
      .map(res => res.json());
  }
}
