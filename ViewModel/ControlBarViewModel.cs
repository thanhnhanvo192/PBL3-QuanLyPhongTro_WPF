using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;

namespace QuanLyPhongTro.ViewModel
{
    public class ControlBarViewModel : BaseViewModel
    {
        #region commands
        public ICommand CloseWindowCommand { get; set; }
        public ICommand MaximizeWindowCommand { get; set; }
        public ICommand MinimizeWindowCommand { get; set; }
        public ICommand MouseMoveWindowCommand { get; set; }
        #endregion

        public ControlBarViewModel()
        {
            CloseWindowCommand = new RelayCommand<UserControl>(
                (p) => p != null,
                (p) =>
                {
                    Window parentWindow = Window.GetWindow(p);
                    if (parentWindow != null)
                    {
                        parentWindow.Close();
                    }
                });

            MaximizeWindowCommand = new RelayCommand<UserControl>(
                (p) => p != null,
                (p) =>
                {
                    Window parentWindow = Window.GetWindow(p);
                    if (parentWindow != null)
                    {
                        if (parentWindow.WindowState != WindowState.Maximized)
                        {
                            parentWindow.WindowState = WindowState.Maximized;
                        }
                        else
                        {
                            parentWindow.WindowState = WindowState.Normal;
                        }
                    }
                });

            MinimizeWindowCommand = new RelayCommand<UserControl>(
                (p) => p != null,
                (p) =>
                {
                    Window parentWindow = Window.GetWindow(p);
                    if (parentWindow != null)
                    {
                        if (parentWindow.WindowState != WindowState.Minimized)
                        {
                            parentWindow.WindowState = WindowState.Minimized;
                        }
                        else
                        {
                            parentWindow.WindowState = WindowState.Maximized;
                        }
                    }
                });

            MouseMoveWindowCommand = new RelayCommand<UserControl>(
                (p) => p != null,
                (p) =>
                {
                    Window parentWindow = Window.GetWindow(p);
                    if (parentWindow != null)
                    {
                        parentWindow.DragMove();
                    }
                });
        }
    }
}
