namespace Senai.Models
{
    public class Tarefa
    {
        public int Id { get; set; }
        public int Descricao { get; set; }
        public int Status { get; set; }
        public int UsuarioId { get; set; }

        public Tarefa(int id, int descricao, int status, int usuarioId)
        {
            Id = id;
            Descricao = descricao;
            Status = status;
            UsuarioId = usuarioId;
        }
    }
}
