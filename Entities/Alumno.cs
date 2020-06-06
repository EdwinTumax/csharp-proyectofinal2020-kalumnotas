using System.ComponentModel.DataAnnotations;

namespace KalumNotas.Entities
{
    public class Alumno
    {
        public string Carne {get;set;}
        [Required]
        public string Apellidos {get;set;}
        [Required]
        public string Nombres {get;set;}
        public string NoExpediente {get;set;}
        [EmailAddress]
        public string Email {get;set;}
        [Required]
        public int ReligionId {get;set;}
        public Religion Religion {get;set;}
    }
}