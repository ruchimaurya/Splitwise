import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class GroupsService {

  constructor(private http: Http) { }
  gid: any;
  getGroupInfo(id:any) {
    return this.http.get('api/groups/info/'+id)
      .map(res => res.json());
  }
  
  getGroupMembers(id: any) {
    return this.http.get('api/friends/' + id)
      .map(res => res.json());
  }
  getIndividualGroupMembers(id: any) {
    return this.http.get('/api/groupmembers/' + id)
      .map(res => res.json());
  }

  GetGroup(gid: any) {
    return this.http.get('/api/groups/' + gid)
      .map(res => res.json());

  }

  CreateGroup(grp: any) {
    return this.http.post('api/groups/',grp)
      .map(res => res.json());
  }

  AddMember(mem: any) {
    return this.http.post('api/groupmembers/', mem)
      .map(res => res.json());
  }

  GetGroupBills(gid: any) {
    return this.http.get('/api/groupbills/' + gid)
      .map(res => res.json());  
  }

  AddGroupBill(gBill: any) {
    gBill.gb_ForGroup = this.gid;
    console.log("service gbill", gBill);
    return this.http.post('/api/groupbills/', gBill)
      .map(res => res.json());  
  }

  GetGroupSettlement(gId: any) {
    gId = this.gid;
    return this.http.get('/api/transactions/groups/settlement/' + gId)
      .map(res => res.json());
  }

  GetBillInfo(bId:any) {
    return this.http.get('/api/groupbills/bill/'+bId)
      .map(r => r.json());
  }

  PostGroupSettlement(gtrans: any) {
    gtrans.t_ReceivedByGroup = this.gid;
    return this.http.post('/api/transactions/', gtrans)
      .map(r => r.json());
  }
}
