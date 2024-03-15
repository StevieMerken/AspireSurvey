using AspireSurvey.Core;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();


app.MapGet("/survey/{id}", (string id) =>
{
    var survey = new Survey()
    {
        Id = Guid.Parse(id),
        Name = $"Survey {id}",
        Description = "First description",
        Questions = new List<SurveyQuestion>
        {
            new FreeText()
            {
                Id = Guid.NewGuid(),
                Question = "Omschrijving 1",
                MaxLength = 255,
                Required = true,
            }
        }
    };
    return survey;
});

app.MapPost("/survey", (SurveyResult result) =>
{
    return Results.Ok();
});

app.MapDefaultEndpoints();

app.Run();

