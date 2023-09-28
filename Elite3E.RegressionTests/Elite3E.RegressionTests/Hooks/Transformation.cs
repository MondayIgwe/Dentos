using System;
using TechTalk.SpecFlow;
using Elite3E.Infrastructure.Extensions;

namespace Elite3E.RegressionTests.Hooks
{

    [Binding]
    public class Transformation
    {
        [StepArgumentTransformation]
        public Table ReplaceDynamicTableValues(Table table)
        {
            table.ReplaceDynamicValues();

            return table;
        }

        [StepArgumentTransformation]
        public string[] TransformToArrayOfStrings(string commaSeparatedStepArgumentValues)
        {
            var stringSeparators = new[] { "," };
            return commaSeparatedStepArgumentValues.Split(stringSeparators, StringSplitOptions.None);
        }
    }
}