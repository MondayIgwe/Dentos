namespace Elite3E.Infrastructure.Selenium
{
    using System;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.IE;
    using OpenQA.Selenium.Firefox;
    using System.Drawing;
    using OpenQA.Selenium.Remote;
    using Elite3E.Infrastructure.Configuration;

    public class Web
        {
            public static bool HeadlessMode { get; private set; }
        public Web(BrowserTypes browserType)
            {
                Use(browserType);
            }
            public IWebDriver Browser { get; private set; }

            

        private void Use(BrowserTypes browser)
            {
                switch (browser)
                {
                    case BrowserTypes.Chrome:
                    {
                        Browser = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory);
                        Browser.Manage().Window.Size = new Size(1980, 1080);
                        break;
                    }
                case BrowserTypes.HeadLessChrome:
                    {
                        var chromeOptions = new ChromeOptions();
                        chromeOptions.AddArguments("headless");
                        chromeOptions.AddArgument("--disable-gpu");
                        chromeOptions.AddUserProfilePreference("download.default_directory", Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads\");
                        chromeOptions.AddArguments("--lang=en-US");
                        chromeOptions.AddArguments("window-size=1920,1080");
                        Browser = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory, chromeOptions);
                        //Browser.Manage().Window.Size = new Size(1980, 1080);
                        HeadlessMode = true;
                        break;
                    }
                    case BrowserTypes.InternetExplorer:
                    {
                        Browser = new InternetExplorerDriver();
                        break;
                    }
                    case BrowserTypes.Firefox:
                    {
                        Browser = new FirefoxDriver();
                        break;
                    }
                    case BrowserTypes.RemoteDriver:
                    {
                        var chromeOptions = new ChromeOptions();
                        Browser = new RemoteWebDriver(new Uri(ApplicationConfigurationBuilder.Instance.RemoteUrl),chromeOptions);
                        break;
                    }

                default:
                    {
                        Browser = new ChromeDriver();
                        break;
                    }
                }

                Browser.Manage().Window.Maximize();

                Browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            }
        }
    }
