import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { WorkoutService } from '../workout.service';

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

  constructor(private router: Router, private woService: WorkoutService) { 

    this.woService.getUsers().subscribe(result => {
      this.users = result;
    }, error => console.error(error));

    this.woService.getExercises().subscribe(result => {
      this.exercises = result;
    }, error => console.error(error));
  }

  selUser(filterVal: any) {
    this.selectedUser = filterVal;
    if (filterVal == "newUser") {
     
    }
  }

  selExercise(filterVal: any) {
    this.selectedExercise = filterVal;
  }

  /*
  openDialog(): void {
    const dialogRef = this.dialog.open(DialogOverviewExampleDialog, {
      width: '250px',
      data: { name: this.name, animal: this.animal }
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.animal = result;
    });
  }   */

}
