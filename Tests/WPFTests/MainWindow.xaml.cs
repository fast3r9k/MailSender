using System;
using System.Net;
using System.Net.Mail;
using System.Windows;

namespace WPFTests
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public void OnSendButtonClick(object sender, RoutedEventArgs e)
        {
           


            var from = Sender.From;
            var to = Sender.To;

            var message = Sender.Message;
           
            var client = new SmtpClient(Sender.Host, Sender.Port);
            client.EnableSsl = true;

            client.Credentials = new NetworkCredential
            {
                UserName = LoginEdit.Text,
                SecurePassword = PasswordEdit.SecurePassword
            };            
            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                ErrorWindow error = new ErrorWindow();
                error.txtError.Text = $"Описание ошибки: {ex.Message}";
                error.ShowDialog();
            }

            SendEndWindow sew = new SendEndWindow();
            sew.ShowDialog();


        }
    }
}
