using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBook_RestSharpAPI
{
    public class AddressBook
    {
        public int id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public double Zip { get; set; }
        public string PhoneNumber { get; set; }
    }
}
