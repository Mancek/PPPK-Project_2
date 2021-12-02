using System.Collections.ObjectModel;
using System.Linq;
using Zadatak.Dal;

namespace Zadatak.ViewModels
{
    public class GenericViewModel<T> where T : class
    {
        public ObservableCollection<T> Items { get; }
        public GenericViewModel()
        {
            Items = new ObservableCollection<T>(RepositoryFactory.GetRepository().GetList<T>());
            Items.CollectionChanged += Items_CollectionChanged;
        }

        private void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                        RepositoryFactory.GetRepository().Add(Items[e.NewStartingIndex]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    RepositoryFactory.GetRepository().Delete(e.OldItems.OfType<T>().ToList()[0]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                        RepositoryFactory.GetRepository().Update(e.NewItems.OfType<T>().ToList()[0]);
                    break;
            }
        }

        internal void Update(T item) => Items[Items.IndexOf(item)] = item;
    }
}
