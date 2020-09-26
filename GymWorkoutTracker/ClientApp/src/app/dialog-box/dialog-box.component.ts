import { Component, OnInit, Inject } from '@angular/core';
import { CounterComponent } from '../counter/counter.component';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-dialog-box',
  templateUrl: './dialog-box.component.html',
  styleUrls: ['./dialog-box.component.css']
})

export class DialogBoxComponent {

  constructor(public dialogRef: MatDialogRef<DialogBoxComponent>,
    @Inject(MAT_DIALOG_DATA) public data: CounterComponent) { }

  onNoClick(): void {
    this.dialogRef.close();
  }
  ngOnInit() {
  }
}
