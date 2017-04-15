namespace FireCalculator {
    public class EquitiesAccount : Asset, IDisbursable {
        public EquitiesAccount(string name)
            : base(name) {
        }

        public decimal Disburse(decimal amount) {
            var amountToDisburse = amount;
            this.Value += amountToDisburse;
            return amountToDisburse;
        }
    }
}