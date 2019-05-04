import { InitialOfferReadModel } from './../models/initial-offer-read-model';
import { Component } from '@angular/core';
import { LoanOfferService } from './loan-offer.service';

@Component({
  selector: 'app-loan-offer',
  templateUrl: './loan-offer.component.html',
  styleUrls: ['./loan-offer.component.scss']
})
export class LoanOfferComponent {

  initialOffer: InitialOfferReadModel;
  isGettingOffer: boolean;

  constructor(private loanOfferService: LoanOfferService) { }

  getInitialOffer(): void {
    this.isGettingOffer = true;
    this.loanOfferService.getLoanOffer(null, null)
      .subscribe(result => {
        this.isGettingOffer = false;
        this.initialOffer = result;
      });
  }
}
