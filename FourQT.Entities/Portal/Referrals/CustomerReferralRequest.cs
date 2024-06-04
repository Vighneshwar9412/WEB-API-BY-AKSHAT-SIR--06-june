using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities.Portal.Referrals
{
    public interface CustomerReferralIds
    {
        public int customerId { get; set; }
        public int projectId { get; set; }
    }

    public interface CustomerReferralCore
    {
        public string? refFullName { get; set; }
        public string? refContactNo { get; set; }
        public string? refMailId { get; set; }
        public string? refAddress { get; set; }
        public string? refRelation { get; set; }
        public string? location { get; set; }
    }

    public interface CustomerReferralAdd
    {
        public int refId { get; set; }
        public string? refContactNo2 { get; set; }
        public string? projectName { get; set; }
        public string? refDate { get; set; }
    }

    public class CustomerReferralRequest : CustomerReferralIds, CustomerReferralCore
    {
        public int customerId { get; set; }
        public int projectId { get; set; }
        public string? refFullName { get; set; }
        public string? refContactNo { get; set; }
        public string? refMailId { get; set; }
        public string? refAddress { get; set; }
        public string? refRelation { get; set; }
        public string? location { get; set; }
        public string? remarks { get; set; }
    }

    public class CustomerReferralDetails: CustomerReferralCore, CustomerReferralAdd
    {
        public int refId { get; set; }
        public string? projectName { get; set; }
        public string? refFullName { get; set; }
        public string? refContactNo { get; set; }
        public string? refContactNo2 { get; set; }
        public string? refMailId { get; set; }
        public string? refAddress { get; set; }
        public string? refRelation { get; set; }
        public string? location { get; set; }
        public string? refDate { get; set; }
        public string? status { get; set; }

        public string? dumpReason { get; set; }
        public string? dumpDate { get; set; }

        public string? successCustomerName { get; set; }
        public string? successBookingDate { get; set; }
        public string? successProject { get; set; }
        public string? successUnitNo { get; set; }
        public string? successArea { get; set; }

        public string? allocatedAmount { get; set; }
        public string? allocatedDate { get; set; }

        public string? paidAmount { get; set; }
        public string? paidReceiptNo { get; set; }
        public string? paidUnitNo { get; set; }
        public string? paidProject { get; set; }
        public string? paidDate { get; set; }
    }
}
