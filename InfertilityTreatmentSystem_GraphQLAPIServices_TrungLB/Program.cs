using InfertilityTreatmentSystem.Services.TrungLB.Service;
using InfertilityTreatmentSystem_GraphQLAPIServices_TrungLB.GraphQLs;
using InfertilityTreatmentSystem.Repositories.TrungLB.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register your services
builder.Services.AddScoped<IServiceProviders, ServiceProviders>();

// Configure GraphQL
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Queries>()
    .AddMutationType<Mutations>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS for Blazor WebAssembly
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorWasm", policy =>
    {
        policy.WithOrigins("https://localhost:7231", "http://localhost:5052", "https://localhost:7139")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    // Seed some test data
    await SeedTestData(app.Services);
}

app.UseHttpsRedirection();

// Enable CORS
app.UseCors("AllowBlazorWasm");

app.UseAuthorization();

app.MapControllers();

// Map GraphQL endpoint
app.MapGraphQL();

app.Run();

// Method to seed test data
async Task SeedTestData(IServiceProvider services)
{
    try
    {
        using var scope = services.CreateScope();
        var serviceProviders = scope.ServiceProvider.GetRequiredService<IServiceProviders>();
        
        // Seed user accounts first
        await SeedUserAccounts(serviceProviders);
        
        // Check if we already have reminder data
        var existingReminders = await serviceProviders.TreatmentReminderTrungLbService.GetAllAsync();
        
        if (existingReminders.Any())
        {
            return; // Data already exists, skip seeding
        }

        // Create some reminder types first
        var reminderType1 = new ReminderTypeTrungLb
        {
            Name = "Medication",
            Description = "Medication reminder"
        };
        
        var reminderType2 = new ReminderTypeTrungLb
        {
            Name = "Appointment",
            Description = "Doctor appointment reminder"
        };

        await serviceProviders.ReminderTypeTrungLbService.CreateAsync(reminderType1);
        await serviceProviders.ReminderTypeTrungLbService.CreateAsync(reminderType2);

        // Create some test treatment reminders
        var testReminders = new List<TreatmentReminderTrungLb>
        {
            new TreatmentReminderTrungLb
            {
                Title = "Take Morning Medication",
                Message = "Remember to take your fertility medication",
                ReminderDate = DateTime.Now.AddHours(2),
                CreatedAt = DateTime.Now,
                IsSent = false,
                IsRecurring = true,
                PatientName = "Jane Smith",
                RelatedDoctor = "Dr. Johnson",
                ReminderTypeId = 1
            },
            new TreatmentReminderTrungLb
            {
                Title = "Doctor Appointment",
                Message = "Scheduled appointment for fertility consultation",
                ReminderDate = DateTime.Now.AddDays(3),
                CreatedAt = DateTime.Now,
                IsSent = false,
                IsRecurring = false,
                PatientName = "John Doe",
                RelatedDoctor = "Dr. Williams",
                ReminderTypeId = 2
            },
            new TreatmentReminderTrungLb
            {
                Title = "Evening Medication",
                Message = "Take evening hormone therapy",
                ReminderDate = DateTime.Now.AddHours(8),
                CreatedAt = DateTime.Now,
                IsSent = false,
                IsRecurring = true,
                PatientName = "Mary Johnson",
                RelatedDoctor = "Dr. Brown",
                ReminderTypeId = 1
            }
        };

        foreach (var reminder in testReminders)
        {
            await serviceProviders.TreatmentReminderTrungLbService.CreateAsync(reminder);
        }
    }
    catch (Exception ex)
    {
        // Silently handle seeding errors
        Console.WriteLine($"Seeding error: {ex.Message}");
    }
}

// Method to seed user accounts
async Task SeedUserAccounts(IServiceProviders serviceProviders)
{
    try
    {
        // Check if admin user already exists
        var existingUser = await serviceProviders.SystemUserAccountService.GetAccountAsync("admin", "Admin1234!");
        
        if (existingUser == null)
        {
            // Create test user accounts if they don't exist
            var testUsers = new List<SystemUserAccount>
            {
                new SystemUserAccount
                {
                    UserName = "admin",
                    Password = "Admin1234!",
                    FullName = "System Administrator",
                    Email = "admin@infertilitytreatment.com",
                    Phone = "0967425254",
                    EmployeeCode = "EMP001",
                    RoleId = 1,
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    ApplicationCode = "INFERTILITY_SYSTEM",
                    CreatedBy = "System"
                },
                new SystemUserAccount
                {
                    UserName = "doctor.leminh",
                    Password = "Doc2024!",
                    FullName = "Dr. Le Minh",
                    Email = "doctor.leminh@infertilitytreatment.com",
                    Phone = "0912345678",
                    EmployeeCode = "DOC001",
                    RoleId = 2,
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    ApplicationCode = "INFERTILITY_SYSTEM",
                    CreatedBy = "System"
                }
            };

            // Use UnitOfWork to add users directly to the database
            using var unitOfWork = new InfertilityTreatmentSystem.Repositories.TrungLB.UnitOfWork();
            
            foreach (var user in testUsers)
            {
                await unitOfWork.SystemUserAccountRepository.CreateAsync(user);
            }
            
            await unitOfWork.SaveChangesWithTransactionAsync();
            Console.WriteLine("Test user accounts created successfully.");
        }
        else
        {
            Console.WriteLine("Test user accounts already exist.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error seeding user accounts: {ex.Message}");
    }
}
