using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ProvaDs2.Models
{
    public class Armario
    {
        [Key]
        public int ArmarioId { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public int PontoX { get; set; }

        [Required]
        public int PontoY { get; set; }

        [ForeignKey("Administrador")]
        public int AdministradorId { get; set; }

        public ICollection<Compartimento> Comp { get; set; }

        public int CompartimentosDisponiveis()
        {
            return Comp.Count(Comp => Comp.Disponivel);
        }
    }
}
