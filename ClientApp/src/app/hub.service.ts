import { Injectable, Inject } from '@angular/core';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@aspnet/signalr';
import { environment } from '../environments/environment';
import { Subject } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import {LineData} from './models/lineModel';

@Injectable({
  providedIn: 'root'
})
export class HubService {

  private hubConnection: HubConnection;
  public addLines = new Subject<LineData>();
  public onConnected = new Subject<boolean>();
  private uri: string
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    console.log("baseuri: " +  baseUrl);
    this.baseUri = baseUrl;
    this.setupHub();
  }
  baseUri: string;
  setupHub() {
    this.createConnection();
    this.registerOnServerEvents();
    this.startConnection();
  }

  invoke(command: string, a, b) {
    this.hubConnection.invoke(command, a, b);
  }
  private createConnection() {
    this.uri = this.baseUri + '/logchanges';
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(this.uri)
      .configureLogging(LogLevel.Information)
      .build();
  }
  private startConnection() {
    this.hubConnection
      .start()
      .then(() => {
        console.log('Connection started!');
        this.onConnected.next(true);
      })
      .catch(err => {
        console.error('Error while establishing connection :(');
      });
  }
  private registerOnServerEvents(): void {
    this.hubConnection.on('addlines', (fileId: number, lines: string[]) => {
      this.addLines.next({ fileId: fileId, lines: lines });
    });
  }
}

