using Moq;
using MvvmCross.Core;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Core.Views;
using MvvmCross.Platform.Core;
using MvvmCross.Platform.Logging;
using MvvmCross.Test.Core;
using System;
using System.Collections.Generic;
using TyreKlicker.Core.Services;
using TyreKlicker.Core.ViewModels;
using Xunit;

namespace TyreKlicker.UnitTests.ViewModelTests
{
    public class LoginViewModelTests
        : MvxIoCSupportingTest
    {
        [Fact]
        public void ViewModelTests()
        {
            base.ClearAll();

            var mockLogService = new Mock<MvvmCross.Logging.IMvxLogProvider>();
            var mockNavigationService = new Mock<MvvmCross.Navigation.IMvxNavigationService>();
            var mockAppSettings = new Mock<IAppSettings>();

            var mockDispatcher = new MockDispatcher();
            Ioc.RegisterSingleton<IMvxViewDispatcher>(mockDispatcher);
            Ioc.RegisterSingleton<IMvxMainThreadDispatcher>(mockDispatcher);

            var firstViewModel = new LoginViewModel(mockLogService.Object, mockNavigationService.Object, mockAppSettings.Object);

            firstViewModel.LoginCommand.Execute(null);

            Assert.Equal(1, mockDispatcher.Requests.Count);

            Assert.True(1.Equals(mockDispatcher.Requests.Count));
            var requests = mockDispatcher.Requests[0];
            Assert.True(requests.ViewModelType == typeof(SplitRootViewModel));
        }
    }

    public class MockDispatcher : MvxMainThreadDispatcher, IMvxViewDispatcher
    {
        public readonly List<MvxViewModelRequest> Requests =
            new List<MvxViewModelRequest>();

        public readonly List<MvxPresentationHint> Hints =
            new List<MvxPresentationHint>();

        public bool ShowViewModel(MvxViewModelRequest request)
        {
            Requests.Add(request);
            return true;
        }

        public bool ChangePresentation(MvxPresentationHint hint)
        {
            Hints.Add(hint);
            return true;
        }

        public bool RequestMainThreadAction(Action action, bool maskExceptions = true)
        {
            action();
            return true;
        }
    }

    //public class ViewModelTestsBase : MvxIoCSupportingTest
    //{
    //    protected MockDispatcher MockDispatcher;

    //    protected override void AdditionalSetup()
    //    {
    //        base.AdditionalSetup();
    //        MockDispatcher = new MockDispatcher();
    //        Ioc.RegisterSingleton<IMvxViewDispatcher>(MockDispatcher);
    //        Ioc.RegisterSingleton<IMvxMainThreadDispatcher>(MockDispatcher);
    //        // required only when passing parameters
    //        Ioc.RegisterSingleton<IMvxStringToTypeParser>(new MvxStringToTypeParser());
    //    }

    //    //[Testfix]
    //    //public void TestInit()
    //    //{
    //    //    Setup();
    //    //}
    //}
}