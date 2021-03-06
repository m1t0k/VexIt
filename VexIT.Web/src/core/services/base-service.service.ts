import {throwError as observableThrowError} from 'rxjs';
import {Injectable} from '@angular/core';
import {AppConfigService} from './app-config.service';
import {HttpClient, HttpHeaders} from '@angular/common/http';

@Injectable()
export class BaseService {

  protected static HttpHeaders = new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8');
  protected apiUrl: string;
  protected baseApiUrl: string;

  constructor(protected http: HttpClient, protected configService: AppConfigService) {
    this.baseApiUrl = configService.getAppConfig().BaseApiUrl;
  }

  public onOperationStart(): void {
  }

  public onOperationEnd(): void {
  }


  protected getFullApiUrl(): string {
    return this.baseApiUrl + this.apiUrl;
  }

  protected handleErrorObservable(error: any) {
    let message = '';
    if (error != null) {
      if (error.error != null) {
        message = 'An error occurred: ' + (error.error.Message == null ? 'Server is unavailable.' : error.error.Message);
      } else if (error.status != null) {
        message = 'Server returned error. Code ' + error.status + '.';
      } else if (error.Message != null) {
        message = 'An error occurred: ' + error.Message;
      }
    }
    return observableThrowError(message || 'Server is unavailable.');
  }
}
