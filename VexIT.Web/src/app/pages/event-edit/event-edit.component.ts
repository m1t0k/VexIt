import {AfterViewInit, Component, OnInit} from '@angular/core';
import {EventDto} from '../../../core/model/event-dto';
import {Observable, of as observableOf} from 'rxjs';
import {EventCategory} from '../../../core/model/event-category.enum';
import {EventsService} from '../../../core/services/events.service';
import {AppConfigService} from '../../../core/services/app-config.service';
import {ActivatedRoute, Router} from '@angular/router';
import {flatMap} from 'rxjs/operators';

@Component({
  selector: 'app-event-edit',
  templateUrl: './event-edit.component.html',
  styleUrls: ['./event-edit.component.css'],
  providers: [AppConfigService, EventsService]
})
export class EventEditComponent implements OnInit, AfterViewInit {

  public eventItem = new EventDto();
  public id = '';

  //todo fix later - move to control
  public day: any;
  public month: any;
  public year: any;
  public hours: any = 0;
  public minutes: any = 0;

  constructor(private service: EventsService, protected route: ActivatedRoute, protected router: Router) {
  }

  ngOnInit() {
    this.getData();
  }

  ngAfterViewInit(): void {
    this.route.queryParams.subscribe(queryParams => {
      if (queryParams['isNew'] === 'true') {
        //this.componentCoreService.sendAlert(AlertType.Succcess, 'Item was successfully created.');
        alert('Event created.');
      }
    });
  }


  public getData(): void {
    this.route.params.pipe(
      flatMap(params => {
          this.id = params['id'] || this.getDefaultId();
          return this.getItem();
        }
      )).subscribe(d => {
      if (this.eventItem.ScheduledAt != null) {
        this.initEventDateVars();
      }
    }, err => console.log(err));

  }

  private initEventDateVars() {
    const date = new Date(this.eventItem.ScheduledAt);
    this.year = date.getFullYear();
    this.month = date.getMonth() + 1;
    this.day = date.getDate();
    this.hours = date.getHours();
    this.minutes = date.getMinutes();

    console.log(date);
    console.log(this.year);
    console.log(this.month);
    console.log(this.day);
  }

  public getItem(): Observable<any> {
    return Observable.create((observer) => {
      if (this.id.length > 0) {
        this.service.getItem(this.id).subscribe(res => {
          this.eventItem = res;
          observer.next();
          observer.complete();
        }, error => {
          observer.error(error);
          observer.complete();
        });
      } else {
        this.createNew();
        observer.next();
        observer.complete();
      }
    });
  }

  public saveData(): void {
    this.eventItem.ScheduledAt = this.buildEventDate();
    console.log(this.eventItem.ScheduledAt);
    if (this.id.length <= 0) {
      this.service.createItem(this.eventItem)
        .subscribe(res => {
            this.eventItem = res;
            this.id = res.Id;
            this.router.navigate(['/events/edit', this.id],
              {queryParams: {isNew: true}});
          },
          error => {
            alert(error);
          });
    } else {
      this.service.updateItem(this.eventItem)
        .subscribe(res => {
            alert('Event saved.');
          },
          error => {
            alert(error);
          });
    }
  }


  protected getDefaultId(): string {
    return '';
  }

  protected createNew(): void {
    this.eventItem = new EventDto();
  }


  public setDescription(event: any) {

  }

  public getEventCategoryList(): Observable<any[]> {
    const list: any[] = [];
    for (const enumMember in EventCategory) {
      list.push(enumMember);
    }
    return observableOf(list.sort());
  }

  public getEventCategoryName(val: EventCategory): string {
    return EventCategory[val];
  }

  private buildEventDate(): Date {
    return new Date(this.year, this.month - 1, this.day, this.hours, this.minutes);
  }

  public goToEventList() {
    this.router.navigate(['/events']);
  }
}
