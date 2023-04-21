using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projekt.Models
{
    public class Firma
    {
        [Key]
        public long Id { get; set; }
        public string Nazwa { get; set; }
        public string NIP { get; set; }
        public List<Paczka> Paczki { get; set; }
    }
}
