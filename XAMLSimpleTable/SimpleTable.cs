using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace XAMLSimpleTable
{
    public class SimpleTable : Grid
    {
        public SimpleTable()
        {
        }

        DataTemplate headerTemplate;
        DataTemplate firstHeaderTemplate;
        DataTemplate cellTemplate;
        DataTemplate firstCellTemplate;
        IList rowsSource;
        IList<ColumnData> columnSource;


        void buildTable()
        {
            Debug.WriteLine("[buildTable() started]");

            if (columnSource == null || rowsSource == null)
                return;

            //clear table
            Children.Clear();
            ColumnDefinitions.Clear();
            RowDefinitions.Clear();

            RowDefinitions.Add(new RowDefinition
            {
                Height = new GridLength(1, GridUnitType.Auto),
            });

            var x = 0;

            //process columns
            foreach (var column in columnSource)
            {
                var headerCell = new ContentControl();
                headerCell.HorizontalAlignment = HorizontalAlignment.Stretch;
                headerCell.HorizontalContentAlignment = HorizontalAlignment.Stretch;

                //apply first header template if first header column and first cell template supplied
                if (x == 0 && firstHeaderTemplate != null)
                    headerCell.ContentTemplate = firstHeaderTemplate;
                else
                    headerCell.ContentTemplate = headerTemplate;

                headerCell.Content = column.Header;

                ColumnDefinitions.Add(new ColumnDefinition()
                {
                    Width = new GridLength(1, column.IsAutoWidth ? GridUnitType.Auto : GridUnitType.Star),
                });

                SetColumn(headerCell, x);
                SetRow(headerCell, 0);

                Children.Add(headerCell);

                var y = 1;

                //process rows
                foreach (var row in rowsSource)
                {
                    var rowCell = new ContentControl();
                    rowCell.HorizontalAlignment = HorizontalAlignment.Stretch;
                    rowCell.HorizontalContentAlignment = HorizontalAlignment.Stretch;

                    //apply first header template if first header column and first cell template supplied
                    if (x == 0 && firstCellTemplate != null)
                        rowCell.ContentTemplate = firstCellTemplate;
                    else
                        rowCell.ContentTemplate = cellTemplate;

                    rowCell.DataContext = row;
                    BindingOperations.SetBinding(rowCell, ContentControl.ContentProperty, column.CellDataBinding);

                    if (RowDefinitions.Count - 1 < y)
                    {
                        RowDefinitions.Add(new RowDefinition()
                        {
                            Height = new GridLength(1, GridUnitType.Auto),
                        });
                    }

                    SetColumn(rowCell, x);
                    SetRow(rowCell, y);

                    Children.Add(rowCell);

                    y++;
                }

                x++;
            }

            Debug.WriteLine("[buildTable() completed]");

        }


        void OnCellTemplateChanged(DataTemplate value)
        {
            cellTemplate = value;
            Debug.WriteLine("[cellTemplate changed]");
        }

        void OnFirstCellTemplateChanged(DataTemplate value)
        {
            firstCellTemplate = value;
            Debug.WriteLine("[firstCellTemplate changed]");
        }

        void OnColumnSourceChanged(IList<ColumnData> value)
        {
            columnSource = value;
            Debug.WriteLine("[columnSource changed]");

            buildTable();
        }

        void OnRowsSource_Changed(IList value)
        {
            rowsSource = value;
            Debug.WriteLine("[rowsSource changed]");

            buildTable();
        }

        void OnFirstHeaderCellTemplateChanged(DataTemplate value)
        {
            firstHeaderTemplate = value;
            Debug.WriteLine("[firstHeaderTemplate changed]");
        }

        void OnHeaderCellTemplateChanged(DataTemplate value)
        {
            headerTemplate = value;
            Debug.WriteLine("[headerTemplate changed]");
        }


        #region HeaderCellTemplate (DependencyProperty)

        public DataTemplate HeaderCellTemplate
        {
            get { return (DataTemplate)GetValue(HeaderCellTemplateProperty); }
            set { SetValue(HeaderCellTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CellTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderCellTemplateProperty =
            DependencyProperty.Register("HeaderCellTemplate", typeof(DataTemplate), typeof(SimpleTable), new PropertyMetadata(null, new PropertyChangedCallback(onHeaderCellTemplatePropertyChanged)));

        static void onHeaderCellTemplatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((SimpleTable)d).OnHeaderCellTemplateChanged(e.NewValue as DataTemplate);
        }

        #endregion

        #region FirstHeaderCellTemplate (DependencyProperty)

        public DataTemplate FirstHeaderCellTemplate
        {
            get { return (DataTemplate)GetValue(FirstHeaderCellTemplateProperty); }
            set { SetValue(FirstHeaderCellTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CellTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FirstHeaderCellTemplateProperty =
            DependencyProperty.Register("FirstHeaderCellTemplate", typeof(DataTemplate), typeof(SimpleTable), new PropertyMetadata(null, new PropertyChangedCallback(onFirstHeaderCellTemplatePropertyChanged)));

        static void onFirstHeaderCellTemplatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((SimpleTable)d).OnFirstHeaderCellTemplateChanged(e.NewValue as DataTemplate);
        }


        #endregion

        #region CellTemplate (DependencyProperty)

        public DataTemplate CellTemplate
        {
            get { return (DataTemplate)GetValue(CellTemplateProperty); }
            set { SetValue(CellTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CellTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CellTemplateProperty =
            DependencyProperty.Register("CellTemplate", typeof(DataTemplate), typeof(SimpleTable), new PropertyMetadata(null, new PropertyChangedCallback(onCellTemplatePropertyChanged)));

        static void onCellTemplatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((SimpleTable)d).OnCellTemplateChanged(e.NewValue as DataTemplate);
        }

        #endregion

        #region FirstCellTemplate (DependencyProperty)

        public DataTemplate FirstCellTemplate
        {
            get { return (DataTemplate)GetValue(FirstCellTemplateProperty); }
            set { SetValue(FirstCellTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CellTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FirstCellTemplateProperty =
            DependencyProperty.Register("FirstCellTemplate", typeof(DataTemplate), typeof(SimpleTable), new PropertyMetadata(null, new PropertyChangedCallback(onFirstCellTemplatePropertyChanged)));

        static void onFirstCellTemplatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((SimpleTable)d).OnFirstCellTemplateChanged(e.NewValue as DataTemplate);
        }

        #endregion

        #region ColumnSource (DependencyProperty)

        public IList<ColumnData> ColumnSource
        {
            get { return (IList<ColumnData>)GetValue(ColumnSourceProperty); }
            set { SetValue(ColumnSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ColumnSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColumnSourceProperty =
            DependencyProperty.Register("ColumnSource", typeof(IList<ColumnData>), typeof(SimpleTable), new PropertyMetadata(null, new PropertyChangedCallback(onColumnSourceChanged)));

        static void onColumnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((SimpleTable)d).OnColumnSourceChanged(e.NewValue as IList<ColumnData>);
        }

        #endregion

        #region RowsSource (DependencyProperty)

        public IList RowsSource
        {
            get { return (IList)GetValue(RowsSourceProperty); }
            set { SetValue(RowsSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowsSourceProperty =
            DependencyProperty.Register("RowsSource", typeof(IList), typeof(SimpleTable), new PropertyMetadata(null, new PropertyChangedCallback(rowsSourcePropertyChanged)));

        static void rowsSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((SimpleTable)d).OnRowsSource_Changed(e.NewValue as IList);
        }

        #endregion

    }

    public class ColumnData
    {
        public string Header { get; set; }
        public bool IsAutoWidth { get; set; }
        public Binding CellDataBinding { get; set; }
    }
}
