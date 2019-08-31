import { DomainValidators } from "./domain-validators";
import { FormControl } from "@angular/forms";

// Straight Jasmine testing without Angular's testing support
describe("ValueService", () => {
  const correctPeselNumbers = [
    "90090515836",
    "92071314764",
    "81100216357",
    "80072909146",
    "93092507490"
  ];

  const incorrectPeselNumbers = [
    "1",
    "11",
    "111",
    "1111",
    "11111",
    "111111",
    "1111111",
    "11111111",
    "111111111",
    "1111111111",
    "11111111111"
  ];

  correctPeselNumbers.forEach(correctPesel => {
    it("#pesel validation should pass for correct PESEL numbers", () => {
      // Given
      const domainValidators = new DomainValidators();
      const formControl = new FormControl();
      formControl.setValue(correctPesel);

      // When
      const validationResult = domainValidators.pesel(formControl);

      // Then
      expect(validationResult).toBeNull();
    });
  });

  incorrectPeselNumbers.forEach(incorrectPesel => {
    it("#pesel validation should fail for incorrect PESEL numbers", () => {
      // Given
      const domainValidators = new DomainValidators();
      const formControl = new FormControl();
      formControl.setValue(incorrectPesel);

      // When
      const validationResult = domainValidators.pesel(formControl);

      // Then
      expect(validationResult.pesel.valid).toBe(false);
    });
  });
});
