using Automation.Engine.Application.UseCases;
using Automation.Engine.Domain.Interfaces;
using Automation.Engine.Infrastructure.Crawler;
using Automation.Engine.Infrastructure.Persistence;
using Automation.Engine.Infrastructure.Rpa;
using Automation.Engine.Worker;
using Automation.Engine.Worker.Jobs;
using Microsoft.EntityFrameworkCore;
using Quartz;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<AutomationContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

builder.Services.AddScoped<IQuoteRepository, QuoteRepository>();
builder.Services.AddScoped<ICrawlerService, CrawlerService>();
builder.Services.AddScoped<IRpaService, RpaService>();

builder.Services.AddScoped<ProcessQuoteUseCase>();

builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();

    var jobKey = new JobKey("QuoteJob");
    q.AddJob<QuoteJob>(opts => opts.WithIdentity(jobKey));

    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("QuoteJob-trigger")
        .StartNow()
        .WithSimpleSchedule(x => x
            .WithIntervalInMinutes(builder.Configuration.GetValue<int>("Schedule:IntervalMinutes"))
            .RepeatForever()));
});

builder.Services.AddQuartzHostedService(opt => opt.WaitForJobsToComplete = true);

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
