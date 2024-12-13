import {inject, Injectable} from '@angular/core';
import {environment} from '@environment';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private readonly baseURL: string = environment.apiURL;
  private readonly http: HttpClient = inject(HttpClient);

  get<T>(partURL: string): Observable<T> {
    return this.http.get<T>(`${this.baseURL}${partURL}`, {});
  }

  post<T>(partURL: string, payload: any, options: { [key: string]: any } = {}): Observable<T> {
    return this.http.post<T>(`${this.baseURL}${partURL}`, payload, options);
  }

  put<T>(partURL:string, payload :any) : Observable<T> {
    return this.http.put<T>(`${this.baseURL}${partURL}`, payload);
  }

  delete<T>(partURL:string):Observable<T> {
    return this.http.delete<T>(`${this.baseURL}${partURL}`);
  }

}
