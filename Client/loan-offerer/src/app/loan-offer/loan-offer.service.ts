import { InitialOfferReadModel } from '../models/initial-offer-read-model';
import { Injectable } from '@angular/core';
import { Observable, from } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoanOfferService {

  constructor() { }

  getLoanOffer(pesel: string, email: string): Observable<InitialOfferReadModel> {
    // Mock implementation, actual implementation is TODO

    const promise = new Promise<InitialOfferReadModel>(
      resolve => setTimeout(
        () => resolve(this.getMockedLoanOffer()), 3000));

    return from(promise);
  }


  private getMockedLoanOffer(): InitialOfferReadModel {
    const initialOfferResult: InitialOfferReadModel = {
      id: '6fc0a80c-33d4-4440-a5c7-c5b17928eb38',
      minAmount: 1000,
      maxAmount: 5000
    };

    return initialOfferResult;
  }
}
