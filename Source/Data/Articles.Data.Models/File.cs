namespace Articles.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class File
    {
        public int FileId { get; set; }

        [StringLength(255)]
        public string FileName { get; set; }

        [StringLength(100)]
        public string ContentType { get; set; }

        public byte[] Content { get; set; }

        public FileType FileType { get; set; }

        public int ArticleId { get; set; }

        public virtual Article Article { get; set; }
    }
}