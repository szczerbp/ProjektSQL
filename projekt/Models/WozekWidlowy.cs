using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projekt.Models
{
    public class WozekWidlowy
    {
        [Key]
        public long Id { get; set; }
        public DateTime DataOstatniegoSerwisu { get; set; }
        public Magazyn Magazyn { get; set; }
        public long MagazynId { get; set; }
        public List<Praca> Prace { get; set; }

    }
}
