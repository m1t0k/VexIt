import {Injectable} from '@angular/core';
import {BehaviorSubject} from 'rxjs';

@Injectable()
export class ComponentCoreService {

  public static SortColumnSubject: BehaviorSubject<string> = new BehaviorSubject<string>('');

  constructor() {
  }

  public changeSortColumn(name: string): void {
    ComponentCoreService.SortColumnSubject.next(name);
  }
}
