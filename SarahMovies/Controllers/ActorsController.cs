using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SarahMovies.Data;
using SarahMovies.Models;
using Tweetinvi;
using VaderSharp2;

namespace SarahMovies.Controllers
{
    public class ActorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActorsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> GetMediaActor(int id)
        {
            var actor = await _context.Actor
                .FirstOrDefaultAsync(m => m.Id == id);
            if(actor == null)
            {
                return NotFound();
            }
            var imageData = actor.MediaActor;
            return File(imageData, "image/jpg");
        }

        // GET: Actors
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Actor.Include(a => a.Movie);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Actors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Actor == null)
            {
                return NotFound();
            }

            var actor = await _context.Actor
                .Include(a => a.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actor == null)
            {
                return NotFound();
            }
            ActorDetailsVM actorDetailsVM = new ActorDetailsVM();
            actorDetailsVM.Actor = actor;
            actorDetailsVM.Tweets = new List<ActorTweet>();

            var userClient = new TwitterClient("iBoMakUuDeZ3pyD1DTS2GTDgo", "Ckp2OcDqtBRV2ZJ25pPD6enq5kKoyV9QE97mIfFQ4A61SbRt0", "AAAAAAAAAAAAAAAAAAAAADQjhwEAAAAAjkC%2FJrVONtRGUW53kt%2Fo8BlZqys%3D6G4wNsRZvdVN7ndnh1MUcKUd3EmVflXWLXE5bkaI6nDna0CVBC");
            var searchResponse = await userClient.SearchV2.SearchTweetsAsync(actor.Name);
            var tweets = searchResponse.Tweets;
            var analyzer = new SentimentIntensityAnalyzer();
            foreach(var tweetText in tweets)
            {
                var tweet = new ActorTweet();
                tweet.TweetText = tweetText.Text;
                var results = analyzer.PolarityScores(tweet.TweetText);
                tweet.Sentiment = results.Compound;
               actorDetailsVM.Tweets.Add(tweet);
            }
            return View(actorDetailsVM);
        }

        // GET: Actors/Create
        public IActionResult Create()
        {
            ViewData["MovieID"] = new SelectList(_context.Movie, "Id", "Title");
            return View();
        }

        // POST: Actors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Gender,Age,ActorIMBD,MovieID")] Actor actor, IFormFile MediaActor)
        {
            if (ModelState.IsValid)
            {
                if(MediaActor != null && MediaActor.Length > 0)
                {
                    var memoryStream = new MemoryStream();
                    await MediaActor.CopyToAsync(memoryStream);
                    actor.MediaActor = memoryStream.ToArray();
                }
                _context.Add(actor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieID"] = new SelectList(_context.Movie, "Id", "Title", actor.MovieID);
            return View(actor);
        }
       
        // GET: Actors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Actor == null)
            {
                return NotFound();
            }

            var actor = await _context.Actor.FindAsync(id);
            if (actor == null)
            {
                return NotFound();
            }
            ViewData["MovieID"] = new SelectList(_context.Movie, "Id", "Title", actor.MovieID);
            return View(actor);
        }

        // POST: Actors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Gender,Age,ActorIMBD,MovieID")] Actor actor, IFormFile? MediaActor)
        {
            if (id != actor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                    if (MediaActor != null && MediaActor.Length > 0)
                    {
                        var memoryStream = new MemoryStream();
                        await MediaActor.CopyToAsync(memoryStream);
                        actor.MediaActor = memoryStream.ToArray();
                    }
                    _context.Add(actor);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                
                }
            ViewData["MovieID"] = new SelectList(_context.Movie, "Id", "Title", actor.MovieID);
            return View(actor);
        }

        // GET: Actors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Actor == null)
            {
                return NotFound();
            }

            var actor = await _context.Actor
                .Include(a => a.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actor == null)
            {
                return NotFound();
            }

            return View(actor);
        }

        // POST: Actors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Actor == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Actor'  is null.");
            }
            var actor = await _context.Actor.FindAsync(id);
            if (actor != null)
            {
                _context.Actor.Remove(actor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActorExists(int id)
        {
            return _context.Actor.Any(e => e.Id == id);
        }
    }
}
