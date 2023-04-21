using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projekt.Models
{
    public class Paczka
    {
        [Key]
        public long Id { get; set; }
        public string Wlasciciel { get; set; }
        public Firma Firma { get; set; }
        public long FirmaId{ get; set; }
        public string Stan { get; set; }
        public Magazyn Magazyn { get; set; }
        public long MagazynId { get; set; }
    }
}
