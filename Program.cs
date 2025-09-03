using DotNetEnv;

// Carrega variáveis definidas no arquivo .env para o ambiente
Env.Load();
// Cria o builder da aplicação com a configuração padrão do ASP.NET Core
var builder = WebApplication.CreateBuilder(args);

// Registra MVC (controllers e views)
builder.Services.AddControllersWithViews();

// Registra HttpClient para chamadas HTTP externas
builder.Services.AddHttpClient();

var app = builder.Build();

// Configura o pipeline de requisição
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // Página genérica de erro em produção
}
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

// Rota padrão aponta para Weather/Index
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Weather}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
