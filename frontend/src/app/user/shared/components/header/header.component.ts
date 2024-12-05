import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {RouterLink} from '@angular/router';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    RouterLink
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent implements OnInit {
  @ViewChild('toggleOpen', { static: true }) toggleOpen!: ElementRef;
  @ViewChild('toggleClose', { static: true }) toggleClose!: ElementRef;
  @ViewChild('collapseMenu', { static: true }) headerContent!: ElementRef;

  ngOnInit() { }

  isMenuVisible: boolean = false;
  handleClick() {
    this.isMenuVisible = !this.isMenuVisible;
  }
}
