import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { WorkoutService } from '../workout.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public results: any;
  public users: any;
  public exercises: any;

  public selectedUser: string;
  public selectedExercise: string;
  public quantity: number = -1;

  constructor(private router: Router, private woService: WorkoutService) {
    this.woService.getResults().subscribe(result => {
      this.results = result;
    }, error => console.error(error));

    this.woService.getUsers().subscribe(result => {
      this.users = result;
    }, error => console.error(error));

    this.woService.getExercises().subscribe(result => {
      this.exercises = result;
    }, error => console.error(error));
  }


  selUser(filterVal: any) {
    this.selectedUser = filterVal;
  }

  selExercise(filterVal: any) {
    this.selectedExercise = filterVal;
  }
  
  public fetchSorted() {
  
    this.woService.getSelectedResults(this.selectedUser, this.selectedExercise, this.quantity).subscribe(result => {
      this.results = result;
      console.log(this.selectedUser);
      console.log(this.selectedExercise);
      console.log(this.quantity);
      console.log(this.results);
    }, error => console.error(error));
  }
}

