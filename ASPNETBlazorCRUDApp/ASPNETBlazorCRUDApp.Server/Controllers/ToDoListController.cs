using ASPNETBlazorCRUDApp.Server.Data;
using ASPNETBlazorCRUDApp.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETBlazorCRUDApp.Server.Controllers
{
    [Produces("application/json")]
    [Route("api/ToDo")]
    public class ToDoListController : Controller
    {
        private readonly ToDoListContext _context;

        public ToDoListController(ToDoListContext context)
        {
            _context = context;
        }

        // GET: api/ToDo
        [HttpGet]
        public IEnumerable<ToDoListItem> GetToDo()
        {
            return _context.ToDoListItems;
        }

        // POST: api/ToDo
        [HttpPost]
        public async Task<IActionResult> PostToDo([FromBody] ToDoListItem todo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.ToDoListItems.Add(todo);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetToDo", new { id = todo.ID }, todo);
        }

        // PUT: api/ToDo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDo([FromRoute] int id, [FromBody] ToDoListItem todo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != todo.ID)
                return BadRequest();

            _context.Entry(todo).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDoExists(id))
                    return NotFound();
                else
                    throw;
            }
            return NoContent();
        }

        // DELETE: api/ToDo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDo([FromRoute] int id)
        {
            var ToDo = await _context.ToDoListItems.SingleOrDefaultAsync(m => m.ID == id);
            if (ToDo == null)
                return NotFound();

            _context.ToDoListItems.Remove(ToDo);
            await _context.SaveChangesAsync();
            return Ok(ToDo);
        }

        private bool ToDoExists(int id)
        {
            return _context.ToDoListItems.Any(e => e.ID == id);
        }
    }
}