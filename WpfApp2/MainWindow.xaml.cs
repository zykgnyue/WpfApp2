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

namespace WpfApp2
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //public Button xxButton;
        public MainWindow()
        {
            InitializeComponent();

            Button xxButton = new Button();

            xxButton.Content = "I came from code behind";
            diapPanelArea.Children.Add(xxButton);

            this.Ok.MouseMove += new MouseEventHandler(this.Ok_Click);
            

            this.Ok.Click += new RoutedEventHandler(this.Ok_Click);
            xxButton.MouseMove += new MouseEventHandler(this.Ok_Click);

        }



        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            Button mybutton = sender as Button;
            MouseEventArgs ee = e as MouseEventArgs;
            
            if (ee != null)
            {
                Point mp = ee.GetPosition(diapPanelArea);
                textBlockMsg.Text = $"Mouse is at X={mp.X} Y={mp.Y}";
            }
        



        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnTest_Click_1(object sender, RoutedEventArgs e)
        {
            // Wann to do something when button clicked
        }

        private void btnTest_MouseMove(object sender, MouseEventArgs e)
        {
            Point xx=e.GetPosition(diapPanelArea);
            Button yy = sender as Button;
            if(yy!=null)
            {
                yy.Content = $"My mouse position x={xx.X}; y={xx.Y}";
            }
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           // ReRrawGraph();
        }

        public void ReRrawGraph()
        {
            canvasGraph.Children.Clear();
            const double margin = 10;
            double xmin = margin;
            //if(canvasGraph.Width==double.NaN)
            //{
            //canvasGraph.Width = diapPanelArea.Width;
            //}
            double xmax = canvasGraph.Width - margin;
            double ymin = margin;
            double ymax = canvasGraph.Height - margin;
            const double step = 10;
            // Make the X axis.
            GeometryGroup xaxis_geom = new GeometryGroup();
            xaxis_geom.Children.Add(new LineGeometry(
                new Point(0, ymax), new Point(canvasGraph.Width, ymax)));
            for (double x = xmin + step;
                x <= canvasGraph.Width - step; x += step)
            {
                xaxis_geom.Children.Add(new LineGeometry(
                    new Point(x, ymax - margin / 2),
                    new Point(x, ymax + margin / 2)));
            }
            Path xaxis_path = new Path();
            xaxis_path.StrokeThickness = 1;
            xaxis_path.Stroke = Brushes.Black;
            xaxis_path.Data = xaxis_geom;

            canvasGraph.Children.Add(xaxis_path);
            // Make the Y ayis.
            GeometryGroup yaxis_geom = new GeometryGroup();
            yaxis_geom.Children.Add(new LineGeometry(
                new Point(xmin, 0), new Point(xmin, canvasGraph.Height)));
            for (double y = step; y <= canvasGraph.Height - step; y += step)
            {
                yaxis_geom.Children.Add(new LineGeometry(
                    new Point(xmin - margin / 2, y),
                    new Point(xmin + margin / 2, y)));
            }
            Path yaxis_path = new Path();
            yaxis_path.StrokeThickness = 1;
            yaxis_path.Stroke = Brushes.Black;
            yaxis_path.Data = yaxis_geom;
            canvasGraph.Children.Add(yaxis_path);
            // Make some data sets.
            Brush[] brushes = { Brushes.Red, Brushes.Green, Brushes.Blue };
            Random rand = new Random();
            for (int data_set = 0; data_set < 3; data_set++)
            {
                int last_y = rand.Next((int)ymin, (int)ymax);
                PointCollection points = new PointCollection();
                for (double x = xmin; x <= xmax; x += step)
                {
                    last_y = rand.Next(last_y - 10, last_y + 10);
                    if (last_y < ymin) last_y = (int)ymin;
                    if (last_y > ymax) last_y = (int)ymax;
                    points.Add(new Point(x, last_y));
                }
                Polyline polyline = new Polyline();
                polyline.StrokeThickness = 1;
                polyline.Stroke = brushes[data_set];
                polyline.Points = points;

                canvasGraph.Children.Add(polyline);
            }
        }

        private void canvasGraph_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ReRrawGraph();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if(e.WidthChanged)
            {
                Size xx = e.NewSize;

                diapPanelArea.Width= xx.Width;

            }
         }

        private void diapPanelArea_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.WidthChanged)
            {
                Size xx = e.NewSize;

                canvasGraph.Width = xx.Width;

            }
        }
    }
}
