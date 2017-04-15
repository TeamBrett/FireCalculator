using System;
using System.Collections.Generic;
using System.Linq;

namespace FireCalculator {
    public class PaymentCalendar {
        private readonly List<(DateTime Date, decimal Amount)> payments = new List<(DateTime, decimal)>();

        public void Add(decimal payment) {
            this.payments.Add((Strategy.Now, payment));
        }

        public decimal TotalForYear(int year) {
            return this.payments.Where(x => x.Date.Year == year).Select(x => x.Item2).Sum();
        }

        public decimal TotalForThisYear() {
            return this.TotalForYear(Strategy.Now.Year);
        }
    }
}