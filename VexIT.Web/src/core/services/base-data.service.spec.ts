import {TestBed, inject} from '@angular/core/testing';
import {BaseDataService} from './base-data.service';
import {BaseDto} from '../model/base-dto';
import {HttpClient, HttpHandler} from '@angular/common/http';
import {AppConfigService} from './app-config.service';
import {ActivatedRoute, Router} from '@angular/router';
import {ActivatedRouterStub} from '../tests/activated-router-stub';
import {RouterStub} from '../tests/router-stub';


describe('BaseDataService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [HttpClient, HttpHandler, AppConfigService, BaseDataService,
        {provide: Router, useClass: RouterStub},
        {provide: ActivatedRoute, useValue: ActivatedRouterStub.Value}]
    });
  });

  it('should be created', inject([BaseDataService], (service: BaseDataService<BaseDto>) => {
    expect(service).toBeTruthy();
  }));
});
