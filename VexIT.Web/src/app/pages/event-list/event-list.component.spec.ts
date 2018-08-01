import {async, ComponentFixture, fakeAsync, TestBed} from '@angular/core/testing';
import {EventListComponent} from './event-list.component';
import {NoDataFoundComponent} from '../../components/no-data-found/no-data-found.component';
import {PaginationComponent} from '../../components/pagination/pagination.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {EventsService} from '../../../core/services/events.service';
import {AppConfigService} from '../../../core/services/app-config.service';
import {HttpClient, HttpHandler} from '@angular/common/http';
import {ComponentCoreService} from '../../../core/services/component-core.service';
import {RouterStub} from '../../../core/tests/router-stub';
import {ActivatedRoute, Router} from '@angular/router';
import {ActivatedRouterStub} from '../../../core/tests/activated-router-stub';
import {SortedColumnComponent} from '../../components/sorted-column/sorted-column.component';
import {EventsServiceStub} from '../../../core/tests/events-service-stub';

describe('EventListComponent', () => {
  let component: EventListComponent;
  let fixture: ComponentFixture<EventListComponent>;
  let service: EventsService;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [ReactiveFormsModule, FormsModule],
      providers: [HttpClient, HttpHandler, AppConfigService, ComponentCoreService, EventsServiceStub,
        {provide: Router, useClass: RouterStub},
        {provide: ActivatedRoute, useValue: ActivatedRouterStub.Value}
      ],

      declarations: [NoDataFoundComponent, PaginationComponent, SortedColumnComponent, EventListComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EventListComponent);
    component = fixture.componentInstance;
    service = fixture.debugElement.injector.get(EventsService);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it(`Check that initially No Data Found is visible, but table and paginator hidden.`, fakeAsync(() => {
    const compiled = fixture.debugElement.nativeElement;
    expect(compiled.querySelector('#noDataFound')).not.toBeNull();
    expect(compiled.querySelector('#pagination')).toBeNull();
    expect(compiled.querySelector('#eventsTable')).toBeNull();

  }));

  it(`Should load 2 events`, fakeAsync(() => {
    spyOn(service, 'getItems').and.returnValues(EventsServiceStub.getItems());
    component.ngOnInit();
    fixture.detectChanges();
    expect(component.data.TotalCount).toEqual(2);
  }));
});
