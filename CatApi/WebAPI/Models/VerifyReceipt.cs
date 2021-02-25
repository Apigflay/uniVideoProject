using System;

namespace WebAPI
{
    /// <summary>
    /// apple 票据验证返回结果实体
    /// </summary>
    public class VerifyReceipt
    {
        public int Status { get; set; }
        public string Environment { get; set; }
        public ReceiptDetails Receipt { get; set; }
    }

    public class ReceiptDetails
    {
        //        receipt_type: "ProductionSandbox",
        public string receipt_type { get; set; }
        //adam_id: 0,
        public int adam_id { get; set; }
        //app_item_id: 0,
        public int app_item_id { get; set; }
        //bundle_id: "com.tange.xinyiba",
        public string bundle_id { get; set; }
        //application_version: "1.0",
        public string application_version { get; set; }
        //download_id: 0,
        public Int64 download_id { get; set; }
        //version_external_identifier: 0,
        public int version_external_identifier { get; set; }
        //receipt_creation_date: "2015-06-05 06:15:25 Etc/GMT",
        public string receipt_creation_date { get; set; }
        //receipt_creation_date_ms: "1433484925000",
        public string receipt_creation_date_ms { get; set; }
        //receipt_creation_date_pst: "2015-06-04 23:15:25 America/Los_Angeles",
        public string receipt_creation_date_pst { get; set; }
        //request_date: "2015-11-30 05:37:09 Etc/GMT",
        public string request_date { get; set; }
        //request_date_ms: "1448861829765",
        public string request_date_ms { get; set; }
        //request_date_pst: "2015-11-29 21:37:09 America/Los_Angeles",
        public string request_date_pst { get; set; }
        //original_purchase_date: "2013-08-01 07:00:00 Etc/GMT",
        public string original_purchase_date { get; set; }
        //original_purchase_date_ms: "1375340400000",
        public string original_purchase_date_ms { get; set; }
        //original_purchase_date_pst: "2013-08-01 00:00:00 America/Los_Angeles",
        public string original_purchase_date_pst { get; set; }
        //original_application_version: "1.0",
        public string original_application_version { get; set; }
        public AppDetails[] In_app { get; set; }
    }

    public class AppDetails
    {
        //        quantity: "1",
        public string quantity { get; set; }
        //product_id: "com.tange.xinyiba.coinLevel1",
        public string product_id { get; set; }
        //transaction_id: "1000000158067921",
        public string transaction_id { get; set; }
        //original_transaction_id: "1000000158067921",
        public string original_transaction_id { get; set; }
        //purchase_date: "2015-06-05 06:15:22 Etc/GMT",
        public string purchase_date { get; set; }
        //purchase_date_ms: "1433484922000",
        public string purchase_date_ms { get; set; }
        //purchase_date_pst: "2015-06-04 23:15:22 America/Los_Angeles",
        public string purchase_date_pst { get; set; }
        //original_purchase_date: "2015-06-05 06:15:22 Etc/GMT",
        public string original_purchase_date { get; set; }
        //original_purchase_date_ms: "1433484922000",
        public string original_purchase_date_ms { get; set; }
        //original_purchase_date_pst: "2015-06-04 23:15:22 America/Los_Angeles",
        public string original_purchase_date_pst { get; set; }
        //is_trial_period: "false"
        public bool is_trial_period { get; set; }

    }
}