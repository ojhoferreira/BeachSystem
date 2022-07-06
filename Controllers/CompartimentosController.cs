using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProvaDs2.Models;

namespace ProvaDS.Controllers
{
    public class CompartimentosController : Controller
    {
        private readonly ProvaContext _context;

        public CompartimentosController(ProvaContext context)
        {
            _context = context;
        }

        // GET: Compartimentos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Compartimento.ToListAsync());
        }

        // GET: Compartimentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compartimento = await _context.Compartimento
                .FirstOrDefaultAsync(m => m.NumeroId == id);
            if (compartimento == null)
            {
                return NotFound();
            }

            return View(compartimento);
        }

        // GET: Compartimentos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Compartimentos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NumeroId,Tamanho,ArmarioId,Disponivel,PessoaId")] Compartimento compartimento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(compartimento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(compartimento);
        }

        // GET: Compartimentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compartimento = await _context.Compartimento.FindAsync(id);
            if (compartimento == null)
            {
                return NotFound();
            }
            return View(compartimento);
        }

        // POST: Compartimentos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NumeroId,Tamanho,ArmarioId,Disponivel,PessoaId")] Compartimento compartimento)
        {
            if (id != compartimento.NumeroId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(compartimento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompartimentoExists(compartimento.NumeroId))
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
            return View(compartimento);
        }

        // GET: Compartimentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compartimento = await _context.Compartimento
                .FirstOrDefaultAsync(m => m.NumeroId == id);
            if (compartimento == null)
            {
                return NotFound();
            }

            return View(compartimento);
        }

        // POST: Compartimentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var compartimento = await _context.Compartimento.FindAsync(id);
            _context.Compartimento.Remove(compartimento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompartimentoExists(int id)
        {
            return _context.Compartimento.Any(e => e.NumeroId == id);
        }

        //Novo Post
        
        [HttpPost]
        public IActionResult Cadastrar([Bind("Tamanho,ArmarioId")] Compartimento compartimento, string CPF)
        {
            if (ModelState.IsValid)
            {
                var dono = _context.Administrador.FirstOrDefault(a => a.CPF == CPF);
 
                if (dono == null)
                {
                    ModelState.AddModelError("CPF", "Esse CPF não é compatível com Adm");
                    return View(compartimento);
                }
 
                _context.Add(compartimento);
                _context.SaveChanges();
                return RedirectToAction("Cadastrar");
            }
 
            return View(compartimento);
        }
 
        public IActionResult Cadastrar(int ArmarioId)
        {
            ViewBag.ArmarioId = ArmarioId;
            return View();
        }
 
        public IActionResult Alocar(int compartimentoid)
        {
            ViewBag.compartimentoid = compartimentoid;
            return View();
        }
 
        [HttpPost]
        public IActionResult Alocar(int compartimentoid, string CPF)
        {
            var pessoa = _context.Pessoa.FirstOrDefault(p => p.CPF == CPF);
 
            if (pessoa == null)
            {
                ModelState.AddModelError("CPF", "Usuarios não existente");
                return View(CPF);
            }
           
            var compartimento = _context.Compartimento.FirstOrDefault(c => c.NumeroId == compartimentoid);
 
            if (!compartimento.Disponivel)
            {
                ModelState.AddModelError("CPF", "Compartimento não está disponível");
                return View(CPF);
            }
 
            pessoa.Compartimento = compartimento;
            compartimento.PessoaId = pessoa.PessoaId;
            compartimento.Disponivel = false;
            _context.SaveChanges();
 
            return RedirectToAction("ListarMeus", new {CPF = CPF});
        }
 
        public IActionResult Listar(int armarioid)
        {
            var compartimentos = _context.Compartimento.Where(c => c.ArmarioId == armarioid).ToList();
 
            return View(compartimentos);
        }
 
        public IActionResult ListarMeus(string CPF)
        {
            var pessoa = _context.Pessoa.FirstOrDefault(p => p.CPF == CPF);
 
            var meuscompartimentos = _context.Compartimento.Where(c => c.PessoaId == pessoa.PessoaId).ToList();
           
            return View(meuscompartimentos);
        }
 
        public IActionResult Desalocar(int compartimentoid, string CPF)
        {
            var pessoa = _context.Pessoa.FirstOrDefault(p => p.CPF == CPF);
 
            if (pessoa == null)
            {
                ModelState.AddModelError("CPF", "CPF não existente");
                return View(CPF);
            }
 
            var compartimento = _context.Compartimento.FirstOrDefault(c => c.NumeroId == compartimentoid);
 
            compartimento.Disponivel = true;
            compartimento.PessoaId = null;
            pessoa.Compartimento = null;
            _context.SaveChanges();
 
            return RedirectToAction("ListarMeus", new {CPF = CPF});
        }

    }
}
