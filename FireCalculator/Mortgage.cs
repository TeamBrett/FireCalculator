using System;
using System.Text;

namespace FireCalculator {
    public class Mortgage : Loan {
        public Mortgage(string name, House house)
            : base(name) {
            this.House = house;
        }

        public decimal MortgageInsurance { get; set; }

        public decimal ClosingCosts { get; set; }

        public decimal Taxes { get; set; }

        public House House { get; set; }

        private string Calculate(Mortgage vars) {
            vars.DownPayment = vars.DownPayment / 100;
            this.CalculateMortgage(vars);

            var totals = new StringBuilder();
            vars.DownPayment = .05m;
            totals.AppendLine("Total @  5%: " + this.CalculateMortgage(vars));
            vars.DownPayment = .10m;
            totals.AppendLine("Total @ 10%: " + this.CalculateMortgage(vars));
            vars.DownPayment = .15m;
            totals.AppendLine("Total @ 15%: " + this.CalculateMortgage(vars));
            vars.DownPayment = .20m;
            totals.AppendLine("Total @ 20%: " + this.CalculateMortgage(vars));
            vars.DownPayment = .25m;
            totals.AppendLine("Total @ 25%: " + this.CalculateMortgage(vars));
            return totals.ToString();
        }

        private string CalculateMortgage(Mortgage vars) {
            var downPaymentPercentage = vars.DownPayment / 100;
            vars.MortgageInsurance = vars.Principal * (decimal).0075 / 12;
            var rate = vars.InterestRate / 100 / 12;
            var downPayment = vars.Principal * downPaymentPercentage;
            var houseCost = vars.Principal;
            var principal = houseCost - downPayment;
            var months = vars.Years * 12;
            var insurance = vars.MortgageInsurance / 12;
            var taxes = vars.Taxes / 12;
            var pmi = vars.MortgageInsurance;

            var numerator = rate * Convert.ToDecimal(Math.Pow((double)(1 + rate), months));
            var denominator = Convert.ToDecimal(Math.Pow((double)(1 + rate), months) - 1);

            var payment = principal * numerator / denominator;

            var log = new StringBuilder();
            log.AppendLine($"Down Payment: {downPayment:C}");
            log.AppendLine($"Payment: {payment:C}");
            log.AppendLine($"Insurance: {insurance:C}");
            log.AppendLine($"Taxes: {taxes:C}");
            log.AppendLine($"PMI: {pmi:C}");
            log.AppendLine($"Total: {(payment + insurance + taxes + pmi):C}");

            decimal totalInterest = 0;
            decimal totalPrincipal = 0;
            decimal totalPmi = 0;
            for (int i = 0; i < months; i++) {
                var interestPayment = principal * rate;
                var principalPayment = payment - interestPayment;

                totalInterest += interestPayment;
                totalPrincipal += principalPayment;

                var twentyPercent = houseCost * .20m;
                var ownedValue = downPayment + totalPrincipal;
                if (ownedValue < twentyPercent) {
                    totalPmi += pmi;
                }

                principal = principal - principalPayment;
            }

            var total = totalPmi + totalPrincipal + totalInterest;

            log.AppendLine($"Total Principal: {totalPrincipal:C}");
            log.AppendLine($"Total Interest: {totalInterest:C}");
            log.AppendLine($"Total PMI: {totalPmi:C}");
            log.AppendLine($"Total: {total:C}");

            ////var monthsSaving = downPayment / (double)SavingsPerMonthInput.Value;
            ////var totalRentWhileSaving = monthsSaving * (double)RentPerMonthInput.Value;

            ////log = new StringBuilder();
            ////log.AppendLine($"Months Saving: {monthsSaving}");
            ////log.AppendLine($"Rent Cost: {totalRentWhileSaving:C}");

            return log.ToString();
        }

    }
}