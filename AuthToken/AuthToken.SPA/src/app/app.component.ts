import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { AuthenticationService } from './services/Authentication.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  private userData: any;

  public constructor(private titleService: Title, private authenticationService: AuthenticationService) { }

  ngOnInit() {
  }


  public setTitle(newTitle: string) {
    this.titleService.setTitle(this.title);
  }

  title = 'AuthToken';
}
