using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Models
{
    public class PaymentResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
        public data data { get; set; }
    }

    public class custom_field
    {
        public string display_name { get; set; }
        public string variable_name { get; set; }
        public string value { get; set; }
    }

    public class metadata
    {
        public List<custom_field> custom_fields { get; set; }
        public string referrer { get; set; }
    }

    public class history
    {
        public string type { get; set; }
        public string message { get; set; }
        public int time { get; set; }
    }

    public class log
    {
        public int time_spent { get; set; }
        public int attempts { get; set; }
        public object authentication { get; set; }
        public int errors { get; set; }
        public bool success { get; set; }
        public bool mobile { get; set; }
        public List<object> input { get; set; }
        public object channel { get; set; }
        public List<history> history { get; set; }
    }

    public class authorization
    {
        public string authorization_code { get; set; }
        public string bin { get; set; }
        public string last4 { get; set; }
        public string exp_month { get; set; }
        public string exp_year { get; set; }
        public string channel { get; set; }
        public string card_type { get; set; }
        public string bank { get; set; }
        public string country_code { get; set; }
        public string brand { get; set; }
        public bool reusable { get; set; }
        public string signature { get; set; }
    }

    public class photo
    {
        public string type { get; set; }
        public string typeId { get; set; }
        public string typeName { get; set; }
        public string url { get; set; }
        public bool isPrimary { get; set; }
    }

    public class metadata2
    {
        public List<photo> photos { get; set; }
    }

    public class customer
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string customer_code { get; set; }
        public object phone { get; set; }
        public metadata2 metadata { get; set; }
        public string risk_action { get; set; }
    }

    public class plan_object
    {
    }

    public class subaccount
    {
    }

    public class data
    {
        public int id { get; set; }
        public string domain { get; set; }
        public string status { get; set; }
        public string reference { get; set; }
        public int amount { get; set; }
        public object message { get; set; }
        public string gateway_response { get; set; }
        public DateTime paid_at { get; set; }
        public DateTime created_at { get; set; }
        public string channel { get; set; }
        public string currency { get; set; }
        public string ip_address { get; set; }
        public metadata metadata { get; set; }
        public log log { get; set; }
        public int fees { get; set; }
        public object fees_split { get; set; }
        public authorization authorization { get; set; }
        public customer customer { get; set; }
        public object plan { get; set; }
        public DateTime paidAt { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime transaction_date { get; set; }
        public plan_object plan_object { get; set; }
        public subaccount subaccount { get; set; }
    }
}
