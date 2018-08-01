import {Component, EventEmitter, Input, OnDestroy, OnInit, Output} from '@angular/core';
import {Subscription} from 'rxjs';
import {ComponentCoreService} from '../../../core/services/component-core.service';

@Component({
  selector: 'app-sorted-column',
  templateUrl: './sorted-column.component.html',
  styleUrls: ['./sorted-column.component.css'],
  providers: [ComponentCoreService]
})
export class SortedColumnComponent implements OnInit, OnDestroy {

  public Title: string;
  private ascSortDirection?: boolean;
  private sortChangedSubscription: Subscription;
  @Input() ColumnName: string;
  @Input() SortByName: string;
  @Output() onSort: EventEmitter<string> = new EventEmitter<string>();


  constructor(private coreService: ComponentCoreService) {
  }

  ngOnInit() {
    this.sortChangedSubscription = ComponentCoreService.SortColumnSubject.subscribe(
      value => {
        this.sortColumnChanged(value);
      }, err => console.log(err)
    );

    this.ascSortDirection = null;
    this.Title = this.ColumnName;
    if (this.SortByName == null || this.SortByName.length <= 0) {
      this.SortByName = this.ColumnName;
    }
  }

  ngOnDestroy() {
    if (this.sortChangedSubscription != null) {
      this.sortChangedSubscription.unsubscribe();
    }
  }

  public sortColumnChanged(columnName: string): void {
    if (columnName !== this.ColumnName) {
      this.ascSortDirection = null;
    }
    this.setTitle();
  }

  public changeSortDirection(): void {
    if (this.ascSortDirection === null) {
      this.ascSortDirection = true;
    } else {
      this.ascSortDirection = !this.ascSortDirection;
    }

    let ascDesc = 'desc';
    if (this.ascSortDirection === true) {
      ascDesc = 'asc';
    }

    this.onSort.emit(this.SortByName + ' ' + ascDesc);
    this.coreService.changeSortColumn(this.ColumnName);
    this.setTitle();
  }

  private setTitle(): void {
    let iconPart = '';
    if (this.ascSortDirection === true) {
      iconPart = '<i class="fa fa-chevron-down" aria-hidden="true"></i>&nbsp;';
    }
    if (this.ascSortDirection === false) {
      iconPart = '<i class="fa fa-chevron-up" aria-hidden="true"></i>&nbsp;';
    }

    this.Title = iconPart + this.ColumnName;
  }
}
