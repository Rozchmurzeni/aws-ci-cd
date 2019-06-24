import { Component, OnInit } from "@angular/core";
import { FormGroup, Validators, FormBuilder } from "@angular/forms";

import { InitialOfferReadModel } from "./../models/initial-offer-read-model";
import { LoanOfferService } from "./loan-offer.service";
import { GetInitialOfferWriteModel } from "../models/get-initial-offer-write-model";
import { DomainValidators } from "../domain-validators";
import { RequestLoanWriteModel } from "../models/request-loan-write-model";

@Component({
  selector: "app-loan-offer",
  templateUrl: "./loan-offer.component.html",
  styleUrls: ["./loan-offer.component.scss"]
})
export class LoanOfferComponent implements OnInit {
  readonly minLoanAmount = 0;

  getInitialOfferModel: GetInitialOfferWriteModel;
  getInitialOfferForm: FormGroup;

  isGettingOffer: boolean;
  isSendingLoanApplication: boolean;
  applicationSent: boolean;

  initialOffer: InitialOfferReadModel;

  loanAmount: number;

  constructor(
    private formBuilder: FormBuilder,
    private loanOfferService: LoanOfferService,
    private domainValidators: DomainValidators
  ) {}

  ngOnInit(): void {
    this.getInitialOfferModel = new GetInitialOfferWriteModel();

    this.getInitialOfferForm = this.formBuilder.group({
      pesel: ["", [Validators.required, this.domainValidators.pesel]],
      email: ["", [Validators.required, Validators.email]]
    });
  }

  getInitialOffer(): void {
    const peselNumber = this.getInitialOfferForm.get("pesel").value;
    const emailAddress = this.getInitialOfferForm.get("email").value;
    const model = <GetInitialOfferWriteModel>{
      PeselNumber: peselNumber,
      EmailAddress: emailAddress
    };

    this.isGettingOffer = true;
    this.loanOfferService.getLoanOffer(model).subscribe(result => {
      this.isGettingOffer = false;
      this.initialOffer = result;
      this.loanAmount = this.initialOffer.MaxLoanAmount;
    });
  }

  sendLoanApplication(): void {
    const model = <RequestLoanWriteModel>{
      OfferId: this.initialOffer.Id,
      RequestedAmount: this.loanAmount
    };
    this.isSendingLoanApplication = true;
    this.loanOfferService.sendLoanApplication(model).subscribe(() => {
      this.isSendingLoanApplication = false;
      this.applicationSent = true;
    });
  }
}
