
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Moq;
using NETCore.MailKit.Core;
using System;
using System.Collections.Generic;
using Totallydays.Data;
using Totallydays.Models;
using Totallydays.Repositories;
using Totallydays.Services;
using Totallydays.ViewsModel;
using Microsoft.AspNetCore.Authentication;
using System.Threading;

namespace TestTotallydaysWebUi
{
    public class TestVariable
    {
        protected Mock<HostingRepository> _hostingRepository;
        protected Mock<HostingTypeRepository> _hostingTypeRepo;
        protected Mock<EquipmentRepository> _equipementRepository;
        protected Mock<ImageRepository> _imageRepository;
        protected Mock<BookingRepository> _bookingRepository;
        protected Mock<BedRepository> _bedRepository;
        protected Mock<BedRoomRepository> _bedRoomRepository;
        protected Mock<UserRepository> _userRepo;
        protected Mock<FakeUserManager> _userManagerMock;
        protected Mock<FakeSignInManager> _signInManagerMock;
        protected Mock<FakeRoleManager> _roleManagerMock;
        protected Mock<UploadService> _uploadService;
        protected Mock<HostingService> _hostingService;
        protected Mock<BookingService> _bookingService;
        protected Mock<GoogleMapService> _googleMapService;
        protected Mock<BedRoomService> _bedrommService;
        protected Mock<ControllerExtenstionServiceRazor> _controllerExtenstionServiceRazor;
        protected Mock<SendMailService> _mailService;
        protected Mock<ReCaptchaService> _recaptchaService;
        protected IWebHostEnvironment _env;
        protected IConfiguration _config;
        protected IOptions<ReCaptchaSetting> _repcaptchaOption;
        protected IRazorViewEngine _razor;
        protected ITempDataProvider _ITempDataProvider;
        protected IServiceProvider _IServiceProvider;
        protected IEmailService _IEmailService;
        protected IWebHostEnvironment _IWebHostEnvironment;

        public void getVariable()
        {
            this._env = (new Mock<IWebHostEnvironment>()).Object;
            
            this._razor = (new Mock<IRazorViewEngine>()).Object;
            this._ITempDataProvider = (new Mock<ITempDataProvider>()).Object;
            this._IServiceProvider = (new Mock<IServiceProvider>()).Object;
            this._IEmailService = (new Mock<IEmailService>()).Object;
            this._IWebHostEnvironment = (new Mock<IWebHostEnvironment>()).Object;

            var configuration = new Mock<IConfiguration>();

            var configurationSection = new Mock<IConfigurationSection>();
            configurationSection.Setup(a => a.Value).Returns("ConnectionStrings");

            configuration.Setup(a => a.GetSection("TestValueKey")).Returns(configurationSection.Object);
            this._config = configuration.Object;

            this._repcaptchaOption = new Mock<IOptions<ReCaptchaSetting>>().Object;
            this.getRepository();
        }
        public void getRepository()
        {
            var options = new DbContextOptionsBuilder<TotallydaysContext>()
             .Options;

            var mockContext = new Mock<TotallydaysContext>(options);
            this._hostingRepository = (new Mock<HostingRepository>(mockContext.Object));
            this._hostingTypeRepo = (new Mock<HostingTypeRepository>(mockContext.Object));
            this._equipementRepository = (new Mock<EquipmentRepository>(mockContext.Object));
            this._imageRepository = (new Mock<ImageRepository>(mockContext.Object));
            this._bookingRepository = (new Mock<BookingRepository>(mockContext.Object));
            this._bedRepository = (new Mock<BedRepository>(mockContext.Object));
            this._bedRoomRepository = (new Mock<BedRoomRepository>(mockContext.Object));
            this._userRepo = (new Mock<UserRepository>(mockContext.Object));
            this._userManagerMock = new Mock<FakeUserManager>();
            this._signInManagerMock = new Mock<FakeSignInManager>();
            this.getService();
        }

        private void getService()
        {
            this._uploadService = new Mock<UploadService>(_env);
            this._hostingService = new Mock<HostingService>(_hostingRepository.Object, _hostingTypeRepo.Object, _uploadService.Object);
            this._bookingService = new Mock<BookingService>(_bookingRepository.Object);
            this._googleMapService = new Mock<GoogleMapService>(_config, _hostingRepository.Object);
            this._bedrommService = new Mock<BedRoomService>(_bedRoomRepository.Object, _bedRepository.Object);
            this._controllerExtenstionServiceRazor = new Mock<ControllerExtenstionServiceRazor>(_razor, _ITempDataProvider, _IServiceProvider);
            this._mailService = new Mock<SendMailService>(_IEmailService, _config, _controllerExtenstionServiceRazor.Object);

            this._recaptchaService = new Mock<ReCaptchaService>(this._repcaptchaOption);
        }

       
        
    }

    public class FakeSignInManager : SignInManager<AppUser>
    {
        public FakeSignInManager() : base(
            new FakeUserManager(),
            new Mock<IHttpContextAccessor>().Object,
            new Mock<IUserClaimsPrincipalFactory<AppUser>>().Object,
            new Mock<IOptions<IdentityOptions>>().Object,
            new Mock<ILogger<SignInManager<AppUser>>>().Object,
            new Mock<IAuthenticationSchemeProvider>().Object,
            new Mock<IUserConfirmation<AppUser>>().Object
            )
        { }
    }


    public class FakeUserManager : UserManager<AppUser>
    {
        public FakeUserManager()
            : base(new Mock<IUserStore<AppUser>>().Object,
                  new Mock<IOptions<IdentityOptions>>().Object,
                  new Mock<IPasswordHasher<AppUser>>().Object,
                  new IUserValidator<AppUser>[0],
                  new IPasswordValidator<AppUser>[0],
                  new Mock<ILookupNormalizer>().Object,
                  new Mock<IdentityErrorDescriber>().Object,
                  new Mock<IServiceProvider>().Object,
                  new Mock<ILogger<UserManager<AppUser>>>().Object)
        { }
    }

    public class FakeRoleManager : RoleManager<AppRole>
    {
        public FakeRoleManager()
            : base(
                  new Mock<IRoleStore<AppRole>>().Object,
                  new Mock<IEnumerable<IRoleValidator<AppRole>>>().Object,
                  new Mock<ILookupNormalizer>().Object,
                  new Mock<IdentityErrorDescriber>().Object,
                  new Mock<ILogger<RoleManager<AppRole>>>().Object)
        { }
    }
}
