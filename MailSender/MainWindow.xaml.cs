using MailSender.Models;
using MailSender.lib;
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
using System.Net.Mail;

namespace MailSender
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

        private void RecipientsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        //private void OnSenButtonClick(object Sender, RoutedEventArgs e)
        //{
        //    var sender = SendersList.SelectedItem as Sender;
        //    if (sender is null) return;
        //    if (!(RecipientsList.SelectedItem is Recipient recipient)) return;
        //    if (!(ServerList.SelectedItem is Server server)) return;
        //    if (!(MessagesList.SelectedItem is Message message))
        //    {
        //        MessageBox.Show("Вы пытаетесь отправить пустое письмо", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
        //        myTab.SelectedIndex = 2;
        //        return;
        //    }

        //    var send_service = new MailSenderService
        //    {
        //        ServerAddress = server.Address,
        //        ServerPort = server.Port,
        //        UseSSL = server.UseSSL,
        //        Login = server.Login,
        //        Password = server.Password,
        //    };

        //    try
        //    {
        //        send_service.SendMessage(sender.Address, recipient.Address, message.Subject, message.Body);
        //    }
        //    catch (SmtpException ex)
        //    {
        //        MessageBox.Show("Возникла ошибка при отправке " + ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}

        //private void ScheduleClick(object sender, RoutedEventArgs e)
        //{
        //    myTab.SelectedIndex = 1;
        //}
    }
}
