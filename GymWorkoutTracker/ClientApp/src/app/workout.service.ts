import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';



@Injectable({
  providedIn: 'root'
})
export class WorkoutService {
  private headers: HttpHeaders;
  private baseUrl: string;;


  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string)
  {
    this.baseUrl = baseUrl;
  }

  public getResults() {
    // Get all data
    return this.http.get(this.baseUrl + 'api/results');
  }
  public getSelectedResults(us, ex, co) {
    //return this.http.get(this.baseUrl +'api/results/selected");
    //tähän yhteys controlleriin: esim. return this.http.get(this.baseUrl +'api/results&user="us"&exercise="ex"&count");
    return this.http.get(this.baseUrl + 'api/results/' + us+"/" + ex+"/"+co );
  }

  public add(result) {
    return this.http.post(this.baseUrl + 'api/results', result, { headers: this.headers });
  }
  public remove(result) {
    return this.http.delete(this.baseUrl + 'api/results/' + result.id, { headers: this.headers });
  }

  public update(result) {
    return this.http.put(this.baseUrl + 'api/results/' + result.id, result, { headers: this.headers });
  }

  public getUsers() {
    //Get all users
    return this.http.get(this.baseUrl + 'api/users');
  }

  public getExercises() {
    //Get all exercises
    return this.http.get(this.baseUrl + 'api/exercises');
  }
}
