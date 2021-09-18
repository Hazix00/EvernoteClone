using EvernoteClone.Model;
using EvernoteClone.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvernoteClone.ViewModel
{
    public class LoginVM
    {
        public User User { get; set; }
        public RegisterCommand RegisterCommand { get; }
        public LoginCommand LoginCommand { get; }

        public LoginVM()
        {
            RegisterCommand = new RegisterCommand(this);
            LoginCommand = new LoginCommand(this);
        }
    }
}
