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
using System.Windows.Shapes;
using QuanLyPhongTro.ViewModel;

namespace QuanLyPhongTro.View
{
    /// <summary>
    /// Interaction logic for TenantWindow.xaml
    /// </summary>
    public partial class TenantView : UserControl
    {
        public TenantView()
        {
            InitializeComponent();
            DataContext = new TenantViewModel();
        }
    }
}
