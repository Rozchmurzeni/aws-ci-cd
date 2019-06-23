import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

import { InitialOfferReadModel } from "../models/initial-offer-read-model";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { ConfigProvider } from "../config-provider";

@Injectable({
  providedIn: "root"
})
export class LoanOfferService {
  constructor(private http: HttpClient, private config: ConfigProvider) {}

  getLoanOffer(
    pesel: string,
    email: string
  ): Observable<InitialOfferReadModel> {
    const model = { peselNumber: pesel, emailAddress: email };
    return this.http.post<InitialOfferReadModel>(
      this.config.loanOfferApiUrl(),
      model,
      { headers: this.getHeaders() }
    );
  }

  sendLoanApplication(id: string, loanAmount: number): Observable<string> {
    const model = { offerId: id, requestedAmount: loanAmount };
    return this.http.put<string>(this.config.loanOfferApiUrl(), model, {
      headers: this.getHeaders()
    });
  }

  private getHeaders(): any {
    return new HttpHeaders({
      "Content-Type": "application/json",
      "x-api-key": this.config.loanOfferApiUrl()
    });
  }
}
