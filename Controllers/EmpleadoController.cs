using Microsoft.AspNetCore.Mvc;

using AppCRUD.Data;
using AppCRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace AppCRUD.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly AppDBContext _dbContext;

        public EmpleadoController(AppDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<Empleado> lista = await _dbContext.Empleados.ToListAsync();
            return View(lista);
        }

        [HttpGet]
        public IActionResult Nuevo()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Nuevo(Empleado empleado)
        {
            await _dbContext.Empleados.AddAsync(empleado);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            Empleado empleado = await _dbContext.Empleados.FirstAsync(e => e.IdEmpleado == id);
            return View(empleado);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Empleado empleado)
        {
            _dbContext.Empleados.Update(empleado);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            Empleado empleado = await _dbContext.Empleados.FirstAsync(e => e.IdEmpleado == id);
            _dbContext.Empleados.Remove(empleado);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }
    }
}
