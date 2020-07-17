using Application.DataTransfer;
using Application.Queries;
using Application.Search;
using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Queries
{
    public class EfGetInvoiceQuery : IGetInvoiceQuery
    {
        private readonly ChinookContext _context;

        public EfGetInvoiceQuery(ChinookContext context)
        {
            _context = context;
        }

        public int Id => 1;

        public string Name => "Search invoices";

        public PagedResponse<ReadInvoice> Execute(InvoiceSearch search)
        {
            var query = _context.Invoice.Include(x => x.Customer).Include(x => x.InvoiceLine).ThenInclude(x => x.Track).AsQueryable();

            if (!string.IsNullOrEmpty(search.Company) || !string.IsNullOrWhiteSpace(search.Company))
            {
                query = query.Where(x => x.Customer.Company.ToLower().Contains(search.Company.ToLower()));
            }

            if (search.MaxTotal.HasValue)
            {
                query = query.Where(x => x.Total <= search.MaxTotal);
            }

            if (search.MinTotal.HasValue)
            {
                query = query.Where(x => x.Total >= search.MinTotal);
            }

            if (!string.IsNullOrEmpty(search.Name) || !string.IsNullOrWhiteSpace(search.Name))
            {
                query = query.Where(x => x.InvoiceLine.Any(t => t.Track.Name.ToLower().Contains(search.Name.ToLower())));
            }

            if (!string.IsNullOrEmpty(search.Country) || !string.IsNullOrWhiteSpace(search.Country))
            {
                query = query.Where(x => x.Customer.Country.ToLower().Contains(search.Country.ToLower()));
            }

            if (search.CustomerId.HasValue)
            {
                query = query.Where(x => x.CustomerId == search.CustomerId);
            }

            var skipCount = search.PerPage * (search.Page - 1);

            var response = new PagedResponse<ReadInvoice>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = query.Count(),
                Items = query.Skip(skipCount).Take(search.PerPage).Select(x => new ReadInvoice
                {
                    Id = x.InvoiceId,
                    FirstName = x.Customer.FirstName,
                    LastName = x.Customer.LastName,
                    Email = x.Customer.Email,
                    Phone = x.Customer.Phone,
                    BillingAddress = x.BillingAddress,
                    BillingCity = x.BillingCity,
                    BillingCountry = x.BillingCountry,
                    BillingPostalCode = x.BillingPostalCode,
                    BillingState = x.BillingState,
                    InvoiceLine = x.InvoiceLine.Select(il => new ReadInvoiceLineDto
                    { 
                        Id = il.InvoiceLineId,
                        TrackName = il.Track.Name,
                        Quantity = il.Quantity,
                        UnitPrice = il.UnitPrice
                    }).ToList()
                }).ToList()
        };


            return response;
        }
    }
}
