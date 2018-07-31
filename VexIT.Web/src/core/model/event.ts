import {BaseDto} from './base-dto';

export class Event extends BaseDto {
  public Name: string;
  public Place: string;
  public ScheduledAt: Date;
}
