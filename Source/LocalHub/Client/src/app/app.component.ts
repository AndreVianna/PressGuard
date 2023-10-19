import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { DataAggregatorService } from './services/data-aggregator.service';
import { WebSocketService } from './services/websocket.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Local Hub';

  public deviceData: any = null;
  public error: any = null;

  constructor(
    private http: HttpClient,
    private webSocketService: WebSocketService,
    private dataAggregatorService: DataAggregatorService) {
    this.webSocketService.connect(9876);
    this.dataAggregatorService.error.subscribe(error => {
        this.error = error;
      }
    );
  }


  ngOnInit() {
    this.dataAggregatorService.latestData.subscribe(data => {
      this.deviceData = data;
    });
  }
}
