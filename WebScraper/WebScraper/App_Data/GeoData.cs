namespace WebScraper.App_Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GeoData")]
    public partial class GeoData
    {
        public int Id { get; set; }

        [StringLength(5)]
        public string PostalCode { get; set; }

        [StringLength(100)]
        public string State { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }
    }
}
