using System;
using System.Threading;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.GenearlJournal;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.GeneralJournal
{
    public class GeneralJournalEntryStatus: IQuestion<string>
    {
        public string Record { get; set; }

        GeneralJournalEntryStatus(string record) => Record = record;

        public static GeneralJournalEntryStatus Value(string record) => new(record);


        public string RequestAs(IActor actor)
        {
            var status = string.Empty;

            for (var retry = 0; retry < 12; retry++)
            {
                actor.AttemptsTo(SearchProcess.ByName("General Journal Entry"));
                actor.AttemptsTo(QuickFind.Search(Record));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                status = actor.AsksFor(Text.Of(GeneralJournalLocators.Status)).Trim();
                if (status.Contains("Posted") || status.Equals("Waiting for Period to open"))
                {
                    break;
                }
                actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
                Console.WriteLine("The Value of GJ status : " + status);
                Thread.Sleep(TimeSpan.FromSeconds(5));
            }
            return status;
        }
    }
}
