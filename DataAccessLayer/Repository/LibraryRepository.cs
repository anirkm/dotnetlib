using BusinessObjects.Entity;

namespace DataAccessLayer.Repository;

public class LibraryRepository : IGenericRepository<Library>
{
    private readonly List<Library> _libraries;

    public LibraryRepository()
    {
        _libraries =
        [
            new Library
            {
                Id = 1,
                Name = "Roubaix Debug Dungeon",
                Address = "44 Avenue Jean Lebas, Roubaix"
            },
            new Library
            {
                Id = 2,
                Name = "Calais Stack Overflow Annex",
                Address = "16 Rue du Pont Lottin, Calais"
            }
        ];
    }

    public IEnumerable<Library> GetAll()
    {
        return _libraries;
    }

    public Library? Get(int id)
    {
        return _libraries.FirstOrDefault(library => library.Id == id);
    }
}
