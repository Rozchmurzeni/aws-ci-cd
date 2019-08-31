import { Injectable } from '@angular/core';
import { FormControl } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class DomainValidators {

  constructor() { }

  pesel(formControl: FormControl) {
    const weight = [1, 3, 7, 9, 1, 3, 7, 9, 1, 3];
    let controlValue = formControl.value;
    const controlNumber = parseInt(controlValue.substring(10, 11));

    let checkSum = 0;

    for (let i = 0; i < weight.length; i++) {
      checkSum += (parseInt(controlValue.substring(i, i + 1)) * weight[i]);
    }

    checkSum = checkSum % 10;

    let isValid = (checkSum ? 10 - checkSum : checkSum) === controlNumber

    return isValid ? null : {
      pesel: {
        valid: false
      }
    };
  }
}
