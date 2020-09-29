using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GymWorkoutTracker.Data;
using GymWorkoutTracker.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System.Security.Cryptography.Xml;

namespace GymWorkoutTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private readonly WorkoutContext _context;


        public ResultsController(WorkoutContext context)
        {
            _context = context;
           
        }

        // GET: api/Results
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Result>>> GetResults()
        {
            var list = await _context.Results.Include(a => a.User).Include(b => b.Exercise).OrderByDescending(s => s.Date).ToListAsync();
            return list;
        }

        // GET: api/Results/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Result>> GetResult(int id)
        {
            var result = await _context.Results.FindAsync(id);

            if (result == null)
            {
                return NotFound();
            }
            return result;
        }


        // GET: api/Results/name&exercise&quantity
        [HttpGet("{user}/{exercise}/{quantity}")]
        public async Task<ActionResult<IEnumerable<Result>>> GetSelectedResults(string user, string exercise, int quantity)
        {
             Console.WriteLine("request received");

           var sortedRecord =  new List<Result>();
            if (user == "allUsers")
            {
                sortedRecord =  _context.Results.Include(a => a.User).Include(b => b.Exercise).OrderByDescending(s => s.Date).ToList();
            }
            else
            {
                sortedRecord = _context.Results.Include(a => a.User).Include(b => b.Exercise).OrderByDescending(s => s.Date).Where(s => s.User.UserName == user).ToList();
            }
            if (exercise != "allExercises")
            {
                if (quantity==-1)
                {
                    sortedRecord = sortedRecord.Where(x => x.Exercise.ExerciseName == exercise).ToList();
                }
                else
                {
                    sortedRecord = sortedRecord.Where(x => x.Exercise.ExerciseName == exercise).TakeLast(quantity).ToList();
                }
            }
            else
            {
                if (quantity != -1)
                {
                sortedRecord = sortedRecord.TakeLast(quantity).ToList();
                }
            }
            return  sortedRecord;
        }
        
      
        // PUT: api/Results/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResult(int id, Result result)
        {
            if (id != result.Id)
            {
                return BadRequest();
            }

            _context.Entry(result).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResultExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Results
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Result>> PostResult(Result result)
        {
            Result newResult = new Result
            {
                User = _context.GetUser(result.User.UserName),
                Exercise = _context.GetExercise(result.Exercise.ExerciseName),
                Weight = result.Weight,
                Repeats = result.Repeats,
                Date = DateTime.Now.Date
            };
             
            _context.Results.Add(newResult);
            await _context.SaveChangesAsync();
   
            return CreatedAtAction("GetResult", new { id = newResult.Id }, newResult);
        }


        // DELETE: api/Results/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Result>> DeleteResult(int id)
        {
            var result = await _context.Results.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            _context.Results.Remove(result);
            await _context.SaveChangesAsync();

            return result;
        }

        private bool ResultExists(int id)
        {
            return _context.Results.Any(e => e.Id == id);
        }
    }
}
