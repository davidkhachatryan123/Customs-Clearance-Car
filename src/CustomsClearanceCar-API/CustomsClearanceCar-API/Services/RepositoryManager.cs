using CustomsClearanceCar_API.Database;
using CustomsClearanceCar_API.Interfaces;

namespace CustomsClearanceCar_API.Services
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ApplicationContext _context;

        public RepositoryManager(ApplicationContext context)
        {
            _context = context;
        }

        private IC3Repository _c3Repository = null!;

        public IC3Repository C3
        {
            get
            {
                _c3Repository ??= new C3Repository(_context);

                return _c3Repository;
            }
        }
    }
}
