import {Injectable} from '@angular/core';
import {AppConfig} from '../system/app-config';
import {environment} from '../../environments/environment';

@Injectable()
export class AppConfigService {

  private AppConfig: AppConfig = new AppConfig();

  constructor() {
    this.AppConfig = new AppConfig();
    this.AppConfig.BaseApiUrl = environment.baseApiUrl;
  }

  public getAppConfig(): AppConfig {
    return this.AppConfig;
  }

}
