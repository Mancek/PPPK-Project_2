using System.Windows.Controls;
using Zadatak.ViewModels;

namespace Zadatak
{
    public class FramedPage<T> : Page where T : class
    {
        public FramedPage(GenericViewModel<T> viewModel)
        {
            ViewModel = viewModel;
        }
        public GenericViewModel<T> ViewModel { get; }
        public Frame Frame { get; set; }
    }
}
