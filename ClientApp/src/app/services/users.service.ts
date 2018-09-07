import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class UsersService {
   
  constructor(private http: Http) { }

  login(user: any) {
    return this.http.get('/api/users/login/' + user.email+'/'+user.password)
      .map(res => res.json());
  }

  getUser(uid: any) {
    return this.http.get('api/users/' + uid)
      .map(res=>res.json());
  }

  checkMail(email: any) {
    return this.http.get('/api/users/getid/'+email)
      .map(res=>res.json());
  
    //return this.http.post('/api/users/',user)
    //  .map(res => res.json());
  }

  userRegistration(user: any) {
   
    return this.http.post('/api/users/',user)
      .map(res => res.json());
  }

  getUsersFriends(id: any) {
    return this.http.get('/api/friends/'+id)
      .map(res => res.json());
  }

  getUsersGroups(id: any) {
    return this.http.get('/api/usersgroups/' + id)
      .map(res => res.json());
  }

  updateUser(uid: any, user: any) {
    return this.http.put('/api/users/'+uid, user)
      .map(res => res.json());
  }

  getUsersActivity(uid:any) {
    return this.http.get('api/users/activity/'+uid)
      .map(res => res.json());
  }

  getUsersExpenses(uid: any) {
    return this.http.get('api/users/transactions/' + uid)
      .map(res => res.json());
  }

 
}
