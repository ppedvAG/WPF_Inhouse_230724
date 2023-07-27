using FlaUI.Core.AutomationElements;
using FlaUI.UIA3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using FluentAssertions;

namespace ppedv.CheesyDrive.UI.WPF.Tests
{
    [TestClass]
    public class CarViewTests
    {
        readonly string appPath = @"C:\Users\ar2\source\repos\WPF_Inhouse_230724\ppedv.CheesyDrive\ppedv.CheesyDrive.UI.WPF\bin\Debug\net48\ppedv.CheesyDrive.UI.WPF.exe";// typeof(MainWindow).Assembly.Location;//.Replace(".dll", ".exe");


        FlaUI.Core.Application app;
        UIA3Automation automation;
        Window win;

        [TestMethod]
        [TestCategory( "UITest")]
        public void Start_app_should_display_some_Cars()
        {
            app = FlaUI.Core.Application.Launch(appPath);
            app.WaitWhileMainHandleIsMissing();
            automation = new UIA3Automation();
            win = app.GetMainWindow(automation);
            win.WaitUntilClickable();
            //win.WaitUntilEnabled();

            //win.FindFirstDescendant("mp").WaitUntilClickable();
            //var loadBtn = win.FindFirstDescendant("b1").AsButton();
            //loadBtn.Click();

            //Thread.Sleep(100);
            var grid = win.FindFirstDescendant(x => x.ByAutomationId("grid1")).AsDataGridView();

            grid.Rows.Length.Should().BeGreaterThan(1);
        }

    }
}
