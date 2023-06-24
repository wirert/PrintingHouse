using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace PrintingHouse.Infrastructure.Data.Entities
{
    [Comment("Connecting table between machines and articles (many to many)")]
    public class MachineArticle
    {
        [Comment("Machine primary key")]
        public int MachineId { get; set; }

        [ForeignKey(nameof(MachineId))]
        public Machine Machine { get; set; } = null!;

        [Comment("Article primary key")]
        public int ArticleId { get; set; }

        [ForeignKey(nameof(ArticleId))]
        public Article Article { get; set; } = null!;
    }
}
