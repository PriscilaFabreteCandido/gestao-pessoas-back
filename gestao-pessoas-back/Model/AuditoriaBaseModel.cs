namespace gestao_pessoas_back.Model
{
    public class AuditoriaBaseModel
    {
        public string? CriadoPor { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
        public string? AtualizadoPor { get; set; }
        public DateTime? DataAtualizacao { get; set; }
    }
}
