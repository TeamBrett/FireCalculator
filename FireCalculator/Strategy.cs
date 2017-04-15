using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace FireCalculator {
    public class Strategy {
        private static readonly AsyncLocal<DateTime> GlobalNow = new AsyncLocal<DateTime>();
        static Strategy() {
            GlobalNow.Value = DateTime.Today;
        }

        public static DateTime Now => GlobalNow.Value;
        public DateTime StartDate { get; set; } = DateTime.Now.Date;
        public List<Asset> Assets { get; set; }
        public List<Liability> Liabilities { get; set; }
        public List<Person> People { get; set; }

        public decimal NetWorth => this.Liabilities.Sum(l => l.Value) - this.Assets.Sum(a => a.Value);

        public void Run(DateTime date) {
            GlobalNow.Value = this.StartDate;

            while (Now <= date) {
                foreach (var job in this.People.SelectMany(x => x.Jobs)) {
                    job.Disburse();
                }

                ////foreach (var asset in this.Assets) {
                ////    asset.Run();
                ////}

                ////foreach (var liability in this.Liabilities) {
                ////    liability.Run();
                ////}
                
                Console.WriteLine(Now.ToString(DateTimePattern.ShortDate));
                GlobalNow.Value = Now.AddDays(1);
            }
        }
    }
}