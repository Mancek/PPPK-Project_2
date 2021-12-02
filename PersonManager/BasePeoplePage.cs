using Zadatak.Models;
using Zadatak.ViewModels;

namespace Zadatak
{
    public class BasePeoplePage : FramedPage<Person>
    {
        public BasePeoplePage(GenericViewModel<Person> viewModel) : base(viewModel)
        {
        }
    }
}
