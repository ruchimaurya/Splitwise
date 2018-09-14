import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { HomePageComponent } from './home-page/home-page.component';
import { MenuBarComponent } from './menu-bar/menu-bar.component';
import { UsersService } from './services/users.service';
import { DashboardComponent } from './dashboard/dashboard.component';
import { GroupsComponent } from './groups/groups.component';
import { FriendsComponent } from './friends/friends.component';
import { ActivityComponent } from './activity/activity.component';
import { GroupsService } from './services/groups.service';
import { FriendsService } from './services/friends.service';
import { AddGroupComponent } from './add-group/add-group.component';
import { ExpensesComponent } from './expenses/expenses.component';
import { AccountSettingComponent } from './account-setting/account-setting.component';
import { EditGroupComponent } from './edit-group/edit-group.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,  
    LoginComponent,
    RegisterComponent,
    HomePageComponent,
    MenuBarComponent,   
    DashboardComponent,
    GroupsComponent,
    FriendsComponent,
    ActivityComponent,
    AddGroupComponent,
    ExpensesComponent,
    AccountSettingComponent,
    EditGroupComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    HttpModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: LoginComponent, pathMatch: 'full' },
      { path: 'addgroup/:uid', component: AddGroupComponent },
    
      { path: 'login', component: LoginComponent },
      {
        path: 'home/:uid', component: HomePageComponent,
        children: [
          { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
          { path: 'dashboard', component: DashboardComponent },
          { path: 'groups/:gid', component: GroupsComponent },
          { path: 'friends/:fid', component: FriendsComponent },
          { path: 'expenses', component: ExpensesComponent },
          { path: 'activity', component: ActivityComponent }           
        ]
      },
      { path: 'registration', component: RegisterComponent },
      { path: ':uid/groups/:gid/edit', component: EditGroupComponent },
      { path: 'settings/:uid', component: AccountSettingComponent },    
      { path: '**', component: LoginComponent }
    ])
  ],
  providers: [UsersService, GroupsService, FriendsService],
  bootstrap: [AppComponent]
})
export class AppModule { }
