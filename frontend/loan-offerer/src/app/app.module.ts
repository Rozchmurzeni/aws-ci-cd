import { LoanOfferService } from "./loan-offer/loan-offer.service";
import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";

import { AppComponent } from "./app.component";
import { LoanOfferComponent } from "./loan-offer/loan-offer.component";
import { DomainValidators } from "./domain-validators";
import { ConfigProvider } from "./config-provider";
import { HttpErrorInterceptor } from "./http-error.interceptor";
import { ProcessFailedComponent } from './loan-offer/process-failed/process-failed.component';

@NgModule({
  declarations: [AppComponent, LoanOfferComponent, ProcessFailedComponent],
  imports: [BrowserModule, FormsModule, ReactiveFormsModule, HttpClientModule],
  providers: [
    LoanOfferService,
    DomainValidators,
    ConfigProvider,
    { provide: HTTP_INTERCEPTORS, useClass: HttpErrorInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
