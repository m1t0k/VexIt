import {TestBed, inject} from '@angular/core/testing';

import {ComponentCoreService} from './component-core.service';
import {HttpClient, HttpHandler} from '@angular/common/http';
import {AppConfigService} from './app-config.service';
import {ActivatedRoute, Router} from '@angular/router';
import {RouterStub} from '../tests/router-stub';
import {ActivatedRouterStub} from '../tests/activated-router-stub';


describe('ComponentCoreService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [HttpClient, HttpHandler, AppConfigService, ComponentCoreService,
        {provide: Router, useClass: RouterStub},
        {provide: ActivatedRoute, useValue: ActivatedRouterStub.Value}
      ]
    });
  });

  it('should be created', inject([ComponentCoreService], (service: ComponentCoreService) => {
    expect(service).toBeTruthy();
  }));
});
