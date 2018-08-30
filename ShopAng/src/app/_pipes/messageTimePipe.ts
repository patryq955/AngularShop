import { Observable } from "rxjs/Observable";
import { AsyncPipe } from "../../../node_modules/@angular/common";
import { Pipe, ChangeDetectorRef } from "../../../node_modules/@angular/core";

@Pipe({
  name: "messageTime",
  pure: false
})
export class MessageTimePipe extends AsyncPipe {
  value: Date;
  transform(obj: any, args?: any[]): any {
    try {
      this.value = new Date(obj);
      return this.SetText();
    } catch (e) {
      return "";
    }
  }

  private SetText() : string {
        var result: string;
        // current time
        let now = new Date().getTime();

        // time since message was sent in seconds
        let delta = (now - this.value.getTime()) / 1000;

        // format string
        if (delta < 10) {
          result = "dostępny";
        } else if (delta < 60) {
          // sent in last minute
          result = Math.floor(delta) + " sekund";
        } else if (delta < 3600) {
          // sent in last hour
          result = Math.floor(delta / 60) + " minut";
        } else if (delta < 86400) {
          // sent on last day
          result = Math.floor(delta / 3600) + " godzin";
        } else {
          // sent more than one day ago
          result = "vor " + Math.floor(delta / 86400) + " dni";
        }
        return result;
      
  }
}
