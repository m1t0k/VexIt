import {Injectable} from '@angular/core';
import {from as observableFrom} from 'rxjs';

@Injectable()
export class ActivatedRouterStub {
  public static Value: any = {
    data: observableFrom([{company: 'COMPANY'}]),
    params: observableFrom([{tab: 0}]),
    url: observableFrom([{company: 'COMPANY'}]),
    queryParams: observableFrom([{isNew: false}]),
    snapshot: {
      url: [
        {
          path: 'login',
        }, {
          path: 'foo',
        },
        {
          path: 'bar',
        },
        {
          path: 'home',
        },
      ],
    },
  };
}
