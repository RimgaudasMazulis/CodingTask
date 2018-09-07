using System;
using System.Collections.Generic;
using System.Linq;
using CodeChallenge.Core.Entities;

namespace CodeChallenge.Data.Context
{
    public static class DbInitializer
    {
        public static void Initialize(CodeChallengeContext context)
        {
            context.Database.EnsureCreated();

            if (context.Taxes.Any())
            {
                return;   // Db is created
            }
            var year = new TaxType() { TaxName = "YEAR" };
            var month = new TaxType() { TaxName = "MONTH" };
            var week = new TaxType() { TaxName = "WEEK" };
            var day = new TaxType() { TaxName = "DAY" };

            context.TaxTypes.AddRange(new TaxType[] { year, month, week, day });

            var vilnius = new Municipality() { Name = "Vilnius", Taxes = new List<Tax>() };
            var kaunas = new Municipality() { Name = "Kaunas", Taxes = new List<Tax>() };
            var klaipeda = new Municipality() { Name = "Klaipeda", Taxes = new List<Tax>() };

            var tax1 = new Tax() { TaxTypeId = year.Id, Description = "Yearly tax", TaxAmount = 0.2, BeginDate = DateTime.Parse("2016-01-01"), EndDate = DateTime.Parse("2016-12-31"), MunicipalityName = "Vilnius", MunicipalityId = vilnius.Id };
            var tax2 = new Tax() { TaxTypeId = month.Id, Description = "Monthly tax", TaxAmount = 0.4, BeginDate = DateTime.Parse("2016-05-01"), EndDate = DateTime.Parse("2016-05-31"), MunicipalityName = "Vilnius", MunicipalityId = vilnius.Id };
            var tax3 = new Tax() { TaxTypeId = day.Id, Description = "Daily tax", TaxAmount = 0.1, BeginDate = DateTime.Parse("2016-01-01"), EndDate = DateTime.Parse("2016-01-01"), MunicipalityName = "Vilnius", MunicipalityId = vilnius.Id };
            var tax4 = new Tax() { TaxTypeId = day.Id, Description = "Daily tax", TaxAmount = 0.1, BeginDate = DateTime.Parse("2016-12-25"), EndDate = DateTime.Parse("2016-12-25"), MunicipalityName = "Vilnius", MunicipalityId = vilnius.Id };
     
            vilnius.Taxes.Add(tax1);
            vilnius.Taxes.Add(tax2);
            vilnius.Taxes.Add(tax3);
            vilnius.Taxes.Add(tax4);            

            context.Municipalities.Add(vilnius);
            context.Municipalities.Add(kaunas);
            context.Municipalities.Add(klaipeda);

            context.SaveChanges();
        }
    }
}