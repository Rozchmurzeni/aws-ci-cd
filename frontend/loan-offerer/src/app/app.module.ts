import { LoanOfferService } from "./loan-offer/loan-offer.service";
import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";

import { AppComponent } from "./app.component";
import { LoanOfferComponent } from "./loan-offer/loan-offer.component";
import { DomainValidators } from "./domain-validators";
import { ConfigProvider } from "./config-provider";

@NgModule({
  declarations: [AppComponent, LoanOfferComponent],
  imports: [BrowserModule, FormsModule, ReactiveFormsModule, HttpClientModule],
  providers: [LoanOfferService, DomainValidators, ConfigProvider],
  bootstrap: [AppComponent]
})
export class AppModule {}
