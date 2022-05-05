using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRepositoryWrapper
    {
        IDimplomaRepository Dimploma { get; }
        IGraduateRepository Graduate { get; }
        IStudentRepository Student { get; }
        IFileService File { get; }
        string Path { set; }
        Task SaveAsync();
    }
}
