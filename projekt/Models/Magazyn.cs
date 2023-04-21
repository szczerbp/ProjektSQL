using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projekt.Models
{
    public class Magazyn
    {
        [Key]
        public long Id { get; set; }
        public string Miasto { get; set; }
        public double Powierzchnia { get; set; }
        public int PojemnoscPaczek { get; set; }
        public List<Paczka> Paczki { get; set; }
        public List<WozekWidlowy> WozkiWidlowe { get; set; }
    }
}
