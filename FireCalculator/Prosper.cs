namespace FireCalculator {
    public class Prosper : Asset, IDisbursable {
        public Prosper(string name)
            : base(name) {
        }

        public decimal Disburse(decimal amount) {
            var amountToDisburse = amount <= 100 ? amount : 100;
            this.Value += amountToDisburse;
            return amountToDisburse;
        }
    }
}