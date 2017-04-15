namespace FireCalculator {
    public class Taxes : INamed, IDisbursable {
        private readonly decimal rate;

        public Taxes(decimal taxRate) {
            this.rate = taxRate;
        }

        public decimal Disburse(decimal amount) {
            var taxes = amount * this.rate;
            this.PaymentCalendar.Add(taxes);
            return taxes;
        }

        public PaymentCalendar PaymentCalendar { get; set; } = new PaymentCalendar();

        public string Name { get; set; } = "Taxes";
    }
}