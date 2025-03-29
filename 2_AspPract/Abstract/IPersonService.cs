using _2_AspPract.Models;

namespace _2_AspPract.Abstract
{
    public interface IPersonService
    {
        Task<List<PersonDto>> GetAllAsync();
        //Task<PersonDto> GetByName(string Name);
        Task AddAsync(PersonDto person);
        Task UpdateAsync(PersonDto person);
        Task DeleteAsync(Guid id);
    }
}
