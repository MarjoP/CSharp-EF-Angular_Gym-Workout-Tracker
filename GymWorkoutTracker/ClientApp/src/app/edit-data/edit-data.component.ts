import { Component, OnInit } from '@angular/core';
import { WorkoutService } from '../workout.service';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-edit-data',
  templateUrl: './edit-data.component.html',
  styleUrls: ['./edit-data.component.css']
})
export class EditDataComponent  {

  public selectedUser: any = { userName: "" };
  public selectedExercise: any = { exerciseName: "" }
  public selectedResult: any;

  public results: any;
  public users: any;
  public exercises: any;

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
    this.selectedUser.userName = filterVal;
  }

  selExercise(filterVal: any) {
    this.selectedExercise.exerciseName = filterVal;
  }

  updateUserList(): void {
    this.woService.getUsers().subscribe(result => {
      this.users = result;
    }, error => console.error(error));
  }

  updateExerciseList(): void {
    this.woService.getExercises().subscribe(result => {
      this.exercises = result;
    }, error => console.error(error));
  }

  editUserName() {

  }

  editExerciseName() {

  }

  deleteUserName() {
    this.woService.deleteUser(this.selectedUser).subscribe(result => {
      Swal.fire("User deleted...");
      this.updateUserList();
    }, error => Swal.fire("Could not delete user"));
  }

  deleteExerciseName() {

  }
  deleteResult() {

  }




  

}
