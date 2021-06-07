using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ReservationFinal.DATA.EF
{
    #region Reservation Metadata
    public class ReservationMetadata
    {
        //public int ReservationID { get; set; }
        [Display(Name = "Owner Instrument")]
        [Required(ErrorMessage = "Owner Instrument is a required field.")]
        public int OwnerInstrumentID { get; set; }

        [Display(Name = "Location")]
        [Required(ErrorMessage = "Location is a required field.")]
        public int LocationID { get; set; }

        [Display(Name = "Reservation Date")]
        [Required(ErrorMessage = "Reservation Date is a required field.")]
        [DataType(DataType.Date)]
        public System.DateTime ReservationDate { get; set; }
    }

    [MetadataType(typeof(ReservationMetadata))]
    public partial class Reservation { }
    #endregion

    #region Location Metadata
    public class LocationMetadata
    {
        //public int LocationID { get; set; }
        [Display(Name = "Location Name")]
        [Required(ErrorMessage = "Location Name is a required field.")]
        [StringLength(50, ErrorMessage = "Location Name must be 50 characters or less.")]
        public string LocationName { get; set; }

        [Required(ErrorMessage = "Address is a required field.")]
        [StringLength(100, ErrorMessage = "Address must be 100 characters or less.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is a required field.")]
        [StringLength(100, ErrorMessage = "City must be 100 characters or less.")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is a required field.")]
        [StringLength(2, ErrorMessage = "State must be 2 characters or less.")]
        public string State { get; set; }

        [Required(ErrorMessage = "Zip Code is a required field")]
        [StringLength(5, ErrorMessage = "Zip Code must be 5 characters or less.")]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Reservation Limit is a required field.")]
        [Display(Name = "Reservation Limit")]
        public byte ReservationLimit { get; set; }
    }

    [MetadataType(typeof(LocationMetadata))]
    public partial class Location { }
    #endregion

    #region OwnerInstruments Metadata
    public class OwnerInstrumentsMetadata
    {
        //public int OwnerInstrumentID { get; set; }

        [Required(ErrorMessage = "Instrument Name is a required field.")]
        [StringLength(50, ErrorMessage = "Instrument Name must be 50 characters or less.")]
        [Display(Name = "Instrument Name")]
        public string InstrumentName { get; set; }

        [Required(ErrorMessage = "Owner is a required field.")]
        //[Display(Name = "Owner")] possibly change later
        public string OwnerID { get; set; }

        [DisplayFormat(NullDisplayText = "[N/A]")]
        [StringLength(50, ErrorMessage = "Instrument Photo must be 50 characters or less.")]
        public string InstrumentPhoto { get; set; }

        [Display(Name = "Special Notes")]
        [UIHint("MultilineText")]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public string SpecialNotes { get; set; }

        [Display(Name = "Active?")]
        [Required(ErrorMessage = "Active? is a required field.")]
        public bool IsActive { get; set; }

        [Display(Name = "Date Added")]
        [Required(ErrorMessage = "Date Added is a required field.")]
        [DataType(DataType.Date)]
        public System.DateTime DateAdded { get; set; }

        [Display(Name = "Instrument Type")]
        [Required(ErrorMessage = "Instrument Type is a required field.")]
        public int InstrumentTypeID { get; set; }
    }

    [MetadataType(typeof(OwnerInstrumentsMetadata))]
    public partial class OwnerInstrument { }
    #endregion

    #region InstrumentType Metadata
    public class InstrumentTypeMetadata
    {
        //public int InstrumentTypeID { get; set; }
        [Required(ErrorMessage = "Instrument Type Name is a required field.")]
        [Display(Name = "Instrument Type Name")]
        public string InstrumentTypeName { get; set; }
    }

    [MetadataType(typeof(InstrumentTypeMetadata))]
    public partial class InstrumentType { }
    #endregion

    #region UserDetails Metadata
    public class UserDetailsMetadata
    {
        //public string UserID { get; set; }
        [Required(ErrorMessage = "First Name is a required field.")]
        [Display(Name = "First Name")]
        [StringLength(50, ErrorMessage = "First Name must be 50 characters or less.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is a required field.")]
        [Display(Name = "Last Name")]
        [StringLength(50, ErrorMessage = "Last Name must be 50 characters or less.")]
        public string LastName { get; set; }
    }

    [MetadataType(typeof(UserDetailsMetadata))]
    public partial class UserDetail { }
    #endregion
}
