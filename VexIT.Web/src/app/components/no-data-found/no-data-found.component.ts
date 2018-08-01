import {Component, Input, OnInit} from '@angular/core';

@Component({
  selector: 'app-no-data-found',
  templateUrl: './no-data-found.component.html',
  styleUrls: ['./no-data-found.component.css']
})
export class NoDataFoundComponent implements OnInit {

  @Input() visible = false;

  constructor() {
  }

  ngOnInit() {
  }

}
