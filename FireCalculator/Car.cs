using System.Collections.Generic;

namespace FireCalculator {
    public class Car : Asset {
        public Car(string name)
            : base(name) {
        }

        public List<CarLoan> Loans { get; set; }
    }
}