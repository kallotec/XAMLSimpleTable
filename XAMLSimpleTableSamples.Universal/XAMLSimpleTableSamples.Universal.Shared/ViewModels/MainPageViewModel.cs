using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using XAMLSimpleTable;

namespace XAMLSimpleTableSamples.Universal.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public MainPageViewModel()
        {
            Columns = new ObservableCollection<ColumnData>
            {
                new ColumnData
                {
                    Header = "Column A",
                    IsAutoWidth = false,
                    CellDataBinding = new Windows.UI.Xaml.Data.Binding
                    {
                        Path = new Windows.UI.Xaml.PropertyPath("ValueA")
                    },
                },
                new ColumnData
                {
                    Header = "Column B",
                    IsAutoWidth = true,
                    CellDataBinding = new Windows.UI.Xaml.Data.Binding
                    {
                        Path = new Windows.UI.Xaml.PropertyPath("ValueB")
                    },
                },
                new ColumnData
                {
                    Header = "Column C",
                    IsAutoWidth = true,
                    CellDataBinding = new Windows.UI.Xaml.Data.Binding
                    {
                        Path = new Windows.UI.Xaml.PropertyPath("ValueC")
                    },
                },
                new ColumnData
                {
                    Header = "Column D",
                    IsAutoWidth = true,
                    CellDataBinding = new Windows.UI.Xaml.Data.Binding
                    {
                        Path = new Windows.UI.Xaml.PropertyPath("ValueD")
                    },
                },
                new ColumnData
                {
                    Header = "Column E",
                    IsAutoWidth = true,
                    CellDataBinding = new Windows.UI.Xaml.Data.Binding
                    {
                        Path = new Windows.UI.Xaml.PropertyPath("ValueE")
                    },
                },
            };

            Data = new ObservableCollection<Data>
            {
                new Data
                {
                    ValueA = "355",
                    ValueB = "24",
                    ValueC = "634",
                    ValueD = "324",
                    ValueE = "1",
                },
                new Data
                {
                    ValueA = "1",
                    ValueB = "333",
                    ValueC = "1",
                    ValueD = "22 12 1212313 213  1",
                    ValueE = "111",
                },
                new Data
                {
                    ValueA = "355",
                    ValueB = "24",
                    ValueC = "634",
                    ValueD = "324",
                    ValueE = "1",
                },
                new Data
                {
                    ValueA = "1",
                    ValueB = "333",
                    ValueC = "1",
                    ValueD = "22",
                    ValueE = "111",
                },
            };
        }

        public ObservableCollection<ColumnData> Columns { get; set; }
        public ObservableCollection<Data> Data { get; set; }


        #region INotifyPropertyChanged implementation

        void propChange(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }

    public class Data
    {
        public string ValueA { get; set; }
        public string ValueB { get; set; }
        public string ValueC { get; set; }
        public string ValueD { get; set; }
        public string ValueE { get; set; }
    }

}
