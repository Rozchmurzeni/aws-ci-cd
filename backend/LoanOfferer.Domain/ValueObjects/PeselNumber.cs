using System;
using LoanOfferer.Domain.Exceptions;

namespace LoanOfferer.Domain.ValueObjects
{
    public class PeselNumber : IValueObject<string>
    {
        public PeselNumber(string value)
        {
            if (!IsPeselNumberValid(value))
            {
                throw new IncorrectPeselNumberException(value);
            }

            Value = value;
        }

        public string Value { get; }

        private static bool IsPeselNumberValid(string rawPeselNumber)
        {
            var result = false;

            if (rawPeselNumber.Length == 11)
            {
                var controlSum = CalculateControlSum(rawPeselNumber);
                var controlNumber = controlSum % 10;
                controlNumber = 10 - controlNumber;

                if (controlNumber == 10)
                {
                    controlNumber = 0;
                }

                var lastDigit = Int32.Parse(rawPeselNumber[rawPeselNumber.Length - 1].ToString());
                result = controlNumber == lastDigit;
            }

            return result;
        }

        private static int CalculateControlSum(string rawPeselNumber)
        {
            int[] weights = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
            var controlSum = 0;

            for (var i = 0; i < rawPeselNumber.Length - 1; i++)
            {
                controlSum += weights[i] * Int32.Parse(rawPeselNumber[i].ToString());
            }

            return controlSum;
        }
    }
}
