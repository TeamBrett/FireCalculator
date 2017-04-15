namespace FireCalculator {
    public abstract class Liability : INamed {
        protected Liability(string name) {
            this.Name = name;
        }

        public string Name { get; set; }

        public decimal Value { get; set; }
    }
}