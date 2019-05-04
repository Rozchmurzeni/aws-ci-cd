import { LoanOfferService } from './loan-offer/loan-offer.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { LoanOfferComponent } from './loan-offer/loan-offer.component';

@NgModule({
  declarations: [
    AppComponent,
    LoanOfferComponent
  ],
  imports: [
    BrowserModule
  ],
  providers: [LoanOfferService],
  bootstrap: [AppComponent]
})
export class AppModule { }
