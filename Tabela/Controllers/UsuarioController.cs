using Microsoft.AspNetCore.Mvc;
using Senai.Data;
using Senai.Models;
namespace Senai.Controllers

{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly GerenciaContext _context;
        public UsuarioController(GerenciaContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult RetornaUsuario(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario == null)
            {
                return NotFound("Não há usuarios com esse id!");
            }
            return Ok(usuario);
        }

        [HttpPost("cadastrar")]
        public IActionResult CadastraUsuario(Usuario usuario)
        {
            _context.Add(usuario);
            _context.SaveChanges();
            return Created("", usuario);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaUsuario(int id, Usuario usuario)
        {
            var usuarioDoBanco = _context.Usuarios.Find(id);
            if (usuarioDoBanco == null)
            {
                return NotFound("Usuario não existe no banco!");
            }
            usuarioDoBanco.Nome = usuario.Nome;
            usuarioDoBanco.Email = usuario.Email;
            usuarioDoBanco.Senha = usuario.Senha;
            _context.SaveChanges();
            return Ok("Atualizado");
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaUsuario(int id)
        {
            var usuarioDoBanco = _context.Usuarios.Find(id);
            if (usuarioDoBanco == null)
            {
                return NotFound("Não encontrado!");
            }
            _context.Remove(usuarioDoBanco);
            _context.SaveChanges();
            return Ok("Deletado");
        }

        [HttpPost("login")]
        public IActionResult Login(Usuario usuario)
        {
            string senhaPadrao = usuario.Email;
            string emailPadrao = usuario.Senha;

            if (!(usuario.Senha == senhaPadrao && usuario.Email == emailPadrao))
            {
                return Unauthorized("Usuário ou senha inválidos!");
            }

            HttpContext.Session.SetString("Emaillogin", usuario.Email);
            Response.Cookies.Append("Usuario", "Clara",
                new CookieOptions
                {
                    Expires = DateTime.Now.AddMinutes(10),
                    HttpOnly = true
                });

            return Ok("login realizado com sucesso");
        }

    }
}