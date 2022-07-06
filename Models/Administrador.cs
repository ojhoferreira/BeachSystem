using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProvaDs2.Models
{
    public class Administrador : Pessoa
    {
        ICollection<Armario> MeusArmarios { get; set; }
    }

}