import {Injectable} from '@angular/core';
import {Observable, of as observableOf} from 'rxjs';
import {PagedResult} from '../system/paged-result';
import {EventDto} from '../model/event-dto';

@Injectable()
export class EventsServiceStub {

  private eventList = [{Name: 'Event1', Place: 'Place1', ScheduledAt: new Date()},
    {Name: 'Event2', Place: 'Place2', ScheduledAt: new Date()}];


  public getItems(pageIndex?: number, pageSize?: number, orderBy?: string): Observable<PagedResult<EventDto>> {
    return observableOf(Object.assign(new PagedResult<EventDto>(), {Items: this.eventList, TotalCount: 2, TotalPages: 1}));
  }

}
