import {catchError, finalize} from 'rxjs/operators';
import {Injectable} from '@angular/core';
import {Response} from '../system/response';
import {BaseService} from './base-service.service';
import {AppConfigService} from './app-config.service';
import {BaseDto} from '../model/base-dto';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {PagedResult} from '../system/paged-result';


@Injectable()
export class BaseDataService<T extends BaseDto> extends BaseService {

  constructor(http: HttpClient, configService: AppConfigService) {
    super(http, configService);
  }

  public createEmpty(): T {
    return {} as T;
  }


  public getItems(pageIndex?: number, pageSize?: number, orderBy?: string): Observable<PagedResult<T>> {
    this.onOperationStart();
    return this.http
      .get<PagedResult<T>>(this.getFullApiUrl() + '?pageIndex=' + pageIndex + '&pageSize=' + pageSize + '&orderBy=' + orderBy).pipe(
        catchError(this.handleErrorObservable),
        finalize(this.onOperationEnd));
  }

  public searchItems(query: T, pageIndex?: number, pageSize?: number, orderBy?: string): Observable<PagedResult<T>> {
    this.onOperationStart();
    return this.http
      .post<PagedResult<T>>(this.getFullApiUrl() + '/search?pageIndex=' + pageIndex + '&pageSize=' + pageSize + '&orderBy=' + orderBy,
        JSON.stringify(query), {headers: BaseService.HttpHeaders}).pipe(
        catchError(this.handleErrorObservable),
        finalize(this.onOperationEnd));
  }

  public getItem(id: string): Observable<T> {
    this.onOperationStart();
    return this.http
      .get<T>(this.getFullApiUrl() + '/' + id, {headers: BaseService.HttpHeaders}).pipe(
        catchError(this.handleErrorObservable),
        finalize(this.onOperationEnd));
  }


  public createItem(item: T): Observable<T> {
    this.onOperationStart();
    return this.http
      .post<T>(this.getFullApiUrl(), JSON.stringify(item), {headers: BaseService.HttpHeaders}).pipe(
        catchError(this.handleErrorObservable),
        finalize(this.onOperationEnd));
  }

  public updateItem(item: T): Observable<Response> {
    this.onOperationStart();
    return this.http
      .put<Response>(this.getFullApiUrl() + '/' + item.Id, JSON.stringify(item), {headers: BaseService.HttpHeaders}).pipe(
        catchError(this.handleErrorObservable),
        finalize(this.onOperationEnd));
  }

  public deleteItem(id: string): Observable<Response> {
    this.onOperationStart();
    return this.http
      .delete<Response>(this.getFullApiUrl() + '/' + id, {headers: BaseService.HttpHeaders}).pipe(
        catchError(this.handleErrorObservable),
        finalize(this.onOperationEnd));
  }
}
