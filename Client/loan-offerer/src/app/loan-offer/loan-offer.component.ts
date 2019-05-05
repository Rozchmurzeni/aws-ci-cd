import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder, FormControl } from '@angular/forms';

import { InitialOfferReadModel } from './../models/initial-offer-read-model';
import { LoanOfferService } from './loan-offer.service';
import { GetInitialOfferWriteModel } from '../models/get-initial-offer-write-model';
import { DomainValidators } from '../domain-validators';

@Component({
  selector: 'app-loan-offer',
  templateUrl: './loan-offer.component.html',
  styleUrls: ['./loan-offer.component.scss']
})
export class LoanOfferComponent implements OnInit {

  getInitialOfferModel: GetInitialOfferWriteModel;
  getInitialOfferForm: FormGroup;

  isGettingOffer: boolean;
  isSendingLoanApplication: boolean;
  applicationSent: boolean;

  initialOffer: InitialOfferReadModel;

  loanAmount: number;

  constructor(private formBuilder: FormBuilder, private loanOfferService: LoanOfferService, private domainValidators: DomainValidators) { }

  ngOnInit(): void {
    this.getInitialOfferModel = new GetInitialOfferWriteModel();

    this.getInitialOfferForm = this.formBuilder.group({
      pesel: ['', [Validators.required, this.domainValidators.pesel]],
      email: ['', [Validators.required, Validators.email]]
    });
  }

  getInitialOffer(): void {
    const pesel = this.getInitialOfferForm.get('pesel').value;
    const email = this.getInitialOfferForm.get('email').value;

    this.isGettingOffer = true;
    this.loanOfferService.getLoanOffer(pesel, email)
      .subscribe(result => {
        this.isGettingOffer = false;
        this.initialOffer = result;
        this.loanAmount = this.initialOffer.maxAmount;
      });
  }

  sendLoanApplication(): void {
    this.isSendingLoanApplication = true;
    this.loanOfferService.sendLoanApplication(this.initialOffer.id, this.loanAmount)
      .subscribe(() => {
        this.isSendingLoanApplication = false;
        this.applicationSent = true;
        console.log('id: ' + this.initialOffer.id);
        console.log('loan amount: ' + this.loanAmount);
      });
  }
}
