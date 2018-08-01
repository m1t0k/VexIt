export class PagedResult<T> {
  public Items: Array<T>;
  public TotalCount: number;
  public TotalPages: number;

  constructor() {
    this.Items = [];
    this.TotalPages = 0;
    this.TotalCount = 0;
  }
}
