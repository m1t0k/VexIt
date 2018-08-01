import {Component, OnInit} from '@angular/core';
import {EventsService} from '../../../core/services/events.service';
import {PagedResult} from '../../../core/system/paged-result';
import {EventDto} from '../../../core/model/event-dto';
import {AppConfigService} from '../../../core/services/app-config.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-event-list',
  templateUrl: './event-list.component.html',
  styleUrls: ['./event-list.component.css'],
  providers: [AppConfigService, EventsService]
})
export class EventListComponent implements OnInit {
  private pageSize = 20;
  public data: PagedResult<EventDto> = new PagedResult<EventDto>();
  private orderBy: string = '';

  constructor(private service: EventsService, private router: Router) {
  }

  ngOnInit() {
    console.log('getting data');
    this.getData();
  }

  private getData() {
    this.service.getItems(1, this.pageSize).subscribe(
      data => {
        console.log('completed');
        this.data = data;
        this.pageSize=1;
      },
      err => {
        // only for demo
        alert('Error occurred: unable to get event list.');
      }
    );
  }

  public onSort(orderBy: string): void {
    this.orderBy = orderBy;
    this.getData();
  }

  public isTableVisible(): boolean {
    return this.data.Items.length > 0;
  }

  public isNotFoundVisible(): boolean {
    return this.data == null || this.data.Items == null || this.data.Items.length <= 0;
  }

  public deleteItem(id: string): void {
    if (!confirm('Are you sure you want to delete this item?')) {
      return;
    }
    this.service.deleteItem(id).subscribe(result => {
        this.getData();
      },
      error => {
        // only for demo
        alert('Error occurred: unable to get event list.');
      });
  }

  public editItem(id: string): void {
    this.router.navigate(['/events/edit', id], {});
  }

  public createItem(): void {
    this.router.navigate(['/events/new']);
  }
}
