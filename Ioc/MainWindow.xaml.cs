using Moq;
using StructureMap;
using StructureMap.Graph;
using StructureMap.Graph.Scanning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
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

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var mock = new Mock<My>(new object[] { new MyData() { Parameter = "Mocky Param" } }); // Mock My constructor.
            mock.Setup(expression => expression.Text).Returns("Hello from Mock"); // Mock My.Text property.

            My proxy = mock.Object;

            Container container = new Container(configuration => {
                configuration.For<My>().Use(proxy); // Register My mock.
                UtilStructureMap.RegisterViewModelToView(configuration);
            });

            UtilStructureMap.CreateViewModel(container, my);

            var myTest = ((UserControlViewModel)my.DataContext).My;
            Title = myTest.Text + " - " + myTest.MyData.Parameter;
        }
    }

    public class My
    {
        public My(MyData myData)
        {
            this.MyData = myData;
        }

        public readonly MyData MyData;

        public virtual string Text // Virtual so it can be overridden by moq framework.
        {
            get
            {
                return "The Original";
            }
        }
    }

    public class MyData
    {
        public MyData()
        {
            Parameter = "Default";
        }

        public string Parameter;
    }
}
