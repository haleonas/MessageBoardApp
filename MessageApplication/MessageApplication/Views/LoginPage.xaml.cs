﻿using MessageApplication.Viewmodel;
using Xamarin.Forms;

namespace MessageApplication.Views
{
    public partial class LoginPage : ContentPage
    {
        
        public LoginPage()
        {
            InitializeComponent();
            
            var viewmodel = new LoginViewModel();
            BindingContext = viewmodel;
           
        }
    }
}