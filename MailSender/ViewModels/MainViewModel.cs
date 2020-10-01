using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using MailSender.Data;
using MailSender.Infrastructure.Commands;
using MailSender.lib.Interfaces;
using MailSender.Models;
using MailSender.ViewModels.Base;

namespace MailSender.ViewModels
{
    class MainViewModel:ViewModel
    {
        private string _Title = "MailSender";

        private IMailService _MailService;
        private ObservableCollection<Server> _Servers;
        private ObservableCollection<Sender> _Senders;
        private ObservableCollection<Recipient> _Recipients;
        private ObservableCollection<Message> _Messages;
        private Server _SelectedServer;
        private Message _SelectedMessage;
        private Sender _SelectedSender;
        private Recipient _SelectedRecipient;

        #region Свойства
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        public ObservableCollection<Server> Servers
        {
            get => _Servers;
            set => Set(ref _Servers, value);
        }

        public ObservableCollection<Sender> Senders
        {
            get => _Senders;
            set => Set(ref _Senders, value);
        }

        public ObservableCollection<Recipient> Recipients
        {
            get => _Recipients;
            set => Set(ref _Recipients, value);
        }

        public ObservableCollection<Message> Messages
        {
            get => _Messages;
            set => Set(ref _Messages, value);
        }

        public Server SelectedServer
        {
            get => _SelectedServer;
            set => Set(ref _SelectedServer, value);
        }       
        
        public Message SelectedMessage
        {
            get => _SelectedMessage;
            set => Set(ref _SelectedMessage, value);
        }
        public Sender SelectedSender
        {
            get => _SelectedSender;
            set => Set(ref _SelectedSender, value);
        }
        public Recipient SelectedRecipient
        {
            get => _SelectedRecipient;
            set => Set(ref _SelectedRecipient, value);
        }

        #endregion


        #region Commands

        #region CreateNewServerCommand
        private ICommand _CreateNewServerCommand;

        public ICommand CreateNewServerCommand => _CreateNewServerCommand
            ??= new DelegateCommand(OnCreateNewServerCommandExecuted, CanCreateNewServerCommandExecute);

        private bool CanCreateNewServerCommandExecute(object p) => true;
        private void OnCreateNewServerCommandExecuted(object p)
        {
            MessageBox.Show("Create new server");
        }
        #endregion

        #region EditServerCommand
        private ICommand _EditServerCommand;

        public ICommand EditServerCommand => _EditServerCommand
            ??= new DelegateCommand(OnEditServerCommandExecuted, CanEditServerCommandExecute);

        private bool CanEditServerCommandExecute(object p) => p is Server || SelectedServer != null;
        private void OnEditServerCommandExecuted(object p)
        {
            var server = p as Server ?? SelectedServer;
            if (server is null) return;
            MessageBox.Show($"Edit server {server.Address }","Управление серверами");
        }
        #endregion

        #region DeleteServerCommand
        private ICommand _DeleteServerCommand;

        public ICommand DeleteServerCommand => _DeleteServerCommand
            ??= new DelegateCommand(OnDeleteServerCommandExecuted, CanDeleteServerCommandExecute);

        private bool CanDeleteServerCommandExecute(object p) => p is Server || SelectedServer != null;
        private void OnDeleteServerCommandExecuted(object p)
        {
            var server = p as Server ?? SelectedServer;
            if (server is null) return;
            Servers.Remove(server);
            SelectedServer = Servers.FirstOrDefault();
            MessageBox.Show($"Delete server {server.Address }", "Управление серверами");
        }
        #endregion

        #region SendMailCommand
        private ICommand _SendMailCommand;

        public ICommand SendMailCommand => _SendMailCommand
            ??= new DelegateCommand(OnSendMailCommandExecuted, CanSendMailCommandExecute);

        private bool CanSendMailCommandExecute(object p)
        {
            if (SelectedServer is null) return false;
            if (SelectedSender is null) return false;
            if (SelectedMessage is null) return false;
            if (SelectedRecipient is null) return false;
            return true;
        }
        private void OnSendMailCommandExecuted(object p)
        {
            var server = SelectedServer;   
            var sender = SelectedSender;   
            var recipient = SelectedRecipient;   
            var message = SelectedMessage;
            var mail_sender = _MailService.GetSender(server.Address, server.Port, server.UseSSL, server.Login, server.Password);
            mail_sender.Send(sender.Address, recipient.Address, message.Subject, message.Body);
        }
        #endregion

        #endregion
        public MainViewModel(IMailService MailService)
        {
            _MailService = MailService;
            Servers = new ObservableCollection<Server>(TestData.Servers);
            Senders = new ObservableCollection<Sender>(TestData.Senders);
            Recipients = new ObservableCollection<Recipient>(TestData.Recipients);
            Messages = new ObservableCollection<Message>(TestData.Messages);
        }
    }
}
