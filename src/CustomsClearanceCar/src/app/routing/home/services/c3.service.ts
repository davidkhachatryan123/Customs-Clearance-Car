import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable, catchError, map } from 'rxjs';
import { Car } from '../models';

@Injectable({
  providedIn: 'root'
})
export class C3Service {
  private apiUrl = '/C3';

  constructor(private http: HttpClient) {
    this.apiUrl = environment.api + this.apiUrl;
  }
  
  getMarks(): Observable<string[]> {
    return this.http.get<string[]>(this.apiUrl + '/GetMarks');
  }

  getModels(mark: string): Observable<string[]> {
    return this.http.get<string[]>(this.apiUrl + `/${mark}/GetModels`);
  }

  getEngineCapacities(mark: string, model: string | null): Observable<number[]> {
    return this.http.get<number[]>(this.apiUrl + `/${mark}/${model ? model : 'nil'}/GetEngineCapacities`);
  }

  calculate(car: Car) {
    console.log(car);
    const payload = {
      mark: car.mark,
      year: car.year
    };

    if(car.model) payload['model'] = car.model;
    if(car.engineCapacity) payload['engineCapacity'] = car.engineCapacity;

    return this.http.post(this.apiUrl + '/Calculate', payload)
    .pipe(
      map(result => result),
      catchError(err => {
        console.error(err);
        return [];
      })
    );
  }
}