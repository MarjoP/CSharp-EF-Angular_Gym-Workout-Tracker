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
  //return selected, path:
    return this.http.get(this.baseUrl + 'api/results/' + us+"/" + ex+"/"+co );
  }

  public addResult(Result) {
    return this.http.post(this.baseUrl + 'api/results', Result,  { headers: this.headers });
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

  public addUser(user) {
    return this.http.post(this.baseUrl + 'api/users', user, { headers: this.headers });
  }

  public addExercise(exercise) {
    return this.http.post(this.baseUrl + 'api/exercises', exercise, { headers: this.headers });
  }

  public deleteUser(user) {
    return this.http.delete(this.baseUrl + 'api/users/' +user.userName, user.userName,);
  }
}
