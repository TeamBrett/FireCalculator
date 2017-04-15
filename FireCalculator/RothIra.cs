namespace FireCalculator {
    public class RothIra : Ira, IDisbursable {
        public RothIra(string name)
            : base(name) {
        }

        public decimal Disburse(decimal amount) {
            return 0;
        }
    }
}