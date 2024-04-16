using ASP1.Data.Dal;
using ASP1.Models;
using ASP1.Models.Home;
using ASP1.Models.Home.SignUp;
using ASP1.Services.Hash;
using ASP1.Services.Kdf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Diagnostics;

namespace ASP1.Controllers
{
    public class HomeController : Controller
    {
        /*���������� �������� ������ ����������� ����� �����������. �� ������������
         ��������� ����� ��� �������� ���� ����������� (������). 
        � ����������� ����� �������� ������������� ��������� _logger*/

        private readonly ILogger<HomeController> _logger;

        /*��������� ������ ��������� (��������) - ���� � �������������� 
         private readonly - �� �� ��������� �������� �� ���� ����������� ���������.
        DIP - ��� ��������� - ��������� (��������). */

        private readonly IHashService _hashService;
        private readonly IKdfService _kdfService;

        private readonly DataAccessor _dataAccessor;

        //������ �������� �� ������������
        public HomeController(ILogger<HomeController> logger, IHashService hashService, IKdfService kdfService, DataAccessor dataAccessor)
        {
            _logger = logger;
            _hashService = hashService;
            this._kdfService = kdfService;
            _dataAccessor = dataAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Razor()
        {
            return View();
        }

        /*�������� ������� �� ������ ����� (�����)
         * Nullable (?) �������� ��� ��, �� ����� ���� �� ����
         * ���� ������� ������ ��� �����, �� ������ ����������� �������� FormModel � PageModels
         */
        public IActionResult Models(HomeModelsFormModel? formModel)
        {
            HomeModelsPageModel model = new()
            {
                PageTitle = "����� � ASP",
                FormModel = formModel
            };
            return View(model);
        }
            
        public ViewResult IoS()
        {
            HomeIoCPageModel model = new()
            {
                HashExample = _hashService.Digest("123"),
                ServiceCode=_hashService.GetHashCode().ToString(), //�����, �� ����� ��������� ���-��� - ��� 
                Title = "IoC",
                KdfExample=_kdfService.GetDerivedKey("123", "123")
            };
            return View(model);
        }
        
        public ViewResult Data()
        {
            return View();
        }

        public ViewResult SignIn()
        {
            return View();
        }

        public ViewResult SignUp(SignUpFormModel? formModel)
        {
            SignUpPageModel pageModel = new()
            {
                signUpFormModel = formModel,
                ValidationErrors=_ValidateSingUpModel(formModel)
            };
            //���������, ���� �� �����-�� ������ � ���� ���������� ������

            if(formModel?.UserEmail!=null) //���� ���������� ������
            {
                if(pageModel.ValidationErrors.Count > 0) //� ������
                {
                    pageModel.Message = "��������� ��������";
                    pageModel.IsSuccess = false;
                }
                else
                {
                    //������������ ������������
                    _dataAccessor.UserDao.SignUpUser(mapUser(formModel));
                    pageModel.Message = "��������� ������";
                    pageModel.IsSuccess=true;
                }
                    
            }
            else
            {
                pageModel.Message = "��������� ��������";
                pageModel.IsSuccess = false;
            }
            return View(pageModel);
        }

        //�� ����� ������, �� ������ - �������� Entity
        private Data.Entities.User mapUser(SignUpFormModel formModel)
        {
            String salt = Guid.NewGuid().ToString();//� ��� ���� ������ ��� ����, ���������� ���!
            Data.Entities.User user = new()
            {
                Id = Guid.NewGuid(),
                Name = formModel.UserName,
                Email = formModel.UserEmail,
                Register = DateTime.Now,
                DerivedKey = _kdfService.GetDerivedKey("123", salt)
            };
            return user;
        }

        //
        private Dictionary <String, String> _ValidateSingUpModel(SignUpFormModel? formModel)
        {
            Dictionary<String, String> res = new(); //������ ������� �� ������� � ���� �����
            if (formModel == null) {
                res[nameof(formModel)] = "Model is null";
            }
            else
            {
                if (String.IsNullOrEmpty(formModel.UserName))
                {
                    res[nameof(formModel.UserName)] = "Name is empty";
                }
                if (String.IsNullOrEmpty(formModel.UserEmail))
                {
                    res[nameof(formModel.UserEmail)] = "Email is empty";
                }
                //���������, ������������ �� ����� ��� � ��
                if(_dataAccessor.UserDao.IsEmailFree(formModel.UserEmail)) {
                    res[nameof(formModel.UserEmail)] = "Email in use";
                }
                //���������� � ���������
                if(!formModel.Confirm)
                {
                    res[nameof(formModel.Confirm)] = "Confirm expeced";
                }
            }
            return res;
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
