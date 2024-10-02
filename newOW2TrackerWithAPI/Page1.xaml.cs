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
using System.Diagnostics;

namespace newOW2TrackerWithAPI
{
    /// <summary>
    /// Page1.xaml の相互作用ロジック
    /// </summary>
    public partial class Page1 : Page
    {
      MainWindow mainWindow;
        public Page1()
        {
            InitializeComponent();
            //testcontent.Content = userData.userName;
            mainWindow = (MainWindow)Application.Current.MainWindow;
            Debug.WriteLine(mainWindow.sharedUserData.userName);
            Debug.WriteLine("page one load");
            testcontent.Content= mainWindow.sharedUserData.userName;

            mainWindow.LoadCompetitiveData();
            

            
        }
    }
}
