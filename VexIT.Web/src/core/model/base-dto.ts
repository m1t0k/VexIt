export class BaseDto {
  public Id: string;

  public isIdSet(): boolean {
    return this.Id != null && this.Id.length > 0;
  }
}
