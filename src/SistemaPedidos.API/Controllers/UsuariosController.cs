using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaPedidos.Domain;
using SistemaPedidos.Infrastructure;

namespace SistemaPedidos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly AppDbContext _context;

    public UsuariosController(AppDbContext context)
    {
        _context = context;
    }

    // LISTAR TODOS
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var lista = await _context.Usuarios.ToListAsync();
        return Ok(lista);
    }

    // CRIAR NOVO
    [HttpPost]
    public async Task<IActionResult> Post(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
        return Ok(usuario);
    }

    // EDITAR (PUT)
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Usuario usuarioAtualizado)
    {
        var usuarioNoBanco = await _context.Usuarios.FindAsync(id);

        if (usuarioNoBanco == null)
            return NotFound("Usuário não encontrado.");

        // Atualiza os campos do Usuário
        usuarioNoBanco.Nome = usuarioAtualizado.Nome;
        usuarioNoBanco.Email = usuarioAtualizado.Email;
        usuarioNoBanco.Cargo = usuarioAtualizado.Cargo;

        await _context.SaveChangesAsync();

        return Ok("Usuário atualizado com sucesso!");
    }

    // EXCLUIR (DELETE)
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);

        if (usuario == null)
            return NotFound("Usuário não existe no banco.");

        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();

        return Ok($"Usuário {id} removido com sucesso!");
    }
}