import {BaseDto} from './base-dto';

export class EventDto extends BaseDto {
  public Name: string;
  public Country: string;
  public City: string;
  public Street: string;
  public Place: string;
  public Description: string;
  public YouTubeUrl: string;
  public CategoryId: number;
  public ScheduledAt: Date;
}
