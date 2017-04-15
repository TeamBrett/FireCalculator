namespace FireCalculator {
    public abstract class Asset : INamed {
        protected Asset(string name) {
            this.Name = name;
        }

        public string Name { get; set; }
        public decimal Value { get; set; }
        public (decimal Limit, Period period) Limit { get; set; }
    }
}