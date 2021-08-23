using MoviesApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace MoviesApp.Services
{
    public class NavigationService : INavigationService
    {
        private Dictionary<string, Type> Pages { get; } = new Dictionary<string, Type>();

        public Page MainPage => Application.Current.MainPage;

        public void Configure(string key, Type pageType) => Pages[key] = pageType;

        public void GoBack() => MainPage.Navigation.PopModalAsync();

        public void NavigateTo(string pageKey, object parameter = null)
        {
            if (Pages.TryGetValue(pageKey, out Type pageType))
            {
                Page page = (Page)Activator.CreateInstance(pageType);
                page.SetNavigationArgs(parameter);

                MainPage.Navigation.PushModalAsync(page);

                (page.BindingContext as BaseViewModel).Initialize(parameter);
            }
            else
            {
                throw new ArgumentException($"This page doesn't exist: {pageKey}.", nameof(pageKey));
            }
        }
    }

    public static class NavigationExtensions
    {
        private static ConditionalWeakTable<Page, object> arguments = new ConditionalWeakTable<Page, object>();

        public static object GetNavigationArgs(this Page page)
        {
            return arguments.TryGetValue(page, out object argument);
        }

        public static void SetNavigationArgs(this Page page, object args) => arguments.Add(page, args);
    }
}
