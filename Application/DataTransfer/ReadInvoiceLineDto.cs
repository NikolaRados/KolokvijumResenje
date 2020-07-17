using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataTransfer
{
    public class ReadInvoiceLineDto
    {
        public int Id { get; set; }
        public string TrackName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
