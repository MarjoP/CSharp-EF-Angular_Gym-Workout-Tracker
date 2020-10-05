import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { WorkoutService } from '../workout.service';
import { Router } from '@angular/router';
import { extractStyleUrls } from '@angular/compiler/src/style_url_resolver';
import { Chart } from 'chart.js';
import { forEachChild } from 'typescript/lib/tsserverlibrary';
import Swal from 'sweetalert2';


@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html',
  styleUrls: ['./fetch-data.component.scss']
})
export class FetchDataComponent {
  public results: any = {
    Date: "",
    User: {
      UserName: ""
    },
    Exercise: {
      ExerciseName: ""
    },
    Repeats: "",
    Weight: "",
  };
  public users: any;
  public exercises: any;

  public selectedUser: string;
  public selectedExercise: string;
  public quantity: number = -1;

  public show: boolean;
  public graphVisible: boolean = true;

  public canvas: any = <HTMLCanvasElement>document.getElementById('myChart');
  public chart: any;
  public ctx: any;
  public labels: String[] = [];
  public MRes: String[] = [];


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
      document.getElementById("res").style.display = "block";
      document.getElementById("myChart").style.display = "none";
      document.getElementById("footer").style.display = "none";
    }, error => console.error(error));
  }

  public hideResults() {
    document.getElementById("res").style.display = "none";
    document.getElementById("myChart").style.display = "none";
    document.getElementById("footer").style.display = "block";
  }

  public getMax() {
    this.woService.getMaxResults(this.selectedUser, this.selectedExercise, this.quantity).subscribe(result => {
      this.results = result;
   
      this.labels = this.results.map(function (e) {
        return e.date.substr(0, 10);
      });
      this.MRes = this.results.map(function (e) {
        return e.weight;
      });

      console.log(this.labels);
      document.getElementById("footer").style.display = "none";
      document.getElementById("res").style.display = "block";
      document.getElementById("myChart").style.display = "block";

      this.createGraph();
    }, error => Swal.fire("Select one user and one exercise first!"));
  }

  createGraph() {
    Chart.defaults.global.elements.line.tension = 0;
    Chart.defaults.global.elements.line.borderColor= 'rgba(217, 0, 83, 0.5)';
    this.canvas = <HTMLCanvasElement>document.getElementById('myChart');
    const ctx = this.canvas.getContext('2d');
    var myChart = new Chart(ctx, {
      type: 'line',
      data: {
        labels: this.labels,
        datasets: [{
          label: 'Max results',
          data: this.MRes
        }]
      },
      options: {
        scales: {
          yAxes: [{
            ticks: {
              beginAtZero:true
            }
          }]
        }
      }
    });
  }


}
