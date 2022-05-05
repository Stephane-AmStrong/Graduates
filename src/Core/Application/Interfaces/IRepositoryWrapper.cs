using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRepositoryWrapper
    {
        IDiplomaRepository Diploma { get; }
        IGraduateRepository Graduate { get; }
        IStudentRepository Student { get; }
        IFileService File { get; }
        string Path { set; }
        Task SaveAsync();
    }
}
