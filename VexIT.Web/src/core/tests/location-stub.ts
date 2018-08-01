import {PopStateEvent} from '@angular/common/src/location/location';
import {Injectable} from '@angular/core';

@Injectable()
export class LocationStub {

  path(includeHash?: boolean): string {
    return '';
  }

  isCurrentPathEqualTo(path: string, query?: string): boolean {
    return true;
  }

  normalize(url: string): string {
    return url;
  }

  prepareExternalUrl(url: string): string {
    return url;
  }

  go(path: string, query?: string): void {
  }

  replaceState(path: string, query?: string): void {
  }

  forward(): void {
  }

  back(): void {
  }

  subscribe(onNext: (value: PopStateEvent) => void, onThrow?: ((exception: any) => void) | null, onReturn?: (() => void) | null): Object {
    return new Object();
  }

}
