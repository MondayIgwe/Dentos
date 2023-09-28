using Boa.Constrictor.Screenplay;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.UDF;
using System.Collections.Generic;
using System.Linq;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.UDFValidationListMapping
{
    public class UDFValidationListMappingFields : IQuestion<bool>
    {
        public static UDFValidationListMappingFields doFieldsExist() => new();

        public bool RequestAs(IActor actor)
        {
            List<bool> fieldsAvailable = new List<bool>();
            fieldsAvailable.Add(actor.AsksFor(Field.IsAvailable(UDFLocators.code)));
            fieldsAvailable.Add(actor.AsksFor(Field.IsAvailable(UDFLocators.description)));
            fieldsAvailable.Add(actor.AsksFor(Field.IsAvailable(UDFValidationListMappingLocators.parentList)));
            fieldsAvailable.Add(actor.AsksFor(Field.IsAvailable(UDFValidationListMappingLocators.childList)));
            fieldsAvailable.Add(actor.AsksFor(Field.IsAvailable(UDFValidationListMappingLocators.isActive)));
            fieldsAvailable.Add(actor.AsksFor(Field.IsAvailable(UDFValidationListMappingLocators.isDefault)));
            fieldsAvailable.Add(actor.AsksFor(Field.IsAvailable(UDFValidationListMappingLocators.sortString)));
            fieldsAvailable.Add(actor.AsksFor(Field.IsAvailable(UDFValidationListMappingLocators.startDate)));
            fieldsAvailable.Add(actor.AsksFor(Field.IsAvailable(UDFValidationListMappingLocators.endDate)));
            bool doesExist = fieldsAvailable.Any(x => (x != true));
            return doesExist;
        }
    }
}
