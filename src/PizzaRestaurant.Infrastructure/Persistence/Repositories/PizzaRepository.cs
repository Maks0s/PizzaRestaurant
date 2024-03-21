using Microsoft.EntityFrameworkCore;
using PizzaRestaurant.Application.Common.Interfaces.Persistence;
using PizzaRestaurant.Domain.Entities;

namespace PizzaRestaurant.Infrastructure.Persistence.Repositories
{
    public class PizzaRepository : IPizzaRepository
    {
        private readonly PizzaDbContext _context;

        public PizzaRepository(PizzaDbContext context)
        {
            _context = context;
        }

        public async Task<Pizza?> AddPizzaAsync(Pizza pizzaToAdd)
        {
            var addedPizza = 
                await _context.Pizzas
                    .AddAsync(pizzaToAdd);

            await _context.SaveChangesAsync();

            return addedPizza.Entity;
        }

        public async Task<Pizza?> GetPizzaAsync(Guid id)
        {
            var pizza = 
                await _context.Pizzas
                    .FindAsync(id);

            return pizza;
        }

        public async Task<ICollection<Pizza>> GetAllPizzasAsync()
        {
            var allPizzas =
                await _context.Pizzas
                    .ToListAsync();

            return allPizzas;
        }

        public async Task<int> UpdatePizzaAsync(Pizza updatedPizza)
        {
            int updatedPizzasCount =
                await _context.Pizzas
                    .Where(pizza => pizza.Id.Equals(updatedPizza.Id))
                    .ExecuteUpdateAsync(pizza => pizza
                        .SetProperty(p => p.Name, updatedPizza.Name)
                        .SetProperty(p => p.CrustType, updatedPizza.CrustType)
                        .SetProperty(p => p.Ingredients, updatedPizza.Ingredients)
                        .SetProperty(p => p.Price, updatedPizza.Price)
                    );

            return updatedPizzasCount;
        }

        public async Task<int> DeletePizzaAsync(Guid id)
        {
            int deletedPizzasCount = 
                await _context.Pizzas
                    .Where(pizza => pizza.Id.Equals(id))
                    .ExecuteDeleteAsync();

            return deletedPizzasCount;
        }
    }
}
