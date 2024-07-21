using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VendasWeb.Data;
using VendasWeb.Models;

namespace VendasWeb.Controllers
{
    public class ItemPedidoesController : Controller
    {
        private readonly VendasWebContext _context;
        private readonly ILogger<ItemPedidoesController> _logger;
        public ItemPedidoesController(VendasWebContext context, ILogger<ItemPedidoesController> logger)
        {
            _context = context;
            _logger = logger;
        }


        public async Task<IActionResult> Index(int? id)
        {
            if (id.HasValue)
            {
                if (_context.Pedido.Any(p => p.IdPedido == id))
                {
                    var pedido = await _context.Pedido
                        .Include(p => p.Cliente)
                        .Include(p => p.ItensPedido.OrderBy(i => i.Produto.Nome))
                        .ThenInclude(i => i.Produto)
                        .FirstOrDefaultAsync(p => p.IdPedido == id);

                    ViewBag.Pedido = pedido;
                    return View(pedido.ItensPedido);
                }
                return RedirectToAction("Index", "Clientes");
            }
            return RedirectToAction("Index", "Clientes");
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? id, int? prod)
        {
            if (id.HasValue)
            {
                if (_context.Pedido.Any(p => p.IdPedido == id))
                {
                    var produtos = _context.Produto
                        .OrderBy(x => x.Nome)
                        .Select(p => new { p.IdProduto, NomePreco = $"{p.Nome} ({p.Preco:C})" })
                        .AsNoTracking().ToList();
                    var produtosSelectList = new SelectList(produtos, "IdProduto", "NomePreco");
                    ViewBag.Produtos = produtosSelectList;

                    if (prod.HasValue && ItemPedidoExiste(id.Value, prod.Value))
                    {
                        var itemPedido = await _context.ItemPedido
                            .Include(i => i.Produto)
                            .FirstOrDefaultAsync(i => i.IdPedido == id && i.IdProduto == prod);
                        return View(itemPedido);
                    }
                    else
                    {
                        return View(new ItemPedido()
                        { IdPedido = id.Value, ValorUnitario = 0, Quantidade = 1 });
                    }
                }
                return RedirectToAction("Index", "Cliente");
            }
            return RedirectToAction("Index", "Cliente");
        }

        private bool ItemPedidoExiste(int ped, int prod)
        {
            return _context.ItemPedido.Any(x => x.IdPedido == ped && x.IdProduto == prod);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ItemPedido itemPedido)
        {
            if (itemPedido.IdPedido <= 0)
            {
                return RedirectToAction("Index", "Clientes");
            }

            var produto = await _context.Produto.FindAsync(itemPedido.IdProduto);
            if (produto == null)
            {
                // Produto não encontrado, retornar erro ou redirecionar
                ModelState.AddModelError("IdProduto", "Produto não encontrado.");
                return View(itemPedido);
            }

            itemPedido.ValorUnitario = produto.Preco;

            if (ItemPedidoExiste(itemPedido.IdPedido, itemPedido.IdProduto))
            {
                _context.ItemPedido.Update(itemPedido);
            }
            else
            {
                _context.ItemPedido.Add(itemPedido);
            }

            await _context.SaveChangesAsync();

            var pedido = await _context.Pedido.FindAsync(itemPedido.IdPedido);
            if (pedido == null)
            {
                // Pedido não encontrado, retornar erro ou redirecionar
                ModelState.AddModelError("IdPedido", "Pedido não encontrado.");
                return View(itemPedido);
            }

            pedido.ValorTotal = _context.ItemPedido
                .Where(i => i.IdPedido == itemPedido.IdPedido)
                .Sum(i => i.ValorUnitario * i.Quantidade);

            _context.Update(pedido);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { id = itemPedido.IdPedido });
        }
     

        // GET: ItemPedidoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemPedido = await _context.ItemPedido.FindAsync(id);
            if (itemPedido == null)
            {
                return NotFound();
            }
            return View(itemPedido);
        }

        // POST: ItemPedidoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPedido,IdProduto,Quantidade,ValorUnitario")] ItemPedido itemPedido)
        {
            if (id != itemPedido.IdPedido)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                    _context.Update(itemPedido);
                    await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(itemPedido);
        }
           
        

        // GET: ItemPedidoes/Delete/5
        public async Task<IActionResult> Delete(int? id, int? prod)
        {
            if (id == null || prod == null)
            {
                return NotFound();
            }

            var itemPedido = await _context.ItemPedido
                .Include(i => i.Produto) 
                .FirstOrDefaultAsync(i => i.IdPedido == id && i.IdProduto == prod);
            if (itemPedido == null)
            {
                return NotFound();
            }

            return View(itemPedido);
        }

        // POST: ItemPedidoes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int idPedido, int idProduto)
        {
            var itemPedido = await _context.ItemPedido.FindAsync(idPedido, idProduto);
            if (itemPedido != null)
            {
                _context.ItemPedido.Remove(itemPedido);
                if (await _context.SaveChangesAsync() > 0)
                {
                    var pedido = await _context.Pedido.FindAsync(itemPedido.IdPedido);
                    pedido.ValorTotal = _context.ItemPedido
                        .Where(i => i.IdPedido == itemPedido.IdPedido)
                        .Sum(i => i.ValorUnitario * i.Quantidade);
                    await _context.SaveChangesAsync();
                }
                else
                return RedirectToAction("Index", new { ped = idPedido });
            }
            else
            {
                return RedirectToAction("Index", new { ped = idPedido });
            }
            return RedirectToAction("Index");
        }
            
        

        private bool ItemPedidoExists(int ped, int prod)
        {
            return _context.ItemPedido.Any(e => e.IdPedido == ped &&  e.IdProduto == prod);
        }
    }
}
