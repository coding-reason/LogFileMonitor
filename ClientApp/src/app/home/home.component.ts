import { Component } from '@angular/core';
import {HubService} from '../hub.service';
import {LineData} from '../models/lineModel';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  /*constructor(@Inject('BASE_URL') baseUrl: string, hs: HubService) {
    hs.baseUri = baseUrl;
    }
    */

  lineData: LineData[];
  constructor(hubService: HubService) {
    hubService.addLines.subscribe(a => {
      this.lineData.push(a);
    });

  }
}
