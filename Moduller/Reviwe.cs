using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace umbraco_lingoquest.Moduller
{
    public class Reviwe
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }
    }

}

