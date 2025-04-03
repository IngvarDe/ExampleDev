using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars.Core.Domain
{
    public class Car
    {
        public Guid Id { get; set; }
        public string CarMake { get; set; }

        public int Year { get; set; }

        public string CarColor { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime Modifieted { get; set; }
    }
}
