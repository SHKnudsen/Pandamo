using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using DynamoPandas.PandamoNodeModels.Nodes;
using System.ComponentModel;

namespace DynamoPandas.PandamoNodeModels.Controls
{
    /// <summary>
    /// Interaction logic for DataframeFormatControl.xaml
    /// </summary>
    public partial class DataframeFormatControl : UserControl, IDisposable
    {
        private DataframeTableNodeModel Model { get; set; }

        public DataframeFormatControl(DataframeTableNodeModel model)
        {
            this.Model = model;
            InitializeComponent();
            if (model.DataTable != null)
                SetDataGridContext(model);

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            Model.PropertyChanged += Model_PropertyChanged;
            Model.PortDisconnected += Model_PortDisconnected;
        }

        private void Model_PortDisconnected(Dynamo.Graph.Nodes.PortModel obj)
        {
            TableGrid.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "DataTable")
            {
                var model = sender as DataframeTableNodeModel;
                SetDataGridContext(model);
            }
        }

        private void SetDataGridContext(DataframeTableNodeModel model)
        {
            // Invoke on UI thread
            this.Dispatcher.Invoke(() =>
            {
                if (model.DataTable == null)
                    return;

                TableGrid.Visibility = System.Windows.Visibility.Visible;
                DataGrid.DataContext = model.DataTable.DefaultView;
            });
        }

        //private void NodeModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{

        //    if (e.PropertyName == "DataframeDictionary")
        //    {
        //        var model = sender as DataframeTableNodeModel;
        //        Dictionary<string, object> dict = model.DataframeDictionary;
        //        if (dict.Count == 0)
        //        {
        //            Dataframe.DataContext = new System.Data.DataTable();
        //        }
        //        else
        //        {
        //            List<object> cols = dict["columns"] as List<object>;
        //            List<object> vals = dict["data"] as List<object>;
        //            List<object> idx = dict["index"] as List<object>;

        //            if (cols.Contains("index"))
        //            {
        //                cols[cols.FindIndex(ind => ind.Equals("index"))] = "OriginalIndex";
        //            }

        //            System.Data.DataTable dataTable = new System.Data.DataTable("dataframe");
        //            dataTable.Columns.Add("index");
        //            foreach (var col in cols)
        //            {
        //                dataTable.Columns.Add(col.ToString(), col.GetType());
        //            }
        //            int counter = 0;
        //            foreach (var val in vals)
        //            {
        //                List<object> lst = val as List<object>;
        //                lst.Insert(0, idx[counter]);
        //                dataTable.Rows.Add(lst.ToArray());
        //                counter++;
        //            }

        //            // Invoke on UI thread
        //            this.Dispatcher.Invoke(() =>
        //            {
        //                Dataframe.DataContext = dataTable.DefaultView;

        //            });
        //        }  
        //    }
        //}

        private void ThumbResizeThumbOnDragDeltaHandler(object sender, DragDeltaEventArgs e)
        {
            var xAdjust = ActualWidth + e.HorizontalChange;

            if (this.Parent.GetType() == typeof(Grid))
            {
                var inputGrid = this.Parent as Grid;

                if (xAdjust >= inputGrid.MinWidth)
                {
                    Width = xAdjust;
                }
            }
        }

        public void Dispose()
        {
            Model.PropertyChanged -= Model_PropertyChanged;
            Model.PortDisconnected -= Model_PortDisconnected;
        }
    }
}
