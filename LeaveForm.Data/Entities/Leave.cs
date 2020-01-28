using System;
using System.ComponentModel.DataAnnotations;

namespace LeaveForm.Data.Entities
{
    public enum LeaveTypes
    {
        [Display(Name = "Other Leave")]
        Other = 0,
        [Display(Name = "Sick Leave")]
        Sick,
        [Display(Name = "Business Leave")]
        Business
    }

    public enum LeaveStatuses
    {
        [Display(Name = "New")]
        New = 0,
        [Display(Name = "Approved")]
        Approved,
        [Display(Name = "Rejected")]
        Rejected,
        [Display(Name = "Canceled")]
        Canceled
    }
    public class Leave
    {
        [Key]
        public Guid Id { get; set; }

        public virtual Employee Employee { get; set; }

        [EnumDataType(typeof(LeaveTypes))]
        public LeaveTypes Type { get; set; }

        [EnumDataType(typeof(LeaveStatuses))]
        public LeaveStatuses Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public DateTime LeaveFrom { get; set; }
        public DateTime LeaveTo { get; set; }
        public float NumberOfLeaveDays { get; set; }
        public string Reason { get; set; }
    }
}
