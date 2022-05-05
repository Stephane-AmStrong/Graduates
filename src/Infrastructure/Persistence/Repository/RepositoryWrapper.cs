using Application.Interfaces;
using Domain.Entities;
using Domain.Settings;
using Persistence.Contexts;
using Persistence.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        //private readonly IHttpContextAccessor _httpContext;
        private IFileService _file;

        private readonly ISortHelper<Dimploma> _dimplomaSortHelper;
        private readonly ISortHelper<Graduate> _graduateSortHelper;
        private readonly ISortHelper<Student> _studentSortHelper;

        private readonly IOptions<EmailSettings> _mailSettings;
        private readonly IOptions<JWTSettings> _jwtSettings;

        private readonly ApplicationDbContext _appDbContext;

        private IDimplomaRepository _dimploma;
        private IGraduateRepository _graduate;
        private IStudentRepository _student;

        private string filePath;

        public string Path
        {
            set { filePath = value; }
        }


        public IFileService File
        {
            get
            {
                if (_file == null)
                {
                    _file = new FileService(_webHostEnvironment, filePath);
                }
                return _file;
            }
        }


        public IDimplomaRepository Dimploma
        {
            get
            {
                if (_dimploma == null)
                {
                    _dimploma = new DimplomaRepository(_appDbContext, _dimplomaSortHelper);
                }
                return _dimploma;
            }
        }


        public IGraduateRepository Graduate
        {
            get
            {
                if (_graduate == null)
                {
                    _graduate = new GraduateRepository(_appDbContext, _graduateSortHelper);
                }
                return _graduate;
            }
        }


        public IStudentRepository Student
        {
            get
            {
                if (_student == null)
                {
                    _student = new StudentRepository(_appDbContext, _studentSortHelper);
                }
                return _student;
            }
        }


        


        public RepositoryWrapper(
            ApplicationDbContext appDbContext,
            IWebHostEnvironment webHostEnvironment,
            IConfiguration configuration,
            IOptions<EmailSettings> mailSettings,
            IOptions<JWTSettings> jwtSettings,
            ISortHelper<Dimploma> dimplomaSortHelper,
            ISortHelper<Graduate> graduateSortHelper,
            ISortHelper<Student> studentSortHelper
            //IHttpContextAccessor httpContextAccessor
            )
        {
            _configuration = configuration;
            _mailSettings = mailSettings;
            _jwtSettings = jwtSettings;
            _appDbContext = appDbContext;

            _dimplomaSortHelper = dimplomaSortHelper;
            _graduateSortHelper = graduateSortHelper;
            _studentSortHelper = studentSortHelper;

            _webHostEnvironment = webHostEnvironment;
            //_httpContext = httpContextAccessor;
        }


        public async Task SaveAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }
    }
}
