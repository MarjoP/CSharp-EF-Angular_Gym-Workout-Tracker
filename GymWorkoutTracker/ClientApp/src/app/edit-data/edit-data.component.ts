import { Component, OnInit } from '@angular/core';
import { WorkoutService } from '../workout.service';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-edit-data',
  templateUrl: './edit-data.component.html',
  styleUrls: ['./edit-data.component.css']
})
export class EditDataComponent {

  public selectedUser: any = { userName: "" };
  public selectedExercise: any = { exerciseName: "" };
  public selectedResult: any = { id: "" };

  public results: any;
  public users: any;
  public exercises: any;

  constructor(private router: Router, private woService: WorkoutService) {
    this.updateUserList();
    this.updateExerciseList();
    this.updateResultList();
  }

  selUser(filterVal: any) {
    this.selectedUser.userName = filterVal;
  }

  selExercise(filterVal: any) {
    this.selectedExercise.exerciseName = filterVal;
  }

  selResult(filterVal: any) {
    this.selectedResult.id = filterVal;
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
  updateResultList(): void {
    this.woService.getSelectedResults('allUsers', 'allExercises', 20).subscribe(result => {
      this.results = result;
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
    }, error => Swal.fire("Oops! Something went wrong. Could not delete user."));
  }

  deleteExerciseName() {
    this.woService.deleteExercise(this.selectedExercise).subscribe(result => {
      Swal.fire("Exercise deleted...");
      this.updateExerciseList();
    }, error => Swal.fire("Oops! Something went wrong. Could not delete exercise."));
  }

  deleteResult() {
    this.woService.removeResult(this.selectedResult).subscribe(result => {
      Swal.fire("Result deleted...");
      this.updateResultList();
    }, error => Swal.fire("Oops! Something went wrong. Result not deleted."));
  }




  

}
