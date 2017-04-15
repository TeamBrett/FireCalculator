namespace FireCalculator {
    public class CarLoan : Loan {
        public CarLoan(string name, Car car)
            : base(name) {
            this.Car = car;
        }

        public Car Car { get; set; }
    }
}