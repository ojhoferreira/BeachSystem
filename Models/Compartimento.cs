using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ProvaDs2.Models
{
   public class Compartimento
    {
          [Key]
        public int NumeroId { get; set; }
        
        [Required]
        public string Tamanho { get; set; }
       
        [ForeignKey("Armario")]
        public int ArmarioId { get; set; }

        public bool Disponivel { get; set; } = true;
        
        [ForeignKey("Pessoa")]
        public int? PessoaId { get; set; }
    }
}