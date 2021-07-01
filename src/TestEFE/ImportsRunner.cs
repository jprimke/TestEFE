//-----------------------------------------------------------------------
// <copyright file="D:\PROJEKTE\TestEFE\src\TestEFE\ImportsRunner.cs" company="AXA Partners">
// Author: Jörg H Primke
// Copyright (c) 2021 - AXA Partners. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bogus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TestEFE.Database;
using TestEFE.Models;
using Z.BulkOperations;

namespace TestEFE
{
    internal class ImportsRunner : BackgroundService
    {
        private readonly ILogger<ImportsRunner> logger;

        private readonly IHostApplicationLifetime applicationLifetime;

        private readonly IServiceScopeFactory scopeFactory;

        public ImportsRunner(ILogger<ImportsRunner> logger, IServiceScopeFactory scopeFactory, IHostApplicationLifetime applicationLifetime)
        {
            this.scopeFactory = scopeFactory;
            this.applicationLifetime = applicationLifetime;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                List<Task> tasks = Enumerable.Range(0, 1).Select(import => RunImport(stoppingToken)).ToList();

                await Task.WhenAll(tasks).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Log(ex);
            }
            finally
            {
                applicationLifetime.StopApplication();
            }
        }

        private async Task RunImport(CancellationToken cancellationToken = default)
        {
            using IServiceScope scope = scopeFactory.CreateScope();

            GenericDataContext context = scope.ServiceProvider.GetRequiredService<GenericDataContext>();

            List<Policy> policies = new();
            long count = 0L, successful = 0L;

            foreach (Policy policy in GeneratePolicies())
            {
                cancellationToken.ThrowIfCancellationRequested();

                count++;

                policies.Add(policy);

                if (count % 1000 == 0)
                {
                    MergePolicies(policies, ref successful, context);
                }
            }

            MergePolicies(policies, ref successful, context);

            await Task.CompletedTask;
        }

        private IEnumerable<Policy> GeneratePolicies()
        {
            int productId = 341;
            int schemeAutoId = 377;
            int schemeTravelId = 413;

            string[] policyNumbers = new[] { "eins", "zwei", "drei", "vier", "fünf", "sechs" };
            int i = 0;

            Faker<AutoPaket> testAutoPakets = new Faker<AutoPaket>().RuleFor(p => p.Active, f => true)
                                                                    .RuleFor(p => p.Created, f => f.Date.Past(1))
                                                                    .RuleFor(p => p.CreatedBy, f => "AXA Partners")
                                                                    .RuleFor(p => p.ChangedBy, f => "AXA Partners")
                                                                    .RuleFor(p => p.LastChanged, f => DateTime.Now)
                                                                    .RuleFor(p => p.ValidFrom, f => f.Date.Past(0))
                                                                    .RuleFor(p => p.ValidTo, f => f.Date.Future(2))
                                                                    .RuleFor(p => p.Name, f => "TestPaket")
                                                                    .RuleFor(p => p.SchemeId, f => schemeAutoId)
                                                                    .RuleFor(p => p.Brand, f => f.Vehicle.Manufacturer())
                                                                    .RuleFor(p => p.Model, f => f.Vehicle.Model())
                                                                    .RuleFor(p => p.Vin, f => f.Vehicle.Vin())
                                                                    .RuleFor(p => p.FirstRegistrationDate, f => f.Date.Past(2));

            Faker<TravelPaket> testTravelPakets = new Faker<TravelPaket>().RuleFor(p => p.Active, f => true)
                                                                        .RuleFor(p => p.Created, f => f.Date.Past(1))
                                                                        .RuleFor(p => p.CreatedBy, f => "AXA Partners")
                                                                        .RuleFor(p => p.ChangedBy, f => "AXA Partners")
                                                                        .RuleFor(p => p.LastChanged, f => DateTime.Now)
                                                                        .RuleFor(p => p.ValidFrom, f => f.Date.Past(0))
                                                                        .RuleFor(p => p.ValidTo, f => f.Date.Future(2))
                                                                        .RuleFor(p => p.Name, f => "TravelPaket")
                                                                        .RuleFor(p => p.SchemeId, f => schemeTravelId)
                                                                        .RuleFor(p => p.Birthday, f => f.Date.Past(18))
                                                                        .RuleFor(
                                                    p => p.InsuredPerson,
                                                    f =>
                                                        new Models.Person
                                                        {
                                                            FirstName = f.Name.FirstName(),
                                                            Name = f.Name.LastName()
                                                        });
;

            Faker<Policy> testPolicies = new Faker<Policy>().RuleFor(p => p.PolicyNumber, f => policyNumbers[i++])
                                                            .RuleFor(
                                             p => p.Person,
                                             f =>
                                                 new Models.Person
                                                 {
                                                     FirstName = f.Name.FirstName(),
                                                     Name = f.Name.LastName()
                                                 })
                                                            .RuleFor(
                                             p => p.Address,
                                             f =>
                                                 new Address
                                                 {
                                                     City = f.Address.City(),
                                                     CountryCode = f.Address.CountryCode(),
                                                     Street = f.Address.StreetAddress(),
                                                     Zip = f.Address.ZipCode()
                                                 })
                                                            .RuleFor(
                                             p => p.Contact,
                                             (f, p) =>
                                                 new Contact
                                                 {
                                                     Email = f.Internet.Email(p.Person.FirstName, p.Person.Name),
                                                     Phone = f.Phone.PhoneNumber()
                                                 })
                                                            .RuleFor(p => p.Created, f => f.Date.Past(1))
                                                            .RuleFor(p => p.CreatedBy, f => "AXA Partners")
                                                            .RuleFor(p => p.ChangedBy, f => "AXA Partners")
                                                            .RuleFor(p => p.LastChanged, f => DateTime.Now)
                                                            .RuleFor(p => p.Active, f => true)
                                                            .RuleFor(p => p.ProductName, f => "Testproduct")
                                                            .RuleFor(p => p.ProductId, f => productId)
                                                            .RuleFor(p => p.ValidFrom, f => f.Date.Past(0))
                                                            .RuleFor(p => p.ValidTo, f => f.Date.Future(2))
                                                            .FinishWith(
                                             (f, p) =>
                                             {
                                                 foreach (var item in testAutoPakets.Generate(1))
                                                 {
                                                     p.Pakets.Add(item);
                                                 }
                                                 foreach (var item in testTravelPakets.Generate(1))
                                                 {
                                                     p.Pakets.Add(item);
                                                 }
                                             });

            try
            {
                List<Policy> policies = testPolicies.Generate(5);
                return policies!;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while genrating policies");
                return Enumerable.Empty<Policy>();
            }
        }

        private void MergePolicies(List<Policy> policies, ref long successful, GenericDataContext context)
        {
            ResultInfo resultInfo = new ResultInfo();
            BulkMergePolicies(policies, resultInfo, context);
            successful += resultInfo.RowsAffected;
            policies.Clear();
        }

        private void BulkMergePolicies(List<Policy> policies, ResultInfo resultInfo, GenericDataContext context)
        {
            List<BulkOperationError> bulkErrors = new();

            try
            {
                context.BulkMerge(
                    policies,
                    options =>
                    {
                        options.IncludeGraph = true;
                        options.IncludeGraphOperationBuilder = operation =>
                                                               {
                                                                   if (operation is BulkOperation<Policy>)
                                                                   {
                                                                       BulkOperation<Policy> bulk = (BulkOperation<Policy>)operation;
                                                                       bulk.ColumnPrimaryKeyExpression = x =>
                                                                                                             new
                                                                                                             {
                                                                                                                 x.ContractId,
                                                                                                                 x.PolicyNumber,
                                                                                                                 x.AdditionalPartForPolicyNumber,
                                                                                                             };
                                                                       bulk.IgnoreOnMergeUpdateExpression = c =>
                                                                                                                new
                                                                                                                {
                                                                                                                    c.Created,
                                                                                                                    c.CreatedBy,
                                                                                                                    c.PolicyNumber,
                                                                                                                    c.AdditionalPartForPolicyNumber
                                                                                                                };
                                                                   }
                                                                   else if (operation is BulkOperation<Paket>)
                                                                   {
                                                                       BulkOperation<Paket> bulk = (BulkOperation<Paket>)operation;
                                                                       bulk.ColumnPrimaryKeyExpression = x =>
                                                                                                             new
                                                                                                             {
                                                                                                                 x.PolicyId,
                                                                                                                 x.Name,
                                                                                                                 x.Special,
                                                                                                                 x.PaketType
                                                                                                             };
                                                                       bulk.IgnoreOnMergeUpdateExpression = c =>
                                                                                                                new
                                                                                                                {
                                                                                                                    c.Created,
                                                                                                                    c.CreatedBy,
                                                                                                                    c.Name,
                                                                                                                    c.Special,
                                                                                                                    c.PaketType,
                                                                                                                    c.PolicyId
                                                                                                                };
                                                                   }
                                                               };
                        options.UseRowsAffected = true;
                        options.ResultInfo = resultInfo;
                        options.Errors = bulkErrors;
                    });

                foreach (BulkOperationError error in bulkErrors)
                {
                    logger.LogError("Error in BulkMerge @{error}", error);
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Exception in BulkMerge @{errors}", bulkErrors);
            }
        }

        private void Log(Exception ex) => logger.LogError(ex, $"Exception while running ImportsRunner");
    }
}
