import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

import { InitialOfferReadModel } from "../models/initial-offer-read-model";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { ConfigProvider } from "../config-provider";
import { GetInitialOfferWriteModel } from "../models/get-initial-offer-write-model";
import { RequestLoanWriteModel } from '../models/request-loan-write-model';

@Injectable({
  providedIn: "root"
})
export class LoanOfferService {
  constructor(private http: HttpClient, private config: ConfigProvider) {}

  getLoanOffer(
    model: GetInitialOfferWriteModel
  ): Observable<InitialOfferReadModel> {
    return this.http.post<InitialOfferReadModel>(
      this.config.loanOfferApiUrl(),
      model,
      { headers: this.getHeaders() }
    );
  }

  sendLoanApplication(model: RequestLoanWriteModel): Observable<string> {
    return this.http.put<string>(this.config.loanOfferApiUrl(), model, {
      headers: this.getHeaders()
    });
  }

  private getHeaders(): any {
    return new HttpHeaders({
      "Content-Type": "application/json",
      "x-api-key": this.config.loanOfferApiKey()
    });
  }
}
