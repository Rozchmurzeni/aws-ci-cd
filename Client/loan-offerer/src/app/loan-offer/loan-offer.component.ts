import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';

import { InitialOfferReadModel } from './../models/initial-offer-read-model';
import { LoanOfferService } from './loan-offer.service';
import { GetInitialOfferWriteModel } from '../models/get-initial-offer-write-model';

@Component({
  selector: 'app-loan-offer',
  templateUrl: './loan-offer.component.html',
  styleUrls: ['./loan-offer.component.scss']
})
export class LoanOfferComponent implements OnInit {

  getInitialOfferModel: GetInitialOfferWriteModel;
  getInitialOfferForm: FormGroup;

  isGettingOffer: boolean;
  initialOffer: InitialOfferReadModel;

  constructor(private formBuilder: FormBuilder, private loanOfferService: LoanOfferService) { }

  ngOnInit(): void {
    this.getInitialOfferModel = new GetInitialOfferWriteModel();

    this.getInitialOfferForm = this.formBuilder.group({
      pesel: ['', Validators.required],
      email: ['', Validators.required]
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
      });
  }
}
