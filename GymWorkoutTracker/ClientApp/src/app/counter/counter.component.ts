import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { WorkoutService } from '../workout.service';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DialogBoxComponent } from '../dialog-box/dialog-box.component';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-counter-component',
  templateUrl: './counter.component.html',
  styleUrls: ['./counter.component.scss']
})

export class CounterComponent {
  public users: any;
  public exercises: any;

  public result: any = {
    User: {
      UserName:""
    },
   Exercise: {
      ExerciseName:""
    },
      Repeats: "",
      Weight: "",
    }


  public newUser: any = { userName: "" };

  public newExercise: any = { exerciseName: "" };

  constructor(private router: Router, private woService: WorkoutService, public dialog: MatDialog) {
    this.woService.getUsers().subscribe(result => {
      this.users = result;
    }, error => console.error(error));

    this.woService.getExercises().subscribe(result => {
      this.exercises = result;
    }, error => console.error(error));
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

  selUser(filterVal: any) {
    this.result.User.UserName = filterVal;
    if (filterVal === "newUser") {
      this.openUserDialog();
    }
  }

  selExercise(filterVal: any) {
    this.result.Exercise.ExerciseName = filterVal;
    
    if (filterVal === "newExercise") {
      this.openExerciseDialog();
    }
  }

  openUserDialog(): void {
    const dialogRef = this.dialog.open(DialogBoxComponent, {
      width: '300px',
      height: '300px',
      data: { newUser: this.newUser.userName }
    });

    dialogRef.afterClosed().subscribe(result => {
      this.newUser.userName = result;
      this.woService.addUser(this.newUser).subscribe(res => {
        Swal.fire('Great! New user added.');
        this.updateUserList();
        this.newUser.userName = "";
      }, error => Swal.fire('Could not add new user! Check if the username already exists in the list or try again with different name.'));
    });
  }

  openExerciseDialog(): void {
    const dialogRef = this.dialog.open(DialogBoxComponent, {
      width: '300px',
      height: '300px',
      data: { newExercise: this.newExercise.ExerciseName }
    });

    dialogRef.afterClosed().subscribe(result => {
      this.newExercise.ExerciseName = result;
      this.woService.addExercise(this.newExercise).subscribe(res => {
        Swal.fire('Great! New exercise added.');
        this.updateExerciseList();
        this.newExercise.ExerciseName = "";
      }, error => Swal.fire('Could not add new exercise! Maybe it is already in the list?'));
    });
  }

  addNewResult(): void {
    console.log(this.result);
    this.woService.addResult(this.result).subscribe(res => {
      Swal.fire('Well done! New results added.');
      this.result.Repeats = "";
      this.result.Weight = "";
    }, error => Swal.fire('Something went wrong...'));

}
}


