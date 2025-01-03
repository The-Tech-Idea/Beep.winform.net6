using TheTechIdea.Beep.Vis.Modules;
using TheTechIdea.Beep.Winform.Controls;
using TheTechIdea.Beep.Winform.Controls.Converters;
using TheTechIdea.Beep.Winform.Controls.Helpers;
using WinFormsApp.UI.Test.Properties;


namespace WinFormsApp.UI.Test
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            EnumBeepThemes theme = EnumBeepThemes.ZenTheme;
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            var x = new Form1();
            x.Theme = theme;
            BeepSplashScreen beepSplashScreen = new BeepSplashScreen();
            beepSplashScreen.Title = "Beep Splash Screen";
            beepSplashScreen.Theme = theme;
            
            //  beepSplashScreen.Message = "Loading...";
            // set the logo image from resourses slack.svg
            //      ImageTools.GetGraphicFilesLocationsFromEmbedded(new string[] { "System" });
            beepSplashScreen.LogoPath = "WinFormsApp.UI.Test.gfx.slack.svg";
            beepSplashScreen.ShowWithFadeIn();
            Application.DoEvents();
            Thread.Sleep(5000);
            beepSplashScreen.HideWithFadeOut();
            BeepWaitScreen beepWaitScreen = new BeepWaitScreen();
            beepWaitScreen.Theme = theme;
            beepWaitScreen.ShowAndRunAsync(async () =>
            {
                await Task.Delay(5000);
            });
            Application.Run(x);

        }
    }
}