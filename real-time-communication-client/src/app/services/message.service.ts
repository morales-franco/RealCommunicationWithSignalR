import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Message } from '../models/message.model';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  constructor(private _httpClient : HttpClient) { }

  create(message: Message): Observable<any>{
    let url = `${ environment.api }/messages`;
    return this._httpClient.post<any>(url, message);
  }

}
