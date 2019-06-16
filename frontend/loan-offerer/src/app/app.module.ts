import { LoanOfferService } from './loan-offer/loan-offer.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { LoanOfferComponent } from './loan-offer/loan-offer.component';
import { DomainValidators } from './domain-validators';

@NgModule({
  declarations: [
    AppComponent,
    LoanOfferComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  providers: [LoanOfferService, DomainValidators],
  bootstrap: [AppComponent]
})
export class AppModule { }
