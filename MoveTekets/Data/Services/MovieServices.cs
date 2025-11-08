using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace MoveTekets.Data.Services
{
    public class MovieServices : ImoveServices
    {
        private readonly AppDbContext context;
        public MovieServices(AppDbContext _context)
        {
            this.context = _context;
        }

        // GetAll
        public async Task<IEnumerable<Move>> GetAll()
        {
            var result = await context.Movies
                                      .Include(m => m.Cinema)
                                      .Include(m => m.Producer)
                                      .Include(m => m.ActorMovies)
                                        .ThenInclude(am => am.Actor)
                                      .OrderBy(m => m.Name)
                                      .ToListAsync();
            return result;
        }

        // GetById
        public async Task<Move> GitById(int id)
        {
            var result = await context.Movies
                                      .FirstOrDefaultAsync(m => m.id == id);
            return result;
        }

        // GetMoveByWithIncludes
        public async Task<Move> GetMoveByWithIncludes(int id)
        {
            var result = await context.Movies
                          .Include(m => m.Cinema)
                          .Include(m => m.Producer)
                          .Include(m => m.ActorMovies)
                            .ThenInclude(am => am.Actor)
                          .FirstOrDefaultAsync(m => m.id == id);

            return result;
        }

        // Add
        public async Task Add(Move move)
        {
            await context.Movies.AddAsync(move);
        }

        // Update
        public async Task<Move> Update(int id, Move updateMovie)
        {
            var existingMove = await context.Movies.FirstOrDefaultAsync(m => m.id == id);
            if (existingMove != null) 
            {
                existingMove.Name = updateMovie.Name;
                existingMove.Description = updateMovie.Description;
                existingMove.Price = updateMovie.Price;
                existingMove.ImageURL = updateMovie.ImageURL;
                existingMove.StartDate = updateMovie.StartDate;
                existingMove.EndDate = updateMovie.EndDate;
                existingMove.MovieCategory = updateMovie.MovieCategory;
                existingMove.CinemaId = updateMovie.CinemaId;
                existingMove.ProducerId = updateMovie.ProducerId;

                return existingMove;
            }
            return null;
        }

        // Delete
        public async Task Delete(int id)
        {
            var movie = await context.Movies.FirstOrDefaultAsync(m => m.id == id);
            if (movie != null)
            {
                context.Movies.Remove(movie);
            }
        }
        public async Task<IEnumerable<Actor>> GetAllActors() 
                              => await context.Actors.ToListAsync();
        public async Task<IEnumerable<Cinema>> GetAllCinemas() 
                              => await context.Cinemas.ToListAsync();
        public async Task<IEnumerable<Producer>> GetAllProducers() 
                              => await context.Producers.ToListAsync();

        // Save
        public async Task save()
        {
            await context.SaveChangesAsync();
        }


    }
}
