using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FireCalculator {
    public enum Period {
        Annually = 1,
        Quarterly = 4,
        Monthly = 12,
        Fortnightly = 24,
        Weekly = 52
    }

    public interface INamed {
        string Name { get; }
    }

    public abstract class Asset : INamed {
        protected Asset(string name) {
            this.Name = name;
        }

        public string Name { get; set; }
        public decimal Value { get; set; }
    }

    public abstract class Liability : INamed {
        protected Liability(string name) {
            this.Name = name;
        }

        public string Name { get; set; }
    }

    public class Strategy {
        public List<Asset> Assets { get; set; }
        public List<Liability> Liabilities { get; set; }
        public List<Person> People { get; set; }
    }

    public class FourOhOneKay : Asset {
        public FourOhOneKay(string name)
            : base(name) {
        }
    }

    public class TraditionalIra : Asset {
        public TraditionalIra(string name)
            : base(name) {
        }
    }

    public class RothIra : Asset {
        public RothIra(string name)
            : base(name) {
        }
    }

    public class Prosper : Asset {
        public Prosper(string name)
            : base(name) {
        }
    }

    public class Car : Asset {
        public Car(string name)
            : base(name) {
        }

        public List<CarLoan> Loans { get; set; }
    }

    public class House : Asset {
        public House(string name)
            : base(name) {
        }

        public List<Mortgage> Mortgages { get; set; }
    }

    public class Job {
        private decimal salary;

        public decimal Salary {
            get => this.salary / (int)this.Period;
            set => this.salary = value * (int)this.Period;
        }

        public Period Period { get; set; } = Period.Annually;

        public Person Person { get; set; }
    }

    public class Person : INamed {
        public Person(string name) {
            this.Name = name;
        }

        public string Name { get; }

        public List<Job> Jobs { get; set; }
    }

    public abstract class Loan : Liability {
        protected Loan(string name)
            : base(name) {
        }

        public decimal DownPayment { get; set; }

        public decimal InterestRate { get; set; }

        public decimal Principal { get; set; }

        public int Years { get; set; }
    }

    public class CarLoan : Loan {
        public CarLoan(string name, Car car)
            : base(name) {
            this.Car = car;
        }

        public Car Car { get; set; }
    }

    public class Mortgage : Loan {
        public Mortgage(string name, House house)
            : base(name) {
            this.House = house;
        }

        public decimal MortgageInsurance { get; set; }

        public decimal ClosingCosts { get; set; }

        public decimal Taxes { get; set; }

        public House House { get; set; }
    }

    public class Program {
        public static void Main(string[] args) {
            var house = new House("Home");
            var car = new Car("BMW Z4");
                    
            var strategy = new Strategy {
                People = new List<Person> {
                    new Person("Brett"),
                    new Person("Bre")
                },
                Assets = new List<Asset> {
                    house,
                    car,
                    new RothIra("TradeKing Roth IRA"),
                    new TraditionalIra("TradeKing Traditional IRA"),
                    new Prosper("Prosper")
                },
                Liabilities = new List<Liability> {
                    new Mortgage("Home Mortgage", house),
                    new CarLoan("Alliant Z4 Loan", car)
                }
            };

            var program = new Program();

            StringBuilder log = new StringBuilder();
            foreach (Mortgage mortgage in strategy.Liabilities.OfType<Mortgage>()) {
                log.Append(program.Calculate(mortgage));
            }

            Console.Write(log);

            string line;
            do {
                line = Console.ReadLine();
                Console.WriteLine(line);
            } while (line.ToLower().Trim() != "quit");
        }

        private string Calculate(Mortgage vars) {
            vars.DownPayment = vars.DownPayment / 100;
            this.CalculateMortgage(vars);

            var totals = new StringBuilder();
            vars.DownPayment = .05m;
            totals.AppendLine("Total @  5%: " + this.CalculateMortgage(vars));
            vars.DownPayment = .10m;
            totals.AppendLine("Total @ 10%: " + this.CalculateMortgage(vars));
            vars.DownPayment = .15m;
            totals.AppendLine("Total @ 15%: " + this.CalculateMortgage(vars));
            vars.DownPayment = .20m;
            totals.AppendLine("Total @ 20%: " + this.CalculateMortgage(vars));
            vars.DownPayment = .25m;
            totals.AppendLine("Total @ 25%: " + this.CalculateMortgage(vars));
            return totals.ToString();
        }

        private string CalculateMortgage(Mortgage vars) {
            var downPaymentPercentage = vars.DownPayment / 100;
            vars.MortgageInsurance = vars.Principal * (decimal).0075 / 12;
            var rate = vars.InterestRate / 100 / 12;
            var downPayment = vars.Principal * downPaymentPercentage;
            var houseCost = vars.Principal;
            var principal = houseCost - downPayment;
            var months = vars.Years * 12;
            var insurance = vars.MortgageInsurance / 12;
            var taxes = vars.Taxes / 12;
            var pmi = vars.MortgageInsurance;

            var numerator = rate * Convert.ToDecimal(Math.Pow((double)(1 + rate), months));
            var denominator = Convert.ToDecimal(Math.Pow((double)(1 + rate), months) - 1);

            var payment = principal * numerator / denominator;

            var log = new StringBuilder();
            log.AppendLine($"Down Payment: {downPayment:C}");
            log.AppendLine($"Payment: {payment:C}");
            log.AppendLine($"Insurance: {insurance:C}");
            log.AppendLine($"Taxes: {taxes:C}");
            log.AppendLine($"PMI: {pmi:C}");
            log.AppendLine($"Total: {(payment + insurance + taxes + pmi):C}");

            decimal totalInterest = 0;
            decimal totalPrincipal = 0;
            decimal totalPmi = 0;
            for (int i = 0; i < months; i++) {
                var interestPayment = principal * rate;
                var principalPayment = payment - interestPayment;

                totalInterest += interestPayment;
                totalPrincipal += principalPayment;

                var twentyPercent = houseCost * .20m;
                var ownedValue = downPayment + totalPrincipal;
                if (ownedValue < twentyPercent) {
                    totalPmi += pmi;
                }

                principal = principal - principalPayment;
            }

            var total = totalPmi + totalPrincipal + totalInterest;

            log.AppendLine($"Total Principal: {totalPrincipal:C}");
            log.AppendLine($"Total Interest: {totalInterest:C}");
            log.AppendLine($"Total PMI: {totalPmi:C}");
            log.AppendLine($"Total: {total:C}");

            ////var monthsSaving = downPayment / (double)SavingsPerMonthInput.Value;
            ////var totalRentWhileSaving = monthsSaving * (double)RentPerMonthInput.Value;

            ////log = new StringBuilder();
            ////log.AppendLine($"Months Saving: {monthsSaving}");
            ////log.AppendLine($"Rent Cost: {totalRentWhileSaving:C}");

            return log.ToString();
        }
    }
}