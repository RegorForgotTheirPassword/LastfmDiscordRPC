﻿using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace LastfmDiscordRPC.MVVM.Views;

public partial class Footer : UserControl
{
    public Footer()
    {
        InitializeComponent();
    }

    private void SourceButton_OnClick(object sender, RoutedEventArgs e)
    {
        const string url = @"https://github.com/RegorForgotTheirPassword/LastfmDiscordRPC";
        ProcessStartInfo sInfo = new ProcessStartInfo(url)
        {
            UseShellExecute = true
        };
        Process.Start(sInfo);
    }
}