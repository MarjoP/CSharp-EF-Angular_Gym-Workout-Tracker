import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { WorkoutService } from '../workout.service';
import { Router } from '@angular/router';
import { extractStyleUrls } from '@angular/compiler/src/style_url_resolver';


@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html',
  styleUrls: ['./fetch-data.component.scss']
})
export class FetchDataComponent {
  public results: any;
  public users: any;
  public exercises: any;

  public selectedUser: string;
  public selectedExercise: string;
  public quantity: number = -1;

  public show: boolean;
  public graphVisible: boolean;

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
      this.show = true;
    }, error => console.error(error));
  }
  
  public hideResults() {
    this.show = false;
  }

  createGraph() {
    this.getMax();
    this.show = true;
    this.graphVisible = true;
  }

  public getMax() {
    this.woService.getMaxResults(this.selectedUser, this.selectedExercise, this.quantity).subscribe(result => {
      this.results = result;
      console.log(this.results);
      this.show = true;
    }, error => console.error(error));
  }
  
}
