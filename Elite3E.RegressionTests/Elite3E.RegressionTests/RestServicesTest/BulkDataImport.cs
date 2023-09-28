using Elite3E.Infrastructure.Extensions;
using Elite3E.Infrastructure.Helper;
using Elite3E.RegressionTests.DataCreators;
using Elite3E.RestServices.Entity;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.RegressionTests.RestServicesTest
{
    public class BulkDataImport
    {
        string fileName = "Data Import v0.4.xlsx";

        [Test]
        public async Task ExcelEntityOrganisationData()
        {
            var dataReader = new DataReader(fileName);

            var result = dataReader.LoadDataTableFromExcelSheet("1_Entity_Organisation");

            var entityData = new EntityData();


            for (var i = 1; i < result.Rows.Count; i++)
            {
                var entity = new ApiEntity();
                try
                {
                    entity.OrganisationName = result.Rows[i].ItemArray[0]?.ToString()!.Trim();
                    entity.EntityType = result.Rows[i].ItemArray[1]?.ToString()!.Trim();
                    entity.SiteDescription = result.Rows[i].ItemArray[3]?.ToString()!.Trim();
                    entity.SiteType = result.Rows[i].ItemArray[4]?.ToString()!.Trim();
                    //entity.Address = result.Rows[i].ItemArray[5]?.ToString()!.Trim(); //This is a lookup
                    entity.AddressOrganisation = result.Rows[i].ItemArray[5]?.ToString()!.Trim();
                    entity.Street = result.Rows[i].ItemArray[6]?.ToString()!.Trim();
                    entity.City = result.Rows[i].ItemArray[7]?.ToString()!.Trim();
                    entity.PostCode = result.Rows[i].ItemArray[8]?.ToString()!.Trim();
                    entity.Country = result.Rows[i].ItemArray[9]?.ToString()!.Trim();
                    entity.LanguageKey = result.Rows[i].ItemArray[10]?.ToString()!.Trim();

                    await entityData.SearchOrCreateAnEntity(entity);
                }
                catch (Exception e)
                {
                    Console.WriteLine(entity.OrganisationName.String);
                    Console.WriteLine(e.Message);
                }

            }
        }

        [Test]
        public async Task ExcelEntityPersonData()
        {
            var dataReader = new DataReader(fileName);

            var result = dataReader.LoadDataTableFromExcelSheet("1_Entity_Person");

            var entityData = new EntityData();

            for (var i = 1; i < result.Rows.Count; i++)
            {
                var entityPerson = new ApiEntity();
                try
                {
                    entityPerson.EntityType = result.Rows[i].ItemArray[0]?.ToString()!.Trim();
                    entityPerson.PersonType = result.Rows[i].ItemArray[1]?.ToString()!.Trim();
                    entityPerson.FirstName = result.Rows[i].ItemArray[2]?.ToString()!.Trim();
                    entityPerson.LastName = result.Rows[i].ItemArray[3]?.ToString()!.Trim();
                    entityPerson.FormatCode = result.Rows[i].ItemArray[4]?.ToString()!.Trim();
                    entityPerson.SiteDescription = result.Rows[i].ItemArray[6]?.ToString()!.Trim();
                    entityPerson.SiteType = result.Rows[i].ItemArray[7]?.ToString()!.Trim();
                    entityPerson.AddressOrganisation = result.Rows[i].ItemArray[8]?.ToString()!.Trim();
                    entityPerson.Street = result.Rows[i].ItemArray[9]?.ToString()!.Trim();
                    entityPerson.City = result.Rows[i].ItemArray[10]?.ToString()!.Trim();
                    entityPerson.PostCode = result.Rows[i].ItemArray[11]?.ToString()!.Trim();
                    entityPerson.Country = result.Rows[i].ItemArray[12]?.ToString()!.Trim();
                    entityPerson.LanguageKey = result.Rows[i].ItemArray[13]?.ToString()!.Trim();

                    await entityData.SearchOrCreateAnEntity(entityPerson);
                }
                catch (Exception ex)
                {
                    Console.Write("Error: " + ex.Message);
                    Console.Write($"{entityPerson.FirstName.String} {entityPerson.FirstName.String}");
                }
            }
        }

        [Test]
        public async Task ExcelEntityFeeEarnerData()
        {
            var dataReader = new DataReader(fileName);

            var result = dataReader.LoadDataTableFromExcelSheet("2_Fee_Earner");

            var entityData = new FeeEarnerData();

            for (var i = 1; i < result.Rows.Count; i++)
            {
                var feeEarner = new ApiFeeEarnerEntity();
                {
                    try
                    {

                        feeEarner.EntityName = result.Rows[i].ItemArray[0]?.ToString()!.Trim();

                        var entity = new ApiEntity();

                        // Required to Identify if it is a Person or Organisation
                        if (result.Rows[i].ItemArray[1].ToString()
                            .Equals("P", StringComparison.CurrentCultureIgnoreCase))
                        {
                            var firstSpaceIndex = feeEarner.EntityName.IndexOf(" ", StringComparison.Ordinal);
                            entity.FirstName = feeEarner.EntityName.Substring(0, firstSpaceIndex);
                            entity.LastName = feeEarner.EntityName.Substring(firstSpaceIndex + 1).Trim();

                            entity.FormattedName = feeEarner.EntityName;
                        }
                        else
                        {
                            entity.OrganisationName = feeEarner.EntityName;
                        }

                        feeEarner.EDIStartDate = result.Rows[i].ItemArray[2]?.ToString()!.Trim();
                        feeEarner.EDIStartDate = DateTime.Parse(feeEarner.EDIStartDate, new CultureInfo("en-GB", true)).ToString("MM/dd/yyyy");
                        feeEarner.Office = result.Rows[i].ItemArray[3]?.ToString()!.Trim();
                        feeEarner.Department = result.Rows[i].ItemArray[4]?.ToString()!.Trim();
                        feeEarner.Section = result.Rows[i].ItemArray[5]?.ToString()!.Trim();
                        feeEarner.Title = result.Rows[i].ItemArray[6]?.ToString()!.Trim();
                        feeEarner.RateClass = result.Rows[i].ItemArray[7]?.ToString()!.Trim();

                        //Fee Earner Rate
                        feeEarner.RateTypeDescription = result.Rows[i].ItemArray[8]?.ToString()!.Trim();
                        feeEarner.FeeEarnerRatesStartDate = result.Rows[i].ItemArray[9]?.ToString()!.Trim();
                        feeEarner.FeeEarnerRatesStartDate = DateTime.Parse(feeEarner.FeeEarnerRatesStartDate, new CultureInfo("en-GB", true)).ToString("MM/dd/yyyy");
                        feeEarner.EffectiveRate = result.Rows[i].ItemArray[10]?.ToString()!.Trim();
                        feeEarner.EffectiveRateCurrencyDescription = result.Rows[i].ItemArray[11]?.ToString();
                        await entityData.SearchAndCreateFeeEranerData(feeEarner, entity);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(feeEarner.EntityName);
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }

        [Test]
        public async Task ExcelClientData()
        {
            var dataReader = new DataReader(fileName);

            var result = dataReader.LoadDataTableFromExcelSheet("3_Client");

            var clientData = new ClientMaintenanceData();

            for (var i = 1; i < result.Rows.Count; i++)
            {
                var client = new ApiClientMaintenanceEntity();

                try
                {
                    client.EntityName = result.Rows[i].ItemArray[0]?.ToString()!.Trim();

                    client.FeeEarnerFullName = result.Rows[i].ItemArray[1]?.ToString()!.Trim();
                    client.DateOpened = result.Rows[i].ItemArray[2]?.ToString()!.Trim();
                    client.DateOpened = DateTime.Parse(client.DateOpened, new CultureInfo("en-GB", true)).ToString("MM/dd/yyyy");
                    client.Status = result.Rows[i].ItemArray[3]?.ToString()!.Trim();

                    client.Currency = result.Rows[i].ItemArray[4]?.ToString()!.Trim();
                    client.Country = result.Rows[i].ItemArray[5]?.ToString()!.Trim();
                    client.InvoiceSiteName = result.Rows[i].ItemArray[6]?.ToString()!.Trim();
                    client.EDIStartDate = result.Rows[i].ItemArray[7]?.ToString()!.Trim();
                    client.EDIStartDate = DateTime.Parse(client.EDIStartDate, new CultureInfo("en-GB", true)).ToString("MM/dd/yyyy");
                    client.BillingFeeEarnerName = result.Rows[i].ItemArray[8]?.ToString()!.Trim();
                    client.ResponsibleFeeEarnerName = result.Rows[i].ItemArray[9]?.ToString()!.Trim();
                    client.SupervisingFeeEarnerName = result.Rows[i].ItemArray[10]?.ToString()!.Trim();
                    client.Office = result.Rows[i].ItemArray[11]?.ToString()!.Trim();

                    await clientData.ClientData(client);
                }
                catch (Exception e)
                {
                    Console.WriteLine(client.EntityName);
                    Console.WriteLine(e.Message);
                }
            }
        }

        [Test]
        public async Task ExcelChargeTypeData()
        {
            var dataReader = new DataReader(fileName);

            var result = dataReader.LoadDataTableFromExcelSheet("4_Charge_Type");

            var chargeTypeGroup = new ChargeTypeData();


            for (var i = 1; i < result.Rows.Count; i++)
            {

                var chargeTypeEntity = new ApiChargeTypeEntity
                {
                    ChargeCode = result.Rows[i].ItemArray[0]?.ToString()!.Trim(),
                    Description = result.Rows[i].ItemArray[1]?.ToString()!.Trim(),
                    CategoryInput = result.Rows[i].ItemArray[2]?.ToString()!.Trim(),
                    TransactionTypeAlias = result.Rows[i].ItemArray[3]?.ToString()!.Trim(),
                    Active = "1"
                };

                try
                {
                    await chargeTypeGroup.SearchAndCreateChargeTypeDataAsync(chargeTypeEntity);
                }
                catch (Exception e)
                {
                    Console.WriteLine(chargeTypeEntity.Description);
                    Console.WriteLine(e.Message);
                }
            }
        }

        [Test]
        public async Task ExcelChargeTypeGroupData()
        {
            var dataReader = new DataReader(fileName);

            var result = dataReader.LoadDataTableFromExcelSheet("5_Charge_Type_Group");

            var chargeTypeGroup = new ChargeTypeData();

            for (var i = 1; i < result.Rows.Count; i++)
            {

                var chargeTypeGroupEntity = new ApiChargeTypeGroupEntity
                {
                    ChargeTypeGroupCode = result.Rows[i].ItemArray[0]?.ToString()!.Trim(),
                    ChargeTypeGroupDescription = result.Rows[i].ItemArray[1]?.ToString()!.Trim(),
                    ChargeTypeGroupExcludeOrIncludeListOption = "IsIncludeList",
                    ChargeTypeGroupIsExcludeOrIncludeListValue = 1
                };
                try
                {
                    var includeOrExclude = result.Rows[i].ItemArray[2]?.ToString()!.Trim();

                    if (includeOrExclude.StartsWith("E", StringComparison.InvariantCultureIgnoreCase))
                    {
                        chargeTypeGroupEntity.ChargeTypeGroupExcludeOrIncludeListOption = "IsExcludeList";
                    }

                    // Start Date not added
                    var chargeTypes = result.Rows[i].ItemArray[4]?.ToString()!.Trim().Split(",");

                    var chargeTypeEntities = chargeTypes
                        .Select(chargeType => new ApiChargeTypeEntity() { Description = chargeType.Trim() }).ToList();

                    await chargeTypeGroup.SearchChargeTypeGroupAndAddChargeTypeDataAsync(chargeTypeGroupEntity, chargeTypeEntities);
                }

                catch (Exception e)
                {
                    Console.WriteLine(chargeTypeGroupEntity.ChargeTypeGroupDescription);
                    Console.WriteLine(e.Message);
                }
            }
        }

        [Test]
        public async Task ExcelDisbursementTypeData()
        {
            var dataReader = new DataReader(fileName);

            var result = dataReader.LoadDataTableFromExcelSheet("6_Disbursement_Type");

            var costTypeData = new CostTypeData();

            for (var i = 1; i < result.Rows.Count; i++)
            {
                var disbursementTypeEntity = new ApiDisbursementTypeEntity
                {
                    Code = result.Rows[i].ItemArray[0]?.ToString()!.Trim(),
                    Description = result.Rows[i].ItemArray[1]?.ToString()!.Trim(),
                    TransactionTypeAlias = result.Rows[i].ItemArray[2]?.ToString()!.Trim(),
                    IsHardDisbursementOrSoftDisbursementOption = "IsSoftCost",
                    IsHardDisbursementOrSoftDisbursementValue = 1
                };

                var hardOrSoft = result.Rows[i].ItemArray[3]?.ToString()!.Trim();

                if (hardOrSoft.StartsWith("H", StringComparison.InvariantCultureIgnoreCase))
                {
                    disbursementTypeEntity.IsHardDisbursementOrSoftDisbursementOption = "IsHardCost";
                }

                try
                {
                    await costTypeData.SearchAndCreateDisbursementTypeDataAsync(disbursementTypeEntity);
                }
                catch (Exception e)
                {
                    Console.WriteLine(disbursementTypeEntity.Description);
                    Console.WriteLine(e.Message);
                }
            }
        }

        [Test]
        public async Task ExcelCostTypeGroupData()
        {
            var dataReader = new DataReader(fileName);

            var result = dataReader.LoadDataTableFromExcelSheet("7_Cost_Type_Group");

            var costTypeData = new CostTypeData();

            for (var i = 1; i < result.Rows.Count; i++)
            {
                var costTypeGroupEntity = new ApiCostTypeGroupEntity
                {
                    Code = result.Rows[i].ItemArray[0]?.ToString()!.Trim(),
                    Description = result.Rows[i].ItemArray[1]?.ToString()!.Trim(),
                    CostTypeGroupExcludeOrIncludeListOption = "IsIncludeList",
                    CostTypeGroupIsExcludeOrIncludeListValue = 1
                };

                var includeOrExclude = result.Rows[i].ItemArray[2]?.ToString()!.Trim();

                if (includeOrExclude.StartsWith("E", StringComparison.InvariantCultureIgnoreCase))
                {
                    costTypeGroupEntity.CostTypeGroupExcludeOrIncludeListOption = "IsExcludeList";
                }

                // Start Date is not there in APiEntity

                var disbursementTypes = result.Rows[i].ItemArray[4]?.ToString()!.Trim().Split(",");

                var disbursementTypeEntities = disbursementTypes.Select(disbursementType =>
                    new ApiDisbursementTypeEntity() { Description = disbursementType.Trim() }).ToList();

                await costTypeData.SearchDisbursementTypeGroupAndAddDisbursementTypeDataAsync(costTypeGroupEntity, disbursementTypeEntities);
            }
        }

        // To Do
        [Test]
        public async Task ExcelMatterData()
        {
            var dataReader = new DataReader(fileName);

            var result = dataReader.LoadDataTableFromExcelSheet("8_Matter");

            var costTypeData = new CostTypeData();

            for (var i = 1; i < result.Rows.Count; i++)
            {
                var matterEntity = new ApiMatterEntity
                {
                    MatterName = result.Rows[i].ItemArray[0]?.ToString()!.Trim(),
                    Status = result.Rows[i].ItemArray[1]?.ToString()!.Trim(),
                    OpenDate = result.Rows[i].ItemArray[2]?.ToString()!.Trim(),
                    Client = result.Rows[i].ItemArray[3]?.ToString()!.Trim(),
                    FeeEarnerFullName = result.Rows[i].ItemArray[4]?.ToString()!.Trim(),
                    Currency = result.Rows[i].ItemArray[6]?.ToString()!.Trim(),
                    MatterCurrencyMethod = result.Rows[i].ItemArray[7]?.ToString()!.Trim(),
                    Office = result.Rows[i].ItemArray[12]?.ToString()!.Trim(),
                    Department = result.Rows[i].ItemArray[13]?.ToString()!.Trim(),
                    Section = result.Rows[i].ItemArray[14]?.ToString()!.Trim(),
                    Rate = result.Rows[i].ItemArray[15]?.ToString()!.Trim(),
                    PayorName = result.Rows[i].ItemArray[16].ToString()!.Trim()
                };
                if (result.Rows[i].ItemArray[8]?.ToString()!.Trim().Length > 0)
                {
                    var billingGroups = result.Rows[i].ItemArray[8]?.ToString()!.Trim().Split(",");
                    matterEntity.BillingGroupList = billingGroups.ToList();

                }

                if (result.Rows[i].ItemArray[9]?.ToString()!.Trim().Length > 0)
                {
                    var costTypeGroups = result.Rows[i].ItemArray[9]?.ToString()!.Trim().Split(",");
                    matterEntity.CostTypeGroupList = costTypeGroups.ToList();
                }

                if (result.Rows[i].ItemArray[10]?.ToString()!.Trim().Length > 0)
                {
                    var chargeTypeGroups = result.Rows[i].ItemArray[10]?.ToString()!.Trim().Split(",");
                    matterEntity.ChargeTypeGroupList = chargeTypeGroups.ToList();
                }

                if (result.Rows[i].ItemArray[11]?.ToString()!.Trim().Length > 0)
                {
                    matterEntity.BillingOffice = result.Rows[i].ItemArray[11]?.ToString()!.Trim();
                }
                await new MatterMaintenanceData().CreateMatter(matterEntity);
            }
        }


        // To Do
        [Test]
        public async Task ExcelMatterData_Lite()
        {
            var dataReader = new DataReader(fileName);

            var result = dataReader.LoadDataTableFromExcelSheet("6_Matter");

            var costTypeData = new CostTypeData();

            for (var i = 1; i < result.Rows.Count; i++)
            {
                var matterEntity = new ApiMatterEntity
                {
                    MatterName = result.Rows[i].ItemArray[0]?.ToString()!.Trim(),
                    Status = result.Rows[i].ItemArray[1]?.ToString()!.Trim(),
                    OpenDate = DateTime.Parse(result.Rows[i].ItemArray[2]?.ToString()!.Trim(), new CultureInfo("en-GB", true)).ToString("MM/dd/yyyy"),
                    Client = result.Rows[i].ItemArray[3]?.ToString()!.Trim(),
                    FeeEarnerFullName = result.Rows[i].ItemArray[4]?.ToString()!.Trim(),
                    Currency = result.Rows[i].ItemArray[6]?.ToString()!.Trim(),
                    MatterCurrencyMethod = result.Rows[i].ItemArray[7]?.ToString()!.Trim(),
                    Office = result.Rows[i].ItemArray[10]?.ToString()!.Trim(),
                    Department = result.Rows[i].ItemArray[11]?.ToString()!.Trim(),
                    Section = result.Rows[i].ItemArray[12]?.ToString()!.Trim(),
                    EDIPractice = result.Rows[i].ItemArray[13]?.ToString()!.Trim(),
                    Rate = result.Rows[i].ItemArray[14]?.ToString()!.Trim()
                };

                if (result.Rows[i].ItemArray[11]?.ToString()!.Trim().Length > 0)
                {
                    matterEntity.BillingOffice = result.Rows[i].ItemArray[9]?.ToString()!.Trim();
                }
                await new MatterMaintenanceData().CreateMatter_Lite(matterEntity);
            }
        }



        [Test]
        public async Task ExcelProformaEdit()
        {
            var dataReader = new DataReader(fileName);

            var result = dataReader.LoadDataTableFromExcelSheet("14_ProformaEdit");
            for (var i = 1; i < result.Rows.Count; i++)
            {
                var proformaEditEntityData = new ApiProformaEntity
                {
                    MatterNumber = result.Rows[i].ItemArray[0]?.ToString()!.Trim(),
                    MatterName = result.Rows[i].ItemArray[1]?.ToString()!.Trim()
                };
                await new ProformaEditData().SearchAndBillProformaAsync(proformaEditEntityData);
            }
        }


        [Test]
        public async Task ExcelReceiptsApplyReverse()
        {
            var dataReader = new DataReader(fileName);

            var result = dataReader.LoadDataTableFromExcelSheet("15_ReceiptsApplyReverse");
            for (var i = 1; i < result.Rows.Count; i++)
            {
                var receiptsEntityData = new ApiReceiptsApplyReverseEntity
                {
                    ReceiptTypeAlias = result.Rows[i].ItemArray[0]?.ToString()!.Trim(),
                    MatterNumber = result.Rows[i].ItemArray[1]?.ToString()!.Trim(),
                    DocumentNumber = string.IsNullOrEmpty(result.Rows[i].ItemArray[2]?.ToString()!.Trim())? "Doc_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+10"): result.Rows[i].ItemArray[2]?.ToString()!.Trim(),
                    Narrative = string.IsNullOrEmpty(result.Rows[i].ItemArray[3]?.ToString()!.Trim()) ? StepArgumentExtension.ReplaceDynamicValues("{Auto}+15"): result.Rows[i].ItemArray[3]?.ToString()!.Trim()
                };

                await new ReceiptsApplyReverseData().AddReceiptWithInvoice(receiptsEntityData);
            }
        }

        [Test]
        public async Task ExcelCreateClientAccountReceipt()
        {
            var dataReader = new DataReader(fileName);

            var result = dataReader.LoadDataTableFromExcelSheet("16_ClientAccountReceipt");
            for (var i = 1; i < result.Rows.Count; i++)
            {
                var receiptsEntityData = new ApiClientAccountReceiptEntity
                {
                    ClientAccountReceiptType = result.Rows[i].ItemArray[0]?.ToString()!.Trim(),
                    MatterNumber = result.Rows[i].ItemArray[1]?.ToString()!.Trim(),
                    DocumentNumber = string.IsNullOrEmpty(result.Rows[i].ItemArray[2]?.ToString()!.Trim()) ? "Receipt_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+8") : result.Rows[i].ItemArray[2]?.ToString()!.Trim(),
                    Amount = string.IsNullOrEmpty(result.Rows[i].ItemArray[3]?.ToString()!.Trim()) ? new Random().Next(200, 1900) : result.Rows[i].ItemArray[3]?.ToString()!.Trim()
                };
                await new ClientAccountReceiptData().SearchAndCreateAClientAccountReceiptDataAsync(receiptsEntityData);
            }
        }

        [Test]
        public async Task ExcelCreateTimeModifyAsync()
        {
            var dataReader = new DataReader("3E Lite Time Cost Modify Sample data v0.1.xlsx");

            var result = dataReader.LoadDataTableFromExcelSheet("8_Time_Modify");
            for (var i = 1; i < result.Rows.Count; i++)
            {
                var timeModifyEntity = new ApiTimeModifyEntity()
                {
                    TimeType = result.Rows[i].ItemArray[2]?.ToString()!.Trim(),
                    WorkDate = result.Rows[i].ItemArray[3]?.ToString()!.Trim(),
                    WorkHours = result.Rows[i].ItemArray[4]?.ToString()!.Trim(),
                    Narrative = result.Rows[i].ItemArray[7]?.ToString()!.Trim(),
                    // -- this a default value, if value is not default then we need extend the api --
                    //WorkType = result.Rows[i].ItemArray[2]?.ToString()!.Trim(),
                    MatterNumber = result.Rows[i].ItemArray[10]?.ToString()!.Trim(),
                    FeeEarnerId = result.Rows[i].ItemArray[11]?.ToString()!.Trim()
                };
                Console.WriteLine("Result Row Id : " + i);
                await new TimeModifyData().CreateTimeModifyAsync(timeModifyEntity);
            }

        }

        [Test]
        public async Task ExcelCreateCostModifyAsync()
        {
            var dataReader = new DataReader("3E Lite Time Cost Modify Sample data v0.1.xlsx");

            var result = dataReader.LoadDataTableFromExcelSheet("9_Disbursement_Modify");
            for (var i = 1; i < result.Rows.Count; i++)
            {
                var disbursementModifyEntity = new ApiDisbursementModifyEntity()
                {
                    WorkDate = result.Rows[i].ItemArray[0]?.ToString()!.Trim(),
                    WorkQuantity = result.Rows[i].ItemArray[3]?.ToString()!.Trim(),
                    Currency = result.Rows[i].ItemArray[4]?.ToString()!.Trim(),
                    WorkRate = result.Rows[i].ItemArray[5]?.ToString()!.Trim(),
                    Narrative = result.Rows[i].ItemArray[6]?.ToString()!.Trim(),
                    DisbursementTypeCode = result.Rows[i].ItemArray[8]?.ToString()!.Trim(),
                    MatterNumber = result.Rows[i].ItemArray[9]?.ToString()!.Trim()
                };

                Console.WriteLine("Result Row Id : " + i);
                await new DisbursementModifyData().CreateDisbursementModifyAsync(disbursementModifyEntity);
            }

        }


    }
}
