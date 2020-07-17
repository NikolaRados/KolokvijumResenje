using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataTransfer
{
    public class ReadInvoice
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string BillingAddress { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingCountry { get; set; }
        public string BillingPostalCode { get; set; }
        public virtual ICollection<ReadInvoiceLineDto> InvoiceLine { get; set; }
    }
}
