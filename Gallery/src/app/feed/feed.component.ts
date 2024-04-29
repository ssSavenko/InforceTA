import {
  OnInit,
  Component
} from '@angular/core';
import { ServerInfoConfigComponent } from '../server-info-config/server-info-config.component';

@Component({
  selector: 'app-feed',
  templateUrl: './feed.component.html',
  styleUrl: './feed.component.css'
})
export class FeedComponent {
  private albumIndex: number = 0;
  private readonly albumsPerPage: number = 5;

  private albumsCount: number = 0;

  public currentItemsList: any;

  public constructor() {
    this.GetElenmentsCount();
    this.GetElements()
  }

  ngOnInit() {
  }

  private GetElenmentsCount() {
    fetch(ServerInfoConfigComponent.serverUrl + "Album/total", {
      method: "GET",
      mode: "cors",
      cache: "no-cache",
      credentials: "same-origin",
      headers: {
        "Content-Type": "application/json",
      },
      referrerPolicy: "no-referrer"
    })
      .then(response => response.json())
      .then(data => { this.albumsCount = data; });
  }

  private GetElements() {

    var url = ServerInfoConfigComponent.serverUrl + "Album?index=" + String(this.albumIndex) + "&count=" + String(this.albumsPerPage);

    var elements = new Promise(function (resolve, reject) {
      fetch(url, {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json'
        }
      }).then(async response => {
        if (response.ok) {
          response.json().then(json => resolve(json));
        } else {
          response.json().then(json => reject(json));
        };
      }).catch(async error => {
        reject(error);
      });
    });

    elements
      .then(results => {
        this.currentItemsList = results;

        // this.currentItemsList.forEach((element: any) => {
        //   this.AddImageUrl(element);
        // });
      });

  }

  // public AddImageUrl(element: any): any {

  //   var url = ServerInfoConfigComponent.serverUrl + "Album/" + String(element["id"]) + "/firstImage";

  //   var elements = new Promise(function (resolve, reject) {
  //     fetch(url, {
  //       method: 'GET',
  //       headers: {
  //         'Content-Type': 'application/json'
  //       }
  //     }).then(async response => {
  //       if (response.ok) {
  //         console.log(response)
  //         response.json().then(json => resolve(json));
  //       } else {
  //         response.json().then(json => reject(json));
  //       };
  //     }).catch(async error => {
  //       reject(error);
  //     });
  //   }) ;

  //   elements
  //     .then((results : any ) => { 
        
  //       element["url"] = results["base64Code"];
  //     });

  // }
 
  public isNextEnabled(): Boolean {
  return this.albumIndex < this.albumsCount + this.albumsPerPage;
}
  public isPrevEnabled(): Boolean {
  return this.albumIndex > 0;
}
}
