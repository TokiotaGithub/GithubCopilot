import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { Beer } from './beer';


@Injectable({
  providedIn: 'root',
  
})
export class BeersService {
  // private apiUrl = 'assets/beers.json'; // Replace with your API URL
  private apiUrl = 'http://localhost:5132'; // Replace with your API URL

  constructor(private http: HttpClient) { }

  getBeers(): Observable<Beer[]> {
    return this.http.get<Beer[]>(`${this.apiUrl}/Cerveza?limit=100`);
  }

  getBeer(id: number): Observable<Beer> {   
    return this.http.get<Beer>(`${this.apiUrl}/Cerveza/${id}`);
  }

  addBeer(beer: Beer): Observable<Beer> {
    return this.http.post<Beer>(this.apiUrl, beer);
  }

  updateBeer(beer: Beer): Observable<Beer> {
    return this.http.put<Beer>(`${this.apiUrl}/${beer.id}`, beer);
  }

  deleteBeer(id: number): Observable<Beer> {
    return this.http.delete<Beer>(`${this.apiUrl}/${id}`);
  }
}