namespace MAUI_ProyectoAvance2.Models
{
    public class CategoriaViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public override string ToString() => Nombre;
    }
}
