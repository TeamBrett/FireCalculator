using System;
using System.Collections.Generic;

namespace FireCalculator {
    public class Person : INamed {
        public Person(string name, DateTime birthday) {
            this.Name = name;
            this.Birthday = birthday;
        }

        public string Name { get; }

        public DateTime Birthday { get; set; }

        public int Age => (int)((Strategy.Now - this.Birthday.Date).TotalDays / 365);

        public List<Job> Jobs { get; set; } = new List<Job>();
    }
}