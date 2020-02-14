using System;
using System.ComponentModel.DataAnnotations;

namespace Coin.Web.Entities
{
    public class Download
    {
        [Key]
        public string Id { get; set; }
        public string Url { get; set; }
        public string ContentType { get; set; }
        public string Filename { get; set; }
        public string Extension { get; set; }

    }
}