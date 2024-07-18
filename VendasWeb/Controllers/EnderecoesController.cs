using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VendasWeb.Data;
using VendasWeb.Models;

namespace VendasWeb.Controllers
{
    public class EnderecoesController : Controller
    {
        private readonly VendasWebContext _context;

        public EnderecoesController(VendasWebContext context)
        {
            _context = context;
        }

        // GET: Enderecoes
        public async Task<IActionResult> Index(int? id)
        {
            if (id.HasValue)
            {
                var cliente = await _context.Cliente.FindAsync(id);
                if (cliente != null)
                {
                    _context.Entry(cliente).Collection(c => c.Enderecos).Load();
                    ViewBag.Cliente = cliente;
                    return View(cliente.Enderecos);
                }
                else
                {
                    return RedirectToAction("Index", "Clientes");
                }
            }
            else
            {
                return RedirectToAction("Index", "clientes");
            
            }
        }

        // GET: Enderecoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .Include(c => c.Enderecos)
                .FirstOrDefaultAsync(c => c.Enderecos.Any(e => e.IdEndereco == id));

            if (cliente == null)
            {
                return NotFound();
            }

            var endereco = cliente.Enderecos.FirstOrDefault(e => e.IdEndereco == id);
            if (endereco == null)
            {
                return NotFound();
            }

            return View(endereco);
        }

        // GET: Enderecoes/Create
        public async Task<IActionResult>  Create(int? id)
        {
            if (id.HasValue)
            {
                var cliente = await _context.Cliente.FindAsync(id);
                if (cliente != null)
                {
                    ViewBag.Cliente = cliente;
                }

            }
            return View();
        }

        // POST: Enderecoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([ Bind("IdEndereco,Logradouro,Numero,Complemento,Bairro,Cidade,Estado,CEP,Referencia,Selecionado")] int? idUsuario, Endereco endereco)
        {
            if (!idUsuario.HasValue)
            {
                return RedirectToAction("index", "Clientes");
            }
            
            var cliente = await _context.Cliente
                .Include(c => c.Enderecos)
                .FirstOrDefaultAsync(c => c.IdUsuario == idUsuario);
            ViewBag.Cliente = cliente;
          
            
                if (cliente.Enderecos.Count() == 0) endereco.Selecionado = true;
                endereco.CEP = ObterCepNormalizado(endereco.CEP);
                    
               
                cliente.Enderecos.Add(endereco); ;
                
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new {id = idUsuario} );
            
           // ViewBag.Cliente = cliente;
            //return View(endereco);
            
        }
           

        // GET: Enderecoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .Include(c => c.Enderecos)
                .FirstOrDefaultAsync(c => c.Enderecos.Any(e => e.IdEndereco == id));

            if (cliente == null)
            {
                return NotFound();
            }

            var endereco = cliente.Enderecos.FirstOrDefault(e => e.IdEndereco == id);
            if (endereco == null)
            {
                return NotFound();
            }

            return View(endereco);
        }

        // POST: Enderecoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEndereco,Logradouro,Numero,Complemento,Bairro,Cidade,Estado,CEP,Referencia,Selecionado")] Endereco endereco)
        {
            if (id != endereco.IdEndereco)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    var cliente = await _context.Cliente
                        .Include(c => c.Enderecos)
                        .FirstOrDefaultAsync(c => c.Enderecos.Any(e => e.IdEndereco == id));

                    if (cliente == null)
                    {
                        return NotFound();
                    }

                    var enderecoExistente = cliente.Enderecos.FirstOrDefault(e => e.IdEndereco == id);
                    if (enderecoExistente == null)
                    {
                        return NotFound();
                    }

                    // Atualize as propriedades do endereço existente
                    enderecoExistente.Logradouro = endereco.Logradouro;
                    enderecoExistente.Numero = endereco.Numero;
                    enderecoExistente.Complemento = endereco.Complemento;
                    enderecoExistente.Bairro = endereco.Bairro;
                    enderecoExistente.Cidade = endereco.Cidade;
                    enderecoExistente.Estado = endereco.Estado;
                    enderecoExistente.CEP = endereco.CEP;
                    enderecoExistente.Referencia = endereco.Referencia;
                    enderecoExistente.Selecionado = endereco.Selecionado;

                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnderecoExists(endereco.IdEndereco))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(endereco);
        }

        // GET: Enderecoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .Include(c => c.Enderecos)
                .FirstOrDefaultAsync(c => c.Enderecos.Any(e => e.IdEndereco == id));

            if (cliente == null)
            {
                return NotFound();
            }

            var endereco = cliente.Enderecos.FirstOrDefault(e => e.IdEndereco == id);
            if (endereco == null)
            {
                return NotFound();
            }

            return View(endereco);
        }

        // POST: Enderecoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Cliente
        .Include(c => c.Enderecos)
        .FirstOrDefaultAsync(c => c.Enderecos.Any(e => e.IdEndereco == id));

            if (cliente == null)
            {
                return NotFound();
            }

            var endereco = cliente.Enderecos.FirstOrDefault(e => e.IdEndereco == id);
            if (endereco != null)
            {
                cliente.Enderecos.Remove(endereco);
                _context.Update(cliente);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool EnderecoExists(int id)
        {
            return _context.Cliente
            .Include(c => c.Enderecos)
            .Any(c => c.Enderecos.Any(e => e.IdEndereco == id));
        }

        private string ObterCepNormalizado(string cep)
        {
            string cepNormalizado = cep.Replace("-", "").Replace(".", "").Trim();
            return cepNormalizado.Insert(5, "-");
        }
    }
}
