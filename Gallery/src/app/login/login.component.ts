import {
  Component,
  OnInit,
  ElementRef,
  ViewChild
} from '@angular/core';
import { NgForm } from '@angular/forms'; 
import { Router, ActivatedRoute } from '@angular/router';
import { ServerInfoConfigComponent } from '../server-info-config/server-info-config.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {  
  
   @ViewChild('login', { static: false }) loginRef: ElementRef = {} as ElementRef;
   @ViewChild('password', { static: false }) passwordRef: ElementRef = {} as ElementRef;
  
    
  constructor(
    private router: Router,
    private route: ActivatedRoute) {
  }


   ngOnInit() { 
    this.loginRef.nativeElement;
    this.passwordRef.nativeElement;
  }

  onSubmit(form: NgForm) { 
    fetch(ServerInfoConfigComponent.serverUrl +"Auth/Login", {
      method: "POST",  
      mode: "cors",  
      cache: "no-cache",  
      credentials: "same-origin",  
      headers: {
        "Content-Type": "application/json", 
        
      }, 
      referrerPolicy: "no-referrer", 
      body: JSON.stringify({ 
        "username": this.loginRef.nativeElement.value,
        "password":  this.passwordRef.nativeElement.value
      }),  
    })
    .then(response => response.json())
    .then(data => { 

      localStorage.setItem('authToken', JSON.stringify(data["secretKey"]));
      localStorage.setItem('usersName', JSON.stringify(data["user"]["login"]));
      window.dispatchEvent(new Event("storage"));
    });
  }
}
 