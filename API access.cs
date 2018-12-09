using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;
using System.Runtime.InteropServices;

namespace irdAPI
{
    public class API
    {

        public API()
        {
        }

        public class BillReturnViewModel
        {
            public string username { get; set; }
            public string password { get; set; }
            public string seller_pan { get; set; }
            public string buyer_pan { get; set; }
            public string fiscal_year { get; set; }
            public string buyer_name { get; set; }
            public string ref_invoice_number { get; set; }
            public string invoice_number { get; set; }
            public string invoice_date { get; set; }
            public string credit_note_number { get; set; }
            public string credit_note_date { get; set; }
            public string reason_for_return { get; set; }
            public Double total_sales { get; set; }
            public Double taxable_sales_vat { get; set; }
            public Double vat { get; set; }
            public Double excisable_amount { get; set; }
            public Double excise { get; set; }
            public Double taxable_sales_hst { get; set; }
            public Double hst { get; set; }
            public Double amount_for_esf { get; set; }
            public Double esf { get; set; }
            public Double export_sales { get; set; }
            public Double tax_exempted_sales { get; set; }
            public bool isrealtime { get; set; }
            public DateTime datetimeClient { get; set; }
        }


        public string bill()
        {
            var client = new HttpClient();


            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            BillReturnViewModel P = new BillReturnViewModel
            {
                username = "Test_CBMS",
                //username = "Nobel",
                password = "test@321",
                seller_pan = "999999999",
                buyer_pan = "123456789",
                buyer_name = "",
                fiscal_year = "2073.074",
                invoice_date = "2074.07.16",
                total_sales = 20000,
                vat = 1000,
                excisable_amount = 0,
                excise = 0,
                taxable_sales_hst = 0,
                hst = 0,
                amount_for_esf = 0,
                esf = 0,
                export_sales = 0,
                tax_exempted_sales = 0,
                isrealtime = true,
                datetimeClient = DateTime.Now
            };

            P.invoice_number = Console.ReadLine();

            client.BaseAddress = new Uri("http://202.166.207.75:9050");

            try
            {
                var response = client.PostAsJsonAsync("api/bill", P).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync();
                    string c = result.Result;
                    switch (c)
                    {

                        case "104":
                            return ("Model Invalid. " + c);

                        case "200":
                            return ("Bill Successfully written. " + c);

                        case "102":
                            return ("Exception while saving bill : Please check fields. " + c);

                        case "101":
                            return ("Bill already exists! " + c);

                        case "100":
                            return ("API credential Error " + c);

                        case "103":
                            return ("Unknown Exception occured Server Side. " + c);

                        case "105":
                            return ("Bill already exists! " + c);


                        default:
                            return ("API return ID not recognized " + c);

                    }
                }
                else return ("Unknown Error");

            }

            catch (Exception ex) { return Convert.ToString(ex); Console.ReadKey(); }
        }
    }
};







