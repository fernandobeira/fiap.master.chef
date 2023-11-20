namespace fiap.master.chef.core.Models
{
    public class Receita
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public IList<Ingrediente> Ingredientes { get; set; }
        public string ModoDePreparo { get; set; }
        public string Foto { get; set; }
        public string Categoria { get; set; }
        public IList<Tag> Tags { get; set; }

    }
}
