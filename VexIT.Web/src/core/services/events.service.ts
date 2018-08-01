import {Injectable} from '@angular/core';
import {BaseDataService} from './base-data.service';
import {AppConfigService} from './app-config.service';
import {HttpClient} from '@angular/common/http';
import {EventDto} from '../model/event-dto';

@Injectable({
  providedIn: 'root'
})
export class EventsService extends BaseDataService<EventDto> {

  constructor(http: HttpClient, configService: AppConfigService) {
    super(http, configService);
    this.apiUrl = '/api/1.0/events/';
  }
}
