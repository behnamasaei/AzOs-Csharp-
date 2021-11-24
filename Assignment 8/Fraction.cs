namespace Assignment_8
{
    public class Fraction
    {
        public string Sum(decimal sorat1, decimal makhrag1, decimal sorat2, decimal makhrag2)
        {
            var makhrag = makhrag1 * makhrag2;
            var sorat1New = sorat1 * (makhrag / makhrag1);
            var sorat2New = sorat2 * (makhrag / makhrag2);
            return ($"{sorat1New + sorat2New}/{makhrag}");
        }

        public string Minus(decimal sorat1, decimal makhrag1, decimal sorat2, decimal makhrag2)
        {
            var makhrag = makhrag1 * makhrag2;
            var sorat1New = sorat1 * (makhrag / makhrag1);
            var sorat2New = sorat2 * (makhrag / makhrag2);
            return ($"{sorat1New - sorat2New}/{makhrag}");
        }

        public string Multi(decimal sorat1, decimal makhrag1, decimal sorat2, decimal makhrag2)
        {
            return ($"{sorat1 * sorat2}/{makhrag1 * makhrag2}");
        }

        public string Division(decimal sorat1, decimal makhrag1, decimal sorat2, decimal makhrag2)
        {
            return $"{sorat1 * makhrag2}/{makhrag1 * sorat2}";
        }

        public string SimpleFraction(decimal sorat, decimal makhrag)
        {
            try
            {
                //assign an integer to the gcd value
                var gcdNum = gcd(sorat, makhrag);
                if (gcdNum != 0)
                {
                    sorat = sorat / gcdNum;
                    makhrag = makhrag / gcdNum;
                }

                if (makhrag < 0)
                {
                    makhrag = makhrag * -1;
                    sorat = sorat * -1;
                }
            }
            catch (Exception exp)
            {
                // display the following error message 
                // if the fraction cannot be reduced
                throw new InvalidOperationException("Cannot reduce Fraction: " + exp.Message);
            }
            return $"{sorat}/{makhrag}";
        }

        public static decimal gcd(decimal answerNumerator, decimal answerDenominator)
        {
            // assigned x and y to the answer Numerator/Denominator, as well as an  
            // empty integer, this is to make code more simple and easier to read
            var x = Math.Abs(answerNumerator);
            var y = Math.Abs(answerDenominator);
            decimal m;
            // check if numerator is greater than the denominator, 
            // make m equal to denominator if so
            if (x > y)
                m = y;
            else
                // if not, make m equal to the numerator
                m = x;
            // assign i to equal to m, make sure if i is greater
            // than or equal to 1, then take away from it
            for (var i = m; i >= 1; i--)
            {
                if (x % i == 0 && y % i == 0)
                {
                    //return the value of i
                    return i;
                }
            }

            return 1;
        }

    }
}
