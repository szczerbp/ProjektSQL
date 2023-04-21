using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projekt.Models
{
    public class Pracownik
    {
        [Key]
        public long Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public Konto Konto { get; set; }
        public long KontoId { get; set; }

    }
}
