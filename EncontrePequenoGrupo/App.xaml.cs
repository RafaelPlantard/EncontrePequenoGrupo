using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity;

namespace EncontrePequenoGrupo
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : MvvmAppBase
    {
        #region Fields
        
        /// <summary>
        /// The dependency injection container.
        /// </summary>
        private readonly IUnityContainer _container = new UnityContainer(); 

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods
        
        #region Protected

        /// <summary>
        /// The action to initialize the things on the app. In general, the container of dependency injection.
        /// </summary>
        /// <param name="args">A implementation of <see cref="IActivatedEventArgs"/>.</param>
        /// <returns>The <see cref="Task"/> object that indicates the asynchronous logic.</returns>
        protected override Task OnInitializeAsync(IActivatedEventArgs args)
        {
            // Register MvvmAppBase services with the container so that view models can take dependencies on them.
            _container.RegisterInstance<ISessionStateService>(SessionStateService);
            _container.RegisterInstance<INavigationService>(NavigationService);
            _container.RegisterInstance<IEventAggregator>(new EventAggregator());

            // Register any app specific types with the container.

            // Set a factory for the ViewModelLocator to use the container to construct view models so their dependencies get injected by the container.
            ViewModelLocationProvider.SetDefaultViewModelFactory((viewModelType) => _container.Resolve(viewModelType));

            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// The action that runs on the launch application, it is asynchronous.
        /// </summary>
        /// <param name="args">The launch activated event arguments.</param>
        /// <returns>The <see cref="Task"/> object that indicates the asynchronous logic.</returns>
        protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            NavigationService.Navigate("MainPage", null);

            return Task.FromResult<object>(null);
        }

        #endregion

        #endregion
    }
}