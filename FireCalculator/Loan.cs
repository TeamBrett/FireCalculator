using System;

namespace FireCalculator {
    public abstract class Loan : Liability {
        protected Loan(string name)
            : base(name) {
        }

        public decimal DownPayment { get; set; }

        public decimal InterestRate { get; set; }

        public decimal Principal { get; set; }

        public int Years { get; set; }

        public DateTime StartDate { get; set; }
    }
}