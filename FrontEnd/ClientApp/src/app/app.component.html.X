
<gm-header></gm-header>

<!-- <main> -->
<!-- <ng-container> -->
  <!-- <ng-container [class.is-mobile]="mediaQueryList.matches"> -->


    <!-- <mat-toolbar class="main-header" [class.is-mobile]="mediaQueryList.matches">
        <button mat-icon-button class="main-header-button" (click)="sidenav.toggle()">
            <mat-icon>menu</mat-icon>
        </button>
        <gm-user-dropdown></gm-user-dropdown>
    </mat-toolbar> -->

  <mat-sidenav-container class="sidenav-container"
    [hasBackdrop]="options.value.hasBackdrop">

    <!-- <mat-sidenav class="sidenav" #sidenav> -->
    <mat-sidenav class="sidenav" #sidenav
      [mode]="options.value.mode"
      [opened]="!mediaQueryList.matches"
      [fixedInViewport]="options.value.fixed"
      [fixedTopGap]="options.value.top"
      [fixedBottomGap]="options.value.bottom">

      <!-- <div class="sidenav-header">
        <button class="sidenav-close" mat-icon-button (click)="sidenav.toggle()">
          <mat-icon>close</mat-icon>
        </button>
      </div> -->

      <gm-sidenav-menu></gm-sidenav-menu>

    </mat-sidenav>

    <mat-sidenav-content [formGroup]="options">

        <router-outlet></router-outlet>

        <!-- <gm-about-project></gm-about-project> -->
        <!-- <gm-main-content></gm-main-content> -->

    </mat-sidenav-content>

  </mat-sidenav-container>


<!-- </ng-container> -->
<!-- </main> -->

<mat-toolbar class="main-footer">
  This is the mat-toolbar footer.
  An element with position: absolute; is positioned relative to the
</mat-toolbar>


<!-- partial -->
<script src='https://code.jquery.com/jquery-3.3.1.min.js'></script>
<script src='https://www.amcharts.com/lib/3/amcharts.js'></script>
<script src='https://www.amcharts.com/lib/3/serial.js'></script>
<script src='https://www.amcharts.com/lib/3/themes/light.js'></script>
<script  src="../../../script.js"></script>
