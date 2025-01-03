using JobCandidateHubAPI;
using JobCandidateHubAPI.DataAccess;
using JobCandidateHubAPI.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
string conn = builder.Configuration.GetConnectionString("CandidateDb");
builder.Services.AddDbContext<CandidateDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("CandidateDb")));
builder.Services.AddScoped<ICandidateRepository, CandidateRepository>();
builder.Services.AddMemoryCache();

var app = builder.Build();

app.MapPost("/api/candidates",async (Candidate candidate, ICandidateRepository repository) => {
    var validationResult = ValidateModel(candidate);
    if (validationResult.Any())
    {
        return Results.BadRequest(validationResult);
    }
    var existingCandidate = await repository.ExistsByEmail(candidate.Email);
    if (existingCandidate != null)
    {
        existingCandidate.FirstName = candidate.FirstName;
        existingCandidate.LastName = candidate.LastName;
        existingCandidate.Email = candidate.Email;
        existingCandidate.TimeInterval = candidate.TimeInterval;
        existingCandidate.Comment = candidate.Comment;
        existingCandidate.PhoneNumber = candidate.PhoneNumber;

        repository.Update(existingCandidate);
        return Results.Ok(existingCandidate);
    }
    else
    {
        await repository.Add(candidate);
        return Results.Created($"/api/candidates/{candidate.Id}", candidate);
    }
});
static List<string> ValidateModel(object model)
{
    var validationResults = new List<ValidationResult>();
    var validationContext = new ValidationContext(model);

    bool isValid = Validator.TryValidateObject(model, validationContext, validationResults, true);

    return isValid ? new List<string>() : validationResults.Select(vr => vr.ErrorMessage).ToList();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
