using PizzaRestaurant.Domain.Entities;

namespace PizzaRestaurant.Application.Common.Interfaces.Persistence
{
    public interface IPizzaRepository
    {
        public Task<Pizza?> AddPizzaAsync(Pizza pizzaToAdd);

        public Task<Pizza?> GetPizzaAsync(Guid id);

        public Task<ICollection<Pizza>> GetAllPizzasAsync();

        public Task<int> UpdatePizzaAsync(Pizza updatedPizza);

        public Task<int> DeletePizzaAsync(Guid id);
    }
}
