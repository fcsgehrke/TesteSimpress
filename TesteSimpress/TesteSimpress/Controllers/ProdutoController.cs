using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TesteSimpress.Models;

namespace TesteSimpress.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly AppDbContext _context;

        public ProdutoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Produtos
        public async Task<IActionResult> Index(int? id)
        {
            ViewBag.Categorias = new SelectList(_context.TblCategoriaProdutos, "Id", "Descricao");

            if (id != null)
            {
                var produto = await _context.TblProdutos
                    .Include(p => p.Categoria)
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (produto == null)
                {
                    return NotFound();
                }
                else
                {
                    ViewData["Produto"] = produto;
                }
            }

            var appDbContext = _context.TblProdutos.Include(p => p.Categoria);
            return View(await appDbContext.ToListAsync());
        }


        // GET: Produtos/Create
        public IActionResult Create()
        {
            ViewBag.Categorias = new SelectList(_context.TblCategoriaProdutos, "Id", "Descricao");
            return View();
        }

        // POST: Produtos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Descricao,Ativo,Perecivel,CategoriaId")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(produto);
                    await _context.SaveChangesAsync();
                }
                catch(DbUpdateException)
                {
                    TempData["Flash"] = "Os campos Nome, Descrição e Categoria são obrigatórios !";
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.TblCategoriaProdutos, "Id", "Descricao", produto.CategoriaId);
            return View(produto);
        }


        // POST: Produtos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Descricao,Ativo,Perecivel,CategoriaId")] Produto produto)
        {
            if (id != produto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (!ProdutoExists(produto.Id))
                        TempData["Flash"] = "Produto não encontrado";
                    else
                        TempData["Flash"] = "Os campos Nome, Descrição e Categoria são obrigatórios !";
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.TblCategoriaProdutos, "Id", "Descricao", produto.CategoriaId);
            return View(produto);
        }

        // GET: Produtos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                TempData["Flash"] = "Produto não encontrado";
            }
            else
            {
                var produto = await _context.TblProdutos
                    .Include(p => p.Categoria)
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (produto == null)
                {
                    TempData["Flash"] = "Produto não encontrado";
                }
                else
                {
                    try
                    {
                        _context.TblProdutos.Remove(produto);
                        await _context.SaveChangesAsync();
                    } 
                    catch(DbUpdateException)
                    {
                        TempData["Flash"] = "Erro ao excluir o produto.";
                    }
                }
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(int id)
        {
            return _context.TblProdutos.Any(e => e.Id == id);
        }
    }
}
