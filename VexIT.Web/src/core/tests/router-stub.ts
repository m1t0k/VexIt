
import {of as observableOf} from 'rxjs';
import {NavigationExtras} from '@angular/router/src/router';
import {Injectable} from '@angular/core';


@Injectable()
export class RouterStub {

  readonly url: string = '';

  navigate(commands: any[], extras?: NavigationExtras): Promise<boolean> {
    return observableOf(true).toPromise();
  }

}
