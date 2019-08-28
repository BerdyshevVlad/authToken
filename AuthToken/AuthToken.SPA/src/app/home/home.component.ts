import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  public isLoggedIn: boolean = false;

  constructor(private toastr: ToastrService,
    private router: Router,
    private route: ActivatedRoute) { }

  ngOnInit() {
    if (localStorage.getItem("user")) {
      this.isLoggedIn = true;
    }
  }

  signOut() {
    if (localStorage.getItem("user")) {
      this.isLoggedIn = false;
      this.router.navigate(['/authorization/login']);
    }
  }

}
