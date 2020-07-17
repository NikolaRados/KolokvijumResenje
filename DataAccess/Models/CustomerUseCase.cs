using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class CustomerUseCase
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int CustomerCaseId { get; set; }
        public Customer Customer { get; set; }
    }
}
