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
    public class PedidoesController : Controller
    {
        private readonly VendasWebContext _context;

        public PedidoesController(VendasWebContext context)
        {
            _context = context;
        }

        // GET: Pedidoes
        public async Task<IActionResult> Index(int? id)
        {
            if (id.HasValue)
            {
                var cliente = await _context.Cliente.FindAsync(id);
                if (cliente != null)
                {
                    var pedidos = await _context.Pedido
                        .Where(p => p.IdCliente == id)
                        .OrderByDescending(x => x.IdPedido)
                        .AsNoTracking().ToListAsync();
                    //_context.Entry(cliente).Collection(c => c.Pedidos).Load();
                    ViewBag.Cliente = cliente;
                    return View(pedidos);
                }
                else
                {
                    return RedirectToAction("Index", "Clientes");
                }
            }
            else
            {
                return RedirectToAction("Index", "Clientes");
            }
           
        }

        // GET: Pedidoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .Include(c => c.Pedidos)
               .FirstOrDefaultAsync(m => m.Pedidos.Any(p => p.IdPedido == id));

            if (cliente == null)
            {
                return NotFound();
            }

            var pedido = cliente.Pedidos.FirstOrDefault(p => p.IdPedido == id);
            if (pedido == null)
            {
                return NotFound();
            }
            return View(pedido);
        }

        // GET: Pedidoes/Create
        public async Task<IActionResult> Create(int ? id)
        {
            if (id.HasValue)
            {
                var cliente = await _context.Cliente.FindAsync(id);
                if (cliente != null)
                {
                    _context.Entry(cliente).Collection(c => c.Pedidos).Load();
                    Pedido pedido = null;
                    if (_context.Pedido.Any(p => p.IdCliente == id && !p.DataPedido.HasValue))
                    {
                        pedido = await _context.Pedido
                            .FirstOrDefaultAsync(p => p.IdCliente == id && !p.DataPedido.HasValue);
                    }
                    else
                    {
                        pedido = new Pedido { IdCliente = id.Value, ValorTotal = 0 };
                        cliente.Pedidos.Add(pedido);
                        await _context.SaveChangesAsync();
                    }
                    return RedirectToAction("Index", "ItemPedidoes", new { id = pedido.IdPedido });
                }
                return RedirectToAction("Index", "Clientes");
            }
            return RedirectToAction("Index", "Clientes");
        }

      

        // POST: Pedidoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPedido,DataPedido,DataEntrega,ValorTotal,IdCliente,IdEntrega")]int ? id, Pedido pedido)
        {
            
            if (ModelState.IsValid)
            {
                if (id.HasValue)
                {
                    _context.Pedido.Add(pedido);
                    await _context.SaveChangesAsync();
                }
              
                
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(pedido);
            }

        }

        // GET: Pedidoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }
            return View(pedido);
        }

        // POST: Pedidoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPedido,DataPedido,DataEntrega,ValorTotal,IdCliente,IdEntrega")] Pedido pedido)
        {
            if (id != pedido.IdPedido)
            {
                return NotFound();
            }

          
                    _context.Update(pedido);
                    await _context.SaveChangesAsync();
               
                    if (!PedidoExists(pedido.IdPedido))
                    {
                        return NotFound();
                    }
            return RedirectToAction(nameof(Index));

            //return View(pedido);
        }

    



    // GET: Pedidoes/Delete/5
    public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido
                .FirstOrDefaultAsync(m => m.IdPedido == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // POST: Pedidoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedido = await _context.Pedido.FindAsync(id);
            if (pedido != null)
            {
                _context.Pedido.Remove(pedido);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoExists(int id)
        {
            return _context.Cliente
                .Include(p => p.Pedidos)
                .Any(e => e.Pedidos.Any(e => e.IdPedido == id));
        }

        [HttpGet]
        public async Task<IActionResult> Fechar(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }

            if (!PedidoExists(id.Value))
            {
                return RedirectToAction("Index", "Clientes");
            }

            var pedido = await _context.Pedido
                .Include(p => p.Cliente)
                .Include(p => p.ItensPedido)
                .ThenInclude(i => i.Produto)
                .FirstOrDefaultAsync(p => p.IdPedido == id);

            return View(pedido);
        }

        [HttpPost]
        public async Task<IActionResult> Fechar(int id)
        {
            if (PedidoExists(id))
            {
                var pedido = await _context.Pedido
                    .Include(p => p.Cliente)
                    .Include(p => p.ItensPedido)
                    .ThenInclude(i => i.Produto)
                    .FirstOrDefaultAsync(p => p.IdPedido == id);

                if (pedido.ItensPedido.Count() > 0)
                {
                    pedido.DataPedido = DateTime.Now;
                    foreach (var item in pedido.ItensPedido)
                        item.Produto.Estoque -= item.Quantidade;
                    if (await _context.SaveChangesAsync() > 0) ;
                    else
                        return RedirectToAction("Index", new { cid = pedido.IdCliente });
                }
                else
                {
                    return RedirectToAction("Index", new { cid = pedido.IdCliente });
                }
            }
                return RedirectToAction("Index", "Clientes");
        }

        [HttpGet]
        public async Task<IActionResult> Entregar(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }

            if (!PedidoExists(id.Value))
            {
                return RedirectToAction("Index", "Clientes");
            }

            var pedido = await _context.Pedido
                .Include(p => p.Cliente)
                .ThenInclude(c => c.Enderecos)
                .Include(p => p.ItensPedido)
                .ThenInclude(i => i.Produto)
                .FirstOrDefaultAsync(p => p.IdPedido == id);

            return View(pedido);
        }

        [HttpPost]
        public async Task<IActionResult> Entregar(int idPedido, int idEndereco)
        {
            if (PedidoExists(idPedido))
            {
                var pedido = await _context.Pedido
                    .Include(p => p.Cliente)
                    .ThenInclude(c => c.Enderecos)
                    .FirstOrDefaultAsync(p => p.IdPedido == idPedido);

                var endereco = pedido.Cliente.Enderecos
                    .FirstOrDefault(e => e.IdEndereco == idEndereco);

                if (endereco != null)
                {
                    pedido.EnderecoEntrega = endereco;
                    pedido.DataEntrega = DateTime.Now;
                    if (await _context.SaveChangesAsync() > 0) ;
                    else
                        return RedirectToAction("Index", new { cid = pedido.IdCliente });
                }
                else
                {
                    return RedirectToAction("Index", new { cid = pedido.IdCliente });
                }
            }
           
                return RedirectToAction("Index", "Clientes");
            
        }
    }
}
