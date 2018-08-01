import {Component, OnInit} from '@angular/core';
import {Router} from '@angular/router';

@Component({
  selector: 'app-top-menu',
  templateUrl: './top-menu.component.html',
  styleUrls: ['./top-menu.component.css']
})
export class TopMenuComponent implements OnInit {

  public tabs: any;

  constructor() {
  }

  ngOnInit() {
    this.setHomeActive();
  }

  public setHomeActive() {
    this.resetTabsState({home: true});
  }

  public setEventsActive() {
    this.resetTabsState({events: true});
  }

  public setAboutActive() {
    this.resetTabsState({about: true});
  }

  public setContactActive() {
    this.resetTabsState({contacts: true});
  }


  private resetTabsState(tabState: any): void {
    this.tabs = Object.assign({
      home: false,
      events: false,
      about: false,
      contacts: false
    }, tabState);
  }

}
