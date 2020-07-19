using CoronaVirusPandemic.Models;
using CoronaVirusPandemic.Services.API;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace CoronaVirusPandemic.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _status;
        public string Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        private string _SearchText;
        public string SearchText
        {
            get => _SearchText;
            set
            {
                SetProperty(ref _SearchText, value);
                ItemsView.Refresh();
            }
        }

        private ObservableCollection<CoronavirusCountry> _data = new ObservableCollection<CoronavirusCountry>();
        public ObservableCollection<CoronavirusCountry> data
        {
            get => _data;
            set => SetProperty(ref _data, value);
        }

        public DelegateCommand LoadDataCommand { get; set; }

        private string _title = "Corona Virus Pandemic: World Real Time Counter";
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        public ICollectionView ItemsView => CollectionViewSource.GetDefaultView(data);
        public MainWindowViewModel()
        {
            LoadDataCommand = new DelegateCommand(LoadData);
            LoadData();
            ItemsView.Filter = new Predicate<object>(o => Filter(o as CoronavirusCountry));
        }
        private bool Filter(CoronavirusCountry item)
        {
            return SearchText == null
                            || item.CountryName.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) != -1;
        }

        private async void LoadData()
        {
            APICoronavirusCountryService service = new APICoronavirusCountryService();
            data.AddRange(await service.GetTopCases());
            Status = "Updated " + DateTime.Now;
        }
    }
}
