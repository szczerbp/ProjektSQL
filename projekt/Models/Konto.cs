using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projekt.Models
{
    public class Konto
    {
        [Key] 
        public long Id { get; set; }
        public string Login { get; set; }
        public string Haslo { get; set; }
        public string TypKonta { get; set; }
        public Pracownik Pracownik { get; set; }
        public long PracownikId { get; set; }
        public List<Praca>? Praca { get; set; }

    }
}
