using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;
using DynamoPandas.PandamoNodeModels.Nodes;
using System.ComponentModel;
using System.Collections;

namespace DynamoPandas.PandamoNodeModels.Controls
{
    /// <summary>
    /// Interaction logic for DataframeFormatControl.xaml
    /// </summary>
    public partial class DataframeFormatControl : UserControl
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public DataframeFormatControl(DataframeTableNodeModel model)
        {
            InitializeComponent();
            model.PropertyChanged += NodeModel_PropertyChanged;          
        }

        private void NodeModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            
            if (e.PropertyName == "DataUpdated")
            {
                var model = sender as DataframeTableNodeModel;
                Dictionary<string, object> dict = model.DataframeDictionary;
                if (dict.Count == 0)
                {
                    Dataframe.DataContext = new System.Data.DataTable();
                }
                else
                {
                    List<object> cols = dict["columns"] as List<object>;
                    List<object> vals = dict["data"] as List<object>;
                    List<object> idx = dict["index"] as List<object>;

                    System.Data.DataTable dataTable = new System.Data.DataTable("dataframe");
                    dataTable.Columns.Add("index");
                    foreach (var col in cols)
                    {
                        dataTable.Columns.Add(col.ToString(), col.GetType());
                    }
                    int counter = 0;
                    foreach (var val in vals)
                    {
                        List<object> lst = val as List<object>;
                        lst.Insert(0, idx[counter]);
                        dataTable.Rows.Add(lst.ToArray());
                        counter++;
                    }
                    // Invoke on UI thread
                    this.Dispatcher.Invoke(() =>
                    {
                        Dataframe.DataContext = dataTable.DefaultView;

                    });
                }
                
            }
                
        }

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
    }
}
