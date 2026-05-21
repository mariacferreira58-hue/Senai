using Senai.Data;
using Senai.Models;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("tarefasUsuario")]
        public IActionResult TarefasUsuario()
        {
            var tarefaLogado = HttpContext.Session.GetString("IdLogado");
            if (tarefaLogado == null)
            {
                return Unauthorized("Faça login antes!");
            }
            var idUsuarioLogado = Request.Cookies["IdLogado"];

            if (idUsuarioLogado != null)
            {
                Console.WriteLine("TESTE: " + int.Parse(idUsuarioLogado));
                var resultado = from c in _context.Usuarios
                                join r in _context.Tarefas
                                on c.Id equals r.IdUsuario
                                where c.Id == int.Parse(idUsuarioLogado)
                                select new
                                {
                                    Usuario = c.Nome,
                                    c.Email,
                                    Tarefas = r.Id,
                                    r.Descricao,
                                    r.Status
                                };
                return Ok(resultado.ToList());
            }
            return Unauthorized("Faça login antes!");
        }


        [HttpPost]
        public IActionResult CadastraTarefa(Tarefa tarefa)
        {
            var tarefaLogado = HttpContext.Session.GetString("IdLogado");
            if (tarefaLogado == null)
            {
                return Unauthorized("Faça login antes!");
            }
            var idTarefaLogado = Request.Cookies["IdLogado"];
            if (idTarefaLogado != null)
                tarefa.IdUsuario = int.Parse(idTarefaLogado);

            _context.Add(tarefa);
            _context.SaveChanges();
            return Created("", tarefa);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaTarefa(int id, Tarefa tarefa)
        {
            var usuarioLogado = HttpContext.Session.GetString("IdLogado");
            if (usuarioLogado == null)
            {
                return Unauthorized("Faça login antes!");
            }

            var tarefaDoBanco = _context.Tarefas.Find(id);
            if (tarefaDoBanco == null)
            {
                return NotFound("Tarefa não existe no banco!");
            }
            tarefaDoBanco.Descricao = tarefa.Descricao;
            tarefaDoBanco.Status = tarefa.Status;
           
            _context.SaveChanges();
            return Ok("Atualizado");
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeletaTarefa(int id)
        {
            var tarefaLogado = HttpContext.Session.GetString("IdLogado");
            if (tarefaLogado == null)
            {
                return Unauthorized("Faça login antes!");
            }
            var tarefaDoBanco = _context.Tarefas.Find(id);
            if (tarefaDoBanco == null)
            {
                return NotFound("Não encontrado!");
            }
            _context.Remove(tarefaDoBanco);
            _context.SaveChanges();
            return Ok("Deletado");
        }
    }
}
