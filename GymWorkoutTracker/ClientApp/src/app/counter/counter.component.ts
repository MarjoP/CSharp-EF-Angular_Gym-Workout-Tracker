import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { WorkoutService } from '../workout.service';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DialogBoxComponent } from '../dialog-box/dialog-box.component';

@Component({
  selector: 'app-counter-component',
  templateUrl: './counter.component.html',
  styleUrls: ['./counter.component.scss']
})

export class CounterComponent {
  public users: any;
  public exercises: any;

  public selectedUser: string;
  public selectedExercise: string;
  public repeats: number;
  public weight: number;

  public newUser: any = {
    userName: ""};
  public newExercise: string;


  constructor(private router: Router, private woService: WorkoutService, public dialog: MatDialog) { 
    this.updateUserList();
    this.updateExerciseList();
    /*this.woService.getUsers().subscribe(result => {
      this.users = result;
    }, error => console.error(error));

    this.woService.getExercises().subscribe(result => {
      this.exercises = result;
    }, error => console.error(error));  */
  }


  addNewResult() {
    //do something
  }

  updateUserList() : void {
    this.woService.getUsers().subscribe(result => {
      this.users = result;
    }, error => console.error(error));
  }

  updateExerciseList(): void {
    this.woService.getExercises().subscribe(result => {
      this.exercises = result;
    }, error => console.error(error));
  }

  selUser(filterVal: any) {
    this.selectedUser = filterVal;
    if (filterVal === "newUser") {
      console.log("New user selected");
      this.openUserDialog();
    }
  }

  selExercise(filterVal: any) {
    this.selectedExercise = filterVal;
    if (filterVal === "newExercise") {
      this.openExerciseDialog();
    }
  }
  
  openUserDialog(): void {
    const dialogRef = this.dialog.open(DialogBoxComponent, {
      width: '300px',
      height: '300px',
      data: { newUser: this.newUser.userName}
    });

    console.log("Dialog opened");
    dialogRef.afterClosed().subscribe(result => {
      this.newUser.userName = result;
      this.woService.addUser(this.newUser).subscribe(res => {
      }, error => console.error(error));
    });
    this.updateUserList();
  }



  openExerciseDialog(): void {
    const dialogRef = this.dialog.open(DialogBoxComponent, {
      width: '300px',
      height: '300px',
      data: { newExercise: this.newExercise}
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.newExercise= result;
    });
  }
}
