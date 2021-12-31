using cuenta_ahorro.EF.Entities;
using System.Collections.Generic;

namespace cuenta_ahorro.Models
{
    public class History
    {
        public double Deposit { get; set; }
        public double WithDraw { get; set; }
        public double TotalBalance { get; set; }
        public Person Client { get; set; }
        public OpeningSavingAccount OpeningSavingAccount { get; set; }
        public List<ManagementAccount> ManagementAccount { get; set; }
    }
}
