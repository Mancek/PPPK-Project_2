using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadatak.Models;
using Zadatak.ViewModels;

namespace Zadatak
{
    public class BaseHotelsPage : FramedPage<Hotel>
    {
        public BaseHotelsPage(GenericViewModel<Hotel> viewModel) : base(viewModel)
        {
        }
    }
}
