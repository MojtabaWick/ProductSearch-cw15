using cw15.DTOs;
using cw15.Infrastructure.Database;
using cw15.Services;
using cw15.Tools;

using var db = new AppDbContext();

var _service = new ProductService();

while (true)
{
    Console.Clear();
    Console.WriteLine(" Product Search");
    Console.WriteLine("------------------");

    var filter = new ProductSearchDto();

    Console.Write("Product name (optional): ");
    filter.Name = Console.ReadLine();

    Console.Write("Min price (optional): ");
    if (decimal.TryParse(Console.ReadLine(), out decimal min)) filter.MinPrice = min;

    Console.Write("Max price (optional): ");
    if (decimal.TryParse(Console.ReadLine(), out decimal max)) filter.MaxPrice = max;

    Console.Write("Color (optional): ");
    filter.Color = Console.ReadLine();

    Console.Write("Brand (optional): ");
    filter.Brand = Console.ReadLine();

    Console.Write("Category ID (optional): ");
    if (int.TryParse(Console.ReadLine(), out int catId)) filter.CategoryId = catId;

    Console.Write("Category name (optional): ");
    filter.CategoryName = Console.ReadLine();

    Console.Write("Sort by (price / name / stock): ");
    filter.SortBy = Console.ReadLine();

    Console.Write("Descending? (y/n): ");
    filter.IsDescending = Console.ReadLine()?.ToLower() == "y";

    filter.PageSize = 3; // 3 products per page

    // Run search
    var results = _service.SearchProduct(filter);

    // Paging
    int currentPage = 1;
    while (true)
    {
        Console.Clear();
        Console.WriteLine($"📄 Page {currentPage}\n");

        var page = results.Skip((currentPage - 1) * filter.PageSize).Take(filter.PageSize)
            .ToList();

        if (page.Count == 0)
            Console.WriteLine("❌ No products found.");
        else
            ConsolePainter.WriteTable(page);


        Console.WriteLine("\n[n] next | [p] previous | [r] new search | [q] quit");
        var key = Console.ReadKey(true).KeyChar;

        if (key == 'n' && currentPage * filter.PageSize < results.Count)
            currentPage++;
        else if (key == 'p' && currentPage > 1)
            currentPage--;
        else if (key == 'r')
            break;
        else if (key == 'q')
            return;
    }
}