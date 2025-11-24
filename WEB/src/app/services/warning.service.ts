import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class WarningService {
  /*private apiUrl = 'https://api.spidermancity.com/warnings';
  constructor(private http: HttpClient) { }

  getWarnings(): Observable<Warning[]> {
    return this.http.get<Warning[]>(this.apiUrl);
  }*/
}
