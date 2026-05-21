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

        [HttpPost("login")]
        public IActionResult Login(Usuario usuario)
        {
            var usuarioBanco = _context.Usuarios.Where(c =>
                c.Email.Equals(usuario.Email) &&
                c.Senha.Equals(usuario.Senha)).ToList();

            if (usuarioBanco.Count == 0)
                return NotFound("Email ou senha incorretos");

            HttpContext.Session.SetString("IdLogado", usuarioBanco[0].Id.ToString());

            Response.Cookies.Append("IdLogado", usuarioBanco[0].Id.ToString(),
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None
                });

            return Ok("Logado com sucesso");
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            Response.Cookies.Delete("IdLogado");
            Response.Cookies.Delete(".AspNetCore.Session");

            return Ok("Logout realizado");
        }

        [HttpPost]
        public IActionResult CadastraUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);

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

            _context.Usuarios.Remove(usuarioDoBanco);

            _context.SaveChanges();

            return Ok("Deletado");
        }
    }
}