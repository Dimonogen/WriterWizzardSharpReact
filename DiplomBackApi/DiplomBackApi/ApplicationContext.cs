using Microsoft.EntityFrameworkCore;
//using System.Data.Entity;
using Litbase.Models;
using Litbase.DTO;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Litbase;

/// <summary>
/// Класс контекста БД
/// </summary>
public class ApplicationContext : DbContext
{
    /// <summary>
    /// Таблица пользователей
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// Таблица типов объектов
    /// </summary>
    public DbSet<ObjType> ObjTypes { get; set; }

    /// <summary>
    /// Таблица атрибутов типов объектов
    /// </summary>
    public DbSet<ObjTypeAttribute> ObjTypeAttributes { get; set; }

    /// <summary>
    /// Таблица объектов
    /// </summary>
    public DbSet<Obj> Objs { get; set; }

    /// <summary>
    /// Таблица атрибутов объектов
    /// </summary>
    public DbSet<ObjAttribute> ObjAttributes { get; set; }

    /// <summary>
    /// Таблица дополнительных атрибутов, не входящих в тип
    /// </summary>
    public DbSet<ObjAdditionalAttribute> ObjAdditionalAttributes { get; set; }

    /// <summary>
    /// Таблица типов атрибутов
    /// </summary>
    public DbSet<AttributeType> AttributeTypes { get; set; }

    /// <summary>
    /// Таблица избранных объектов
    /// </summary>
    public DbSet<FavoriteObj> FavoriteObjs { get; set; }

    /// <summary>
    /// Состояния объектов
    /// </summary>
    public DbSet<ObjState> ObjStates { get; set; }

    /// <summary>
    /// Пункты левого меню
    /// </summary>
    public DbSet<MenuElement> MenuElements { get; set; }

    /// <summary>
    /// Таблица связей объектов
    /// </summary>
    public DbSet<LinkObj> LinkObjs { get; set; }

    /// <summary>
    /// Настройки пользователей
    /// </summary>
    public DbSet<UserSettings> UserSettings { get; set; }

    //private IConfiguration _configuration;

    //private ILogger _logger { get; }

    /// <summary>
    /// Конструктор контекста БД
    /// </summary>
    /*public ApplicationContext(IConfiguration configuration, ILogger<ApplicationContext> logger)
    {
        _configuration = configuration;
        _logger = logger;
        Database.EnsureCreated();
    }*/

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    public int GetCurId<TEntity>(int userId, DbSet<TEntity> dbSet) where TEntity : BaseEntity
    {
        int count = dbSet.Where(x => x.UserId == userId).Count();
        int maxId = 0;
        if (count > 0)
            maxId = dbSet.Where(x => x.UserId == userId).Max(x => x.Id);
        return maxId;
    }


    /// <summary>
    /// Вызываемая при конфигурации функция, здесь настройки подключения к БД
    /// </summary>
    /// <param name="optionsBuilder"></param>
    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _configuration.GetSection("ConnectionString");
        optionsBuilder.UseNpgsql(connectionString.Value);

        
        //base.OnConfiguring(optionsBuilder);
    }*/

    /// <summary>
    /// При создании модели БД вызывается функция, но делает ли она что-то хз. Вроде миграции базу меняют, а это шляпа...
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Diplom");
        base.OnModelCreating(modelBuilder);
    }

    /// <summary>
    /// Очистка и заполнение базы заранее подготовленными данными
    /// </summary>
    /// <returns></returns>
    public async Task ClearInit()
    {
        string DataFolder = "SeedData";

        await ClearData(UserSettings);
        await ClearData(MenuElements);
        await ClearData(ObjAttributes);
        await ClearData(Objs);
        await ClearData(LinkObjs);
        await ClearData(ObjStates);
        await ClearData(ObjTypeAttributes);
        await ClearData(AttributeTypes);
        await ClearData(ObjTypes);
        await ClearData(Users);

        await SeedData(Users, DataFolder);
        await SeedData(ObjStates, DataFolder);
        await SeedData(ObjTypes, DataFolder);
        await SeedData(AttributeTypes, DataFolder);
        await SeedData(ObjTypeAttributes, DataFolder);
        await SeedData(Objs, DataFolder);
        await SeedData(LinkObjs, DataFolder);
        await SeedData(ObjAttributes, DataFolder);
        await SeedData(MenuElements, DataFolder);
        await SeedData(UserSettings, DataFolder);
    }

    /// <summary>
    /// Заполнение базы минимальными данными для пользователя нового
    /// </summary>
    /// <param name="user"></param>
    /// <param name="templateId"></param>
    /// <returns></returns>
    public async Task UserInitDb(User user, int templateId)
    {
        string fileJson = File.ReadAllText(Path.Combine("Templates", $"Template_{templateId}.json"));

        JObject jo = JObject.Parse(fileJson);

        foreach (var item in jo)
        {
            string value = item.Value.ToString();
            switch (item.Key)
            {
                case "ObjType":
                    await UserInitTable<ObjType>(user, value, ObjTypes);
                    break;
                case "ObjState":
                    await UserInitTable<ObjState>(user, value, ObjStates);
                    break;
                case "Obj":
                    await UserInitTable<Obj>(user, value, Objs);
                    break;
                case "AttributeType":
                    await UserInitTable<AttributeType>(user, value, AttributeTypes);
                    break;
                case "MenuElement":
                    await UserInitTable<MenuElement>(user, value, MenuElements);
                    break;
                default:
                    break;
            }
        }
    }


    private async Task UserInitTable<TEntity>(User user, string data, DbSet<TEntity> dbSet) where TEntity : BaseEntity
    {
        string name = typeof(TEntity).Name;
        try
        {
            var objs = JsonConvert.DeserializeObject<List<TEntity>>(data);

            if (objs == null)
                return;

            for (int i = 0; i < objs.Count; i++)
            {
                objs[i].UserId = user.Id;
            }

            await dbSet.AddRangeAsync(objs);
            await this.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            //Console.WriteLine($"Error Seeding data entity: {name}, error msg:{ex.Message} \n\n {ex.StackTrace}");
            //_logger.LogError($"Error init data entity: {name}, error msg:{ex.Message} \n\n {ex.StackTrace}");
        }
    }

    

    /// <summary>
    /// Функция очистки таблицы соответствующего DbSet'а.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="dbSet"></param>
    /// <returns></returns>
    private async Task ClearData<TEntity>(DbSet<TEntity> dbSet) where TEntity : class
    {
        dbSet.ExecuteDelete();
        await this.SaveChangesAsync();
    }

    /// <summary>
    /// Функция заполнения таблицы соответствующего DbSet'а начальными тестовыми данными.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="dbSet"></param>
    /// <param name="filePath"></param>
    /// <returns></returns>
    private async Task SeedData<TEntity>(DbSet<TEntity> dbSet, string filePath) where TEntity : class
    {
        string name = typeof(TEntity).Name;
        //Console.WriteLine($"Start seeding: {name}");
        //_logger.LogDebug($"Start seeding: {name}");

        try
        {
            string fileJson = File.ReadAllText(Path.Combine(filePath, name + ".json"));

            var objs = JsonConvert.DeserializeObject<List<TEntity>>(fileJson);

            if (objs == null)
                return;

            dbSet.AddRange(objs);

            await this.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            //Console.WriteLine($"Error Seeding data entity: {name}, error msg:{ex.Message} \n\n {ex.StackTrace}");
            //_logger.LogError($"Error Seeding data entity: {name}, error msg:{ex.Message} \n\n {ex.StackTrace}");
        }

        try
        {
            var type = typeof(TEntity);
            var id = type.GetProperty("Id");

            if (id == null)
                return;

            string q1 = $"select max(id) from \"Diplom\".{name.ToLower()};";

            int max_id = (Database.SqlQueryRaw<int>(q1).ToList())[0];

            string query = $"select setval('\"Diplom\".{name.ToLower()}_id_seq',{max_id});";

            var seq = Database.SqlQueryRaw<long>(query).ToList();

            await this.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            //Console.WriteLine($"Error fix DB sequence entity: {name}, error msg:{ex.Message} \n\n {ex.StackTrace}");
            //_logger.LogError($"Error fix DB sequence entity: {name}, error msg:{ex.Message} \n\n {ex.StackTrace}");
        }
        
    }

    /// <summary>
    /// Сохранить базу данных в json'ы
    /// </summary>
    /// <returns></returns>
    public async Task SaveDbToJson()
    {
        string folder = "DbDump";

        var type = GetType();
        var members = type.GetProperties();
            
        var membersFiltr = members.Where(m => m.PropertyType.Name.Contains("DbSet")).ToList();
        foreach(var elem in membersFiltr)
        {
            Console.WriteLine(elem.Name);

            var set = elem.GetValue(this); //Convert.ChangeType( elem.GetValue(this), elem.PropertyType);

            var method = elem.PropertyType.GetMethod("AsQueryable");
            var result = method.Invoke(set, null);
            var list = ((IQueryable) result).Cast<object>().ToList();
            string json = JsonConvert.SerializeObject(list, Formatting.Indented);
            var name = elem.PropertyType.GenericTypeArguments[0].Name;
            File.WriteAllText(folder + Path.DirectorySeparatorChar + name + ".json", json);
            //Console.WriteLine();
            //var list = await result;
            //var method = methods.Where(m => m.Name.Contains("AsAsyncEnumerable")).ToList();
            //var list = 
        }
        
    }

}
