using System.Collections.Generic;

namespace FireCalculator {
    public interface IDisbursable {
        decimal Disburse(decimal amount);
    }

    public enum DisbursementAmountType {
        Percent,
        Dollar
    }

    public class Job {
        private decimal salary;

        public List<IDisbursable> Distributions { get; set; } = new List<IDisbursable>();

        public decimal DisbursementAmount => this.salary / (int)this.DisbursementPeriod;

        public Period DisbursementPeriod { get; set; } = Period.Annually;

        public Person Person { get; set; }

        public void SetSalary(decimal amount, Period period) {
            this.salary = amount * (int)period;
        }

        public void Disburse() {
            if (!Strategy.Now.IsToday(this.DisbursementPeriod)) {
                return;
            }

            var amountToDisburse = this.DisbursementAmount;

            decimal totalAmountDisbursed = 0 ;
            foreach (var distribution in this.Distributions) {
                var amountDisbursed = distribution.Disburse(amountToDisburse);
                totalAmountDisbursed += amountDisbursed;
                amountToDisburse -= amountDisbursed;
            }
        }
    }
}