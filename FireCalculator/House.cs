using System.Collections.Generic;

namespace FireCalculator {
    public class House : Asset {
        public House(string name)
            : base(name) {
        }

        public List<Mortgage> Mortgages { get; set; }
    }
}