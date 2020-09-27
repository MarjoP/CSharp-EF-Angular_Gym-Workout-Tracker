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

  public newUser: string;
  public newExercise: string;


  constructor(private router: Router, private woService: WorkoutService, public dialog: MatDialog) { 

    this.woService.getUsers().subscribe(result => {
      this.users = result;
    }, error => console.error(error));

    this.woService.getExercises().subscribe(result => {
      this.exercises = result;
    }, error => console.error(error));
  }
  addNewResult() {
    //do something
  }

  selUser(filterVal: any) {
    this.selectedUser = filterVal;
    if (filterVal == "newUser") {
      //do something
    }
  }

  selExercise(filterVal: any) {
    this.selectedExercise = filterVal;
    if (filterVal == "newExercise") {
      //do something 
    }
  }
  
  openUserDialog(): void {
    const dialogRef = this.dialog.open(DialogBoxComponent, {
      width:'250px',
      data: { newUser: this.newUser, }
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.newUser = result;
    });
  }

  openExerciseDialog(): void {
    const dialogRef = this.dialog.open(DialogBoxComponent, {
      width: '250px;',
      data: { newExercise: this.newExercise, }
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.newUser = result;
    });
  }
}
