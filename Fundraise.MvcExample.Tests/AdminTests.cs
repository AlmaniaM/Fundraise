﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Firefox;
using Fundraise.MvcExample.Tests.Config;
using OpenQA.Selenium;

namespace Fundraise.MvcExample.Tests
{
    [TestClass]
    public class AdminTests
    {
        public static IisExpressWebServer WebServer;
        public static FirefoxDriver Browser;

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            var app = new WebApplication(ProjectLocation.FromFolder("Fundraise.MvcExample"), 12365);
            WebServer = new IisExpressWebServer(app);
            WebServer.Start();

            Browser = new FirefoxDriver();
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            Browser.Quit();
            WebServer.Stop();
        }

        [TestMethod]
        public void CreateNewAccount()
        {
            Browser.Manage().Window.Maximize();
            Browser.Navigate().GoToUrl("http://localhost:12365/Account/Register");

            var emailBox = Browser.FindElementById("Email");
            emailBox.SendKeys("test@alexlindgren.com");

            var passwordBox = Browser.FindElementById("Password");
            passwordBox.SendKeys("test1234");

            var confirmPasswordBox = Browser.FindElementById("ConfirmPassword");
            confirmPasswordBox.SendKeys("test1234");

            var submitButton = Browser.FindElementById("RegisterSubmit");
            submitButton.Submit();

            Assert.IsTrue(Browser.Url == "http://localhost:12365/", "The browser should redirect to 'http://localhost:12365/'");
            Assert.IsTrue(Browser.PageSource.Contains(""), "After registering, browser should display 'Hello test@alexlindgren.com!'");
        }
    }
}
