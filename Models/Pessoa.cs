using System;
using System.ComponentModel.DataAnnotations;


namespace ProvaDs2.Models
{ 
    public class Pessoa
    {
        [Key]
        public int PessoaId { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string CPF { get; set; }
        [Required]
        public string Email { get; set; }

        public Compartimento Compartimento { get; set; }

    }
}