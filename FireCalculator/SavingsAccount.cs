namespace FireCalculator {
    public class SavingsAccount : Asset {
        public SavingsAccount(string name)
            : base(name) {
        }

        public decimal InterestRate { get; set; }
    }
}