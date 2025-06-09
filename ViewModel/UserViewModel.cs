using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using QuanLyPhongTro.Model;

namespace QuanLyPhongTro.ViewModel
{
    public class UserViewModel : BaseViewModel
    {
        private string _Password;
        private string _NewPassword;
        private string _NewPasswordConfirm;
        private bool _IsEditingPassword;
        public string Password
        {
            get => _Password;
            set
            {
                _Password = value;
                OnPropertyChanged();
            }
        }
        public string NewPassword
        {
            get => _NewPassword;
            set
            {
                _NewPassword = value;
                OnPropertyChanged();
            }
        }
        public string NewPasswordConfirm
        {
            get => _NewPasswordConfirm;
            set
            {
                _NewPasswordConfirm = value;
                OnPropertyChanged();
            }
        }
        public bool IsEditingPassword
        {
            get => _IsEditingPassword;
            set
            {
                _IsEditingPassword = value;
                OnPropertyChanged();
            }
        }
        public ICommand LogoutCommand { get; set; }
        public ICommand ChangPasswordCommand { get; set; }
        public ICommand EnablePasswordEditCommand { get; set; }
        public DashboardViewModel dashboardViewModel { get; set; } = new DashboardViewModel();

        public UserViewModel() 
        {
            IsEditingPassword = false;
            LogoutCommand = new RelayCommand<UserControl>(
            (p) => true,
            (p) =>
            {
                Window parentWindow = Window.GetWindow(p);
                if (parentWindow != null)
                {
                    dashboardViewModel.LoadedWindowCommand.Execute(parentWindow);
                }
            });

            ChangPasswordCommand = new RelayCommand<object>(
            (p) =>
            {
                 if (Password == null || NewPassword == null || NewPasswordConfirm == null)
                     return false;
                 return true;
            },
            (p) =>
            {
                string PasswordHash = MD5Hash(Base64Encode(Password));
                var currentAccount = DataProvider.Ins.DB.Accounts.FirstOrDefault(a => a.PasswordHash == PasswordHash);
                if (currentAccount != null)
                {
                    if (NewPassword != NewPasswordConfirm)
                    {
                        MessageBox.Show("Mật khẩu mới không chính xác!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    string NewPasswordHash = MD5Hash(Base64Encode(NewPassword));
                    currentAccount.PasswordHash = NewPasswordHash;
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                } else
                {
                    MessageBox.Show("Mật khẩu hiện tại không đúng!", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                }
            });

            EnablePasswordEditCommand = new RelayCommand<object>((p) => true,
            (p) =>
            {
                IsEditingPassword = true;
            });
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string MD5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

    }
}
