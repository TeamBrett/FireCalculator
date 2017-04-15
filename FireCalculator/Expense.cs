using System;

namespace FireCalculator {
    public class Expense : INamed, IDisbursable {
        private decimal Amount { get; }
        private Period Period { get; }
        public Expense(string name, decimal amount, Period disbursementPeriod) {
            this.Name = name;
            this.Amount = amount;
            this.Period = disbursementPeriod;
        }

        public decimal Disburse(decimal amount) {
            if (!Strategy.Now.IsToday(this.Period)) {
                Console.WriteLine("disbursement period of expenses does not match job disbursement");
            }

            amount = amount > this.Amount ? this.Amount : amount;
            this.PaymentCalendar.Add(amount);
            return amount;
        }

        public PaymentCalendar PaymentCalendar { get; set; } = new PaymentCalendar();

        public string Name { get; set; }
    }
}