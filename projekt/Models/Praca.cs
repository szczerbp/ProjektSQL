using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projekt.Models
{
    public class Praca
    {
        [Key]
        public long Id { get; set; }
        public DateTime CzasRozpoczecia { get; set; }
        public DateTime CzasZakonczenia { get; set; }
        public List<Paczka> PrzewiezionePaczki { get; set; }
        public WozekWidlowy WozekWidlowy { get; set; }
        public long WozekWidlowyId { get; set; }
        public Konto Konto { get; set; }
        public long KontoId { get; set; }

    }
}
