using System;

namespace ExternalScoringService
{
    internal struct PeselNumber
    {
        private readonly string _rawPeselNumber;

        internal int FirstPart => Int32.Parse(_rawPeselNumber.Substring(0, 6));
        internal int SecondPart => Int32.Parse(_rawPeselNumber.Substring(6, 5));

        internal PeselNumber(string rawPeselNumber)
        {
            if (!IsPeselNumberValid(rawPeselNumber))
            {
                throw new ArgumentException("PESEL number is not valid", nameof(rawPeselNumber));
            }

            _rawPeselNumber = rawPeselNumber;
        }

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
