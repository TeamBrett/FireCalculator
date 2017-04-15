using System.Threading;

namespace FireCalculator {
    public abstract class Ira : Asset {
        private static AsyncLocal<PaymentCalendar> ContributionsValue { get; } = new AsyncLocal<PaymentCalendar> {
            Value = new PaymentCalendar()
        };

        private static PaymentCalendar Contributions => ContributionsValue.Value; 

        protected Ira(string name) : base(name) {
        }

        protected static decimal GetContributionLimit(int year) {
            var contributed = Contributions.TotalForThisYear();
            return 5500 - contributed;
        }

        protected static void AddContribution(int year, decimal contribution) {
            Contributions.Add(contribution);
        }
    }

    public class TraditionalIra : Ira, IDisbursable {
        public TraditionalIra(string name)
            : base(name) {
        }

        public decimal Disburse(decimal amount) {
            var year = Strategy.Now.Year;

            var limit = GetContributionLimit(Strategy.Now.Year);

            var amountToDisburse = amount <= limit ? amount : limit;

            this.Value += amountToDisburse;

            AddContribution(year, amountToDisburse);

            return amountToDisburse;
        }
    }
}