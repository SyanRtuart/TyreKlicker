﻿using System.Threading.Tasks;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace TyreKlicker.XF.Core.ViewModels
{
    public abstract class MvxNavigationViewModel
        : MvxViewModel
    {
        private bool _isBusy;
        private IMvxLog _log;

        protected MvxNavigationViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
        {
            LogProvider = logProvider;
            NavigationService = navigationService;
        }

        public bool IsBusy
        {
            get => _isBusy;

            set
            {
                _isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        protected virtual IMvxNavigationService NavigationService { get; }

        protected virtual IMvxLogProvider LogProvider { get; }

        protected virtual IMvxLog Log => _log ?? (_log = LogProvider.GetLogFor(GetType()));
    }

    public abstract class MvxNavigationViewModel<TParameter> : MvxNavigationViewModel, IMvxViewModel<TParameter>
    {
        protected MvxNavigationViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(
            logProvider, navigationService)
        {
        }

        public abstract void Prepare(TParameter parameter);
    }

    //TODO: Not possible to name MvxViewModel, name is MvxViewModelResult for now
    public abstract class MvxNavigationViewModelResult<TResult> : MvxNavigationViewModel, IMvxViewModelResult<TResult>
    {
        protected MvxNavigationViewModelResult(IMvxLogProvider logProvider, IMvxNavigationService navigationService) :
            base(logProvider, navigationService)
        {
        }

        public TaskCompletionSource<object> CloseCompletionSource { get; set; }

        public override void ViewDestroy(bool viewFinishing = true)
        {
            if (viewFinishing && CloseCompletionSource != null && !CloseCompletionSource.Task.IsCompleted &&
                !CloseCompletionSource.Task.IsFaulted)
                CloseCompletionSource?.TrySetCanceled();

            base.ViewDestroy(viewFinishing);
        }
    }

    public abstract class MvxNavigationViewModel<TParameter, TResult> : MvxNavigationViewModelResult<TResult>,
        IMvxViewModel<TParameter, TResult>
    {
        protected MvxNavigationViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(
            logProvider, navigationService)
        {
        }

        public abstract void Prepare(TParameter parameter);
    }
}