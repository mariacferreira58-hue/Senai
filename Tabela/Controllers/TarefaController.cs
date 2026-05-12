using Microsoft.AspNetCore.Mvc;
using Senai.Data;
using Senai.Models;
namespace Senai.Controllers

{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly GerenciaContext _context;
        public TarefaController(GerenciaContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult RetornaReserva(int id)
        {
            var reservas = _context.Tarefas.Find(id);
            if (reservas == null)
            {
                return NotFound("Reserva não encontrada");
            }
            return Ok(reservas);
        }

        [HttpGet("tarefasUsuario/{id}")]
        public IActionResult TarefasUsuario(int identUsuario)
        {
            var resultado = from c in _context.Usuarios
                            join t in _context.Tarefas
                            on c.Id equals t.Id
                            where identUsuario == c.Id
                            select new
                            {
                                Usuario = c.Nome,
                                c.Email,
                                c.Senha,
                                Tarefas = t.Descricao,
                                t.Status,
                            };
            return Ok(resultado.ToList());
        }

        [HttpPost]
        public IActionResult CadastraReserva(Tarefa tarefa)
        {
            _context.Add(tarefa);
            _context.SaveChanges();
            return Created("", tarefa);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaTarefa(int id, Tarefa tarefa)
        {
            var reservaDoBanco = _context.Tarefas.Find(id);
            if (reservaDoBanco == null)
            {
                return NotFound("Reserva não existe no banco!");
            }
            reservaDoBanco.Descricao = tarefa.Descricao;
            reservaDoBanco.Status = tarefa.Status;
            reservaDoBanco.Id = tarefa.Id;
            _context.SaveChanges();
            return Ok("Atualizado");
        }
    }
}
