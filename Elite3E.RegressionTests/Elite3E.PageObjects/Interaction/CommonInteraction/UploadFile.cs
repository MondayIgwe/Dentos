using System.IO;
using System.Reflection;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.PageObjects.PageLocators.CommonLocators;

namespace Elite3E.PageObjects.Interaction.CommonInteraction
{
    public class UploadFile : ITask
    {
        public string FileName { get; }

        private UploadFile(string fileName)
        {
            FileName = fileName;
        }

        public static UploadFile Upload(string fileName) => new(fileName);

        public void PerformAs(IActor actor)
        {
            //Uploads a file. Filename must include the file extension and file must be present in the Resources folder.
            //Resources folder: Global_UI_Automation\Elite3E.RegressionTests\Elite3E.RegressionTests\Resources
            var driver = actor.Using<BrowseTheWeb>().WebDriver;
            var input = driver.FindElement(CommonLocator.FileInput.Query);

            input.SendKeys(GetResourceFilePath(FileName));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        private string GetResourceFilePath(string fileName)
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) +
                   "\\Resources\\" + fileName;
        }
    }
}
