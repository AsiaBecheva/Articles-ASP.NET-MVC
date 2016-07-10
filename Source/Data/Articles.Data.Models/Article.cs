namespace Articles.Data.Models
{
    using Common.Models;
    using System.Collections.Generic;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Article : IAuditInfo
    {
        public Article()
        {
            this.CreatedOn = DateTime.Now;
        }

        public int Id { get; set; }

        [MaxLength(200)]
        public string Title { get; set; }

        [MaxLength(7000)]
        public string Content { get; set; }

        public virtual ICollection<File> Images { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool PreserveCreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string AuthorId { get; set; }

        public virtual User Author { get; set; }
    }
}
