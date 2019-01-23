using Grpc.Core;
using Helloworld;
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

namespace GreeterClientWPF
{
  /// <summary>
  /// Interaction logic for login.xaml
  /// </summary>
  public partial class login : Window
  {
    public login()
    {
      InitializeComponent();
    }

    private void btn_login_Click(object sender, RoutedEventArgs e)
    {
      var logintrue = new Greeter.GreeterClient(
        new Channel("127.0.0.1:50051", ChannelCredentials.Insecure)).LoginEvent(
        new Usercred{ Name = tb_username.Text, Password = tb_password.Text });
      bool loginsikerult = logintrue.Sikerult;
      if (loginsikerult)
      {
        MainWindow.currentUser.Username = tb_username.Text;
        this.Close();
      }
      else
        tb_hiba.Text = "nem jó név/jelszó";
    }

    private void btn_registration_Click(object sender, RoutedEventArgs e)
    {
      var registrationtrue = new Greeter.GreeterClient(
        new Channel("127.0.0.1:50051", ChannelCredentials.Insecure)).AddEvent(
        new Usercred { Name = tb_username.Text, Password = tb_password.Text });
      bool regnemsikerult = registrationtrue.Sikerult;
      if (regnemsikerult)
        tb_hiba.Text = "nem sikerult a regisztráció";
      else
        tb_hiba.Text = "sikeres regisztráció";
    }
  }
}
