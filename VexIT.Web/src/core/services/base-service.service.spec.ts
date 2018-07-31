import { TestBed, inject } from '@angular/core/testing';

import { BaseService } from './base-service.service';
import {HttpClient, HttpHandler} from '@angular/common/http';
import {AppConfigService} from './app-config.service';
import {ActivatedRoute, Router} from '@angular/router';
import {RouterStub} from '../tests/router-stub';
import {ActivatedRouterStub} from '../tests/activated-router-stub';

describe('BaseServiceService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [HttpClient, HttpHandler,  AppConfigService, BaseService,
        {provide: Router, useClass: RouterStub},
        {provide: ActivatedRoute, useValue: ActivatedRouterStub.Value}]
    });
  });

  it('should be created', inject([BaseService], (service: BaseService) => {
    expect(service).toBeTruthy();
  }));
});
